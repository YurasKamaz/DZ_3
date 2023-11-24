namespace Lib;

public class Order {
    public int ID {get; set;}
    public Baker? baker {get; set;}
    public Courier? courier {get; set;}
    public TimeSpan CompleteTime {get; set;}
    public Consts.OrderStatuses Status {get; set;}
    Dictionary<Consts.OrderStatuses, int>? TimeOnStage;
    
    public Order(int ID, TimeSpan CompleteTime) {
        this.ID = ID;
        this.CompleteTime = CompleteTime;
        Status = Consts.OrderStatuses.InQueue;
        TimeOnStage = new Dictionary<Consts.OrderStatuses, int>();
    }

    public void setTimeInOven(int time) {
        setTimeOnStage(Consts.OrderStatuses.InOven, time);
    }

    public void setTimeInQueue(int time) {
        setTimeOnStage(Consts.OrderStatuses.InQueue, time);
    }

    public void setTimeInQueueToWarehouse(int time) {
        setTimeOnStage(Consts.OrderStatuses.InQueueToStorage, time);
    }

    public void setTimeInDelivery(int time) {
        setTimeOnStage(Consts.OrderStatuses.InDelivery, time);
    }
    
    public void Complete() {
        Status = Consts.OrderStatuses.Complited;
        int sumTime = TimeOnStage!.Sum(a => a.Value);
        setTimeOnStage(Status, sumTime);
    }

    private void setTimeOnStage(Consts.OrderStatuses status, int time) {
        if(TimeOnStage == null) TimeOnStage = new Dictionary<Consts.OrderStatuses, int>();
        else if(!TimeOnStage.ContainsKey(status)) {
            Console.WriteLine($"[{this.ID}] [{status}] [{TimeSpan.FromSeconds(time)}]");
            TimeOnStage.Add(status, time);
        }
        else TimeOnStage[status] = time;
        Status = status;
    }

    public TimeSpan GetComplinationTime() => TimeSpan.FromSeconds(TimeOnStage![Consts.OrderStatuses.Complited]);

    public Consts.OrderStatuses GetStageWithMaxTime() {
        int maxTime = int.MinValue;
        Consts.OrderStatuses status = Consts.OrderStatuses.Complited;
        foreach (var item in TimeOnStage!) {
            if(item.Key == Consts.OrderStatuses.Complited) continue;
            if(item.Value > maxTime) {
                maxTime = item.Value;
                status = item.Key;
            }
        }
        return status;
    }
}