namespace Lib;

public class Storage {
    public int Capacity {get; set;}
    List<Courier>? couriers;
    PriorityQueue<Baker, int>? bakersQueue;
    List<Order>[]? ordersInTimeMoment;

    public Storage(int Capacity, List<Courier> couriers, int workdayLength) {
        this.Capacity = Capacity;
        this.couriers = couriers;
        ordersInTimeMoment = new List<Order>[workdayLength];
        for(int i = 0; i< ordersInTimeMoment.Length; i++) ordersInTimeMoment[i] = new List<Order>();
        bakersQueue = new PriorityQueue<Baker, int>();
    }

    public void ProcessingBakers(Baker baker) {
        if(ordersInTimeMoment![baker.Time].Count + 1 > Capacity) {
            for(int i = baker.Time; i < ordersInTimeMoment.Length; i++) {
                if(ordersInTimeMoment[i].Count + 1 > Capacity) {
                    Order ord = ordersInTimeMoment[i].Last();
                    Baker b = ord.baker!;
                    bakersQueue!.Enqueue(b, b.Time);
                    b.inQueue = true;
                    ordersInTimeMoment[i].RemoveAt(0);
                }
                baker.CurrentOrder!.setTimeInQueueToWarehouse(0);
                ordersInTimeMoment[i].Add(baker.CurrentOrder);
                ordersInTimeMoment[i] = ordersInTimeMoment[i].OrderBy(o => o.baker!.Time).ToList();
            }
        }
        else {
            bakersQueue!.Enqueue(baker, baker.Time);
            baker.inQueue = true;
        }
    }

    public void OrdersInQueue() {
        for(int i=0; i<ordersInTimeMoment!.Length; i++) {
            var freeCouriers = couriers!.Where(c => c.Time <= i).ToList();
            foreach(Courier courier in freeCouriers) {
                if(ordersInTimeMoment[i].Count == 0) break;
                int ordersToTake = Math.Min(courier.GetMaxOrdersToDeliver(), ordersInTimeMoment[i].Count);
                var takenOrders = ordersInTimeMoment[i].GetRange(0, ordersToTake);
                courier.TakeOrders(takenOrders);
                for(int j = i; j < ordersInTimeMoment.Length; j++) {
                    foreach (var order in takenOrders) {
                        ordersInTimeMoment[j].Remove(order);
                    }
                }
                courier.Deliver();
            }
        }
    }

    public void BakersInQueue() {
        while(bakersQueue!.Count > 0) {
            Baker firstBaker = bakersQueue.Peek();
            bool isFreePlace = false;
            for(int i = firstBaker.Time; i < ordersInTimeMoment!.Length; i++) {
                if(ordersInTimeMoment[i].Count + 1 <= Capacity) {
                    firstBaker.CurrentOrder!.setTimeInQueueToWarehouse(i-firstBaker.Time);
                    firstBaker.Time = i;
                    for(int j = i; j < ordersInTimeMoment.Length; j++) ordersInTimeMoment[j].Add(firstBaker.CurrentOrder);
                    isFreePlace = true;
                    firstBaker.inQueue = false;
                    bakersQueue.Dequeue();
                    break;
                }
            }
            if(!isFreePlace) break;
        }
    }
}