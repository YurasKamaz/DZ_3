namespace Lib;

public static class Consts {
    public static readonly TimeSpan WorkdayLength = TimeSpan.FromSeconds(86400);
    public static readonly TimeSpan TimeToDeliver = TimeSpan.FromMinutes(60);
    public static readonly TimeSpan TimeToCook = TimeSpan.FromMinutes(60);
    public enum OrderStatuses { InQueue, InOven, InQueueToStorage, InDelivery, Complited }
}