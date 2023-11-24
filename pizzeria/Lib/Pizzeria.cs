namespace Lib;

public class Pizzeria {
    public List<Order>? Orders {get; set;}
    public List<Baker>? Bakers {get; set;}
    public List<Courier>? Couriers {get; set;}
    public int StorageCapacity {get; set;}
    
    Storage? Storage;
    List<Order>? CompleteOrders;

    public Pizzeria(List<Order> Orders, List<Baker> Bakers, List<Courier> Couriers, int StorageCapacity) {
        this.Orders = Orders;
        this.Bakers = Bakers;
        this.Couriers = Couriers;
        this.StorageCapacity = StorageCapacity;
        Storage = new Storage(StorageCapacity, Couriers, (int)Consts.WorkdayLength.TotalSeconds);
        CompleteOrders = Orders.Where(o => o.Status == Consts.OrderStatuses.Complited).ToList();
        foreach(Order order in CompleteOrders) Orders.Remove(order);
    }

    public void ProcessingOrders() {
        while(Orders!.Count > 0) {
            int orderIndex = 0;
            foreach(Baker baker in Bakers!) {
                if(orderIndex >= Orders.Count) break;

                Order order = Orders[orderIndex];
                if(order.Status == Consts.OrderStatuses.InQueue && !baker.inQueue) {
                    order.setTimeInQueue(baker.Time);
                    baker.Cook(order);
                    if(baker.Time >= (int)Consts.WorkdayLength.TotalSeconds) break;
                    Storage!.ProcessingBakers(baker);
                }
                orderIndex++;
            }
            Storage!.OrdersInQueue();
            Storage!.BakersInQueue();
            
            var complete = Orders.Where(o => o.Status == Consts.OrderStatuses.Complited);
            CompleteOrders!.AddRange(complete);
            foreach(Order order in CompleteOrders) Orders.Remove(order);
        }
    }

    public void GiveReport() {
        var freeOrders = CompleteOrders!.Where(o => o.GetComplinationTime() > o.CompleteTime);
        if(freeOrders.Count() == 0) {
            var lastOrderComplete = CompleteOrders!.Max(o => o.GetComplinationTime());
            if(Consts.TimeToCook + Consts.TimeToDeliver + lastOrderComplete <= Consts.WorkdayLength) Console.WriteLine("Увеличить число заказов");
            return;
        }

        Dictionary<Baker, int> bakersWithPoints = new Dictionary<Baker, int>();
        Dictionary<Courier, int> couriersWithPoints = new Dictionary<Courier, int>();
        int InQueueToWarehousePoints = 0;
        int InQueuePoints = 0;
        foreach(Order order in freeOrders) {
            Consts.OrderStatuses slowestStage = order.GetStageWithMaxTime();
            switch(slowestStage) {
                case Consts.OrderStatuses.InQueue:
                    InQueuePoints++;
                    break;
                case Consts.OrderStatuses.InOven:
                    if (bakersWithPoints.ContainsKey(order.baker!))
                        bakersWithPoints[order.baker!]++;
                    else
                        bakersWithPoints.Add(order.baker!, 1);
                    break;
                case Consts.OrderStatuses.InQueueToStorage:
                    InQueueToWarehousePoints++;
                    break;
                case Consts.OrderStatuses.InDelivery:
                    if (couriersWithPoints.ContainsKey(order.courier!))
                        couriersWithPoints[order.courier!]++;
                    else
                        couriersWithPoints.Add(order.courier!, 1);
                    break;
            }
        }
        var slowestCourier = couriersWithPoints.Where(c => c.Value == couriersWithPoints.Max(c => c.Value));
        var slowestBaker = bakersWithPoints.Where(b => b.Value == bakersWithPoints.Max(b => b.Value));
        if(InQueuePoints > freeOrders.Count() * 0.1 || slowestBaker.Count() == Bakers!.Count()) Console.WriteLine("Нанять нового пекаря");
        if(slowestBaker.Count() != Bakers!.Count()) {
            foreach(var b in slowestBaker) Console.WriteLine($"Уволить пекаря {b.Key.ID}");
        }
        if(InQueueToWarehousePoints > freeOrders.Count() * 0.1) Console.WriteLine("Расширить склад");
        if(slowestCourier.Count() == Couriers!.Count()) Console.WriteLine("Нанять нового курьера");
        if(slowestCourier.Count() != Couriers!.Count()) {
            foreach(var c in slowestCourier) Console.WriteLine($"Уволить курьера {c.Key.ID}");
        }
    }
}