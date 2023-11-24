using System.Dynamic;

namespace Lib;

public class Baker
{
    public int ID {get; set;}
    public int Performance {get; set;}
    public int Time {get; set;}
    public Order? CurrentOrder {get; set;}
    public bool inQueue {get; set;}

    public Baker(int ID, int Performance) {
        this.ID = ID;
        this.Performance = Performance;
        Time = 0;
    }

    public void Cook(Order order) {
        CurrentOrder = order;
        Time += (int)Consts.TimeToCook.TotalSeconds / Performance;
        order.setTimeInOven((int)Consts.TimeToCook.TotalSeconds / Performance);
        order.baker = this;
    }
}
