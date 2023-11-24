namespace Lib;

public class Courier {
    public int ID {get; set;}
    public int Performance {get; set;}
    public int BagCapacity {get; set;}
    public int Time {get; set;}
    public List<Order> CurrentOrders {get; set;}

    public Courier(int ID, int Performance, int BagCapacity) {
        this.ID = ID;
        this.Performance = Performance;
        this.BagCapacity = BagCapacity;
        Time = 0;
        CurrentOrders = new List<Order>();
    }

    public int GetMaxOrdersToDeliver() => BagCapacity - CurrentOrders.Count;

    public void TakeOrders(List<Order> orders) {
        CurrentOrders.AddRange(orders);
    }

    public void Deliver() {
        int order_count = 0;
        while(CurrentOrders.Count > 0) {
            Order order = CurrentOrders.First();
            order.courier = this;
            Time = (int)Consts.TimeToDeliver.TotalSeconds / Performance;
            order.setTimeInDelivery((int)Consts.TimeToCook.TotalSeconds / Performance * (order_count+1));
            order.Complete();
            order_count++;
            CurrentOrders.RemoveAt(0);
        }
    }
}