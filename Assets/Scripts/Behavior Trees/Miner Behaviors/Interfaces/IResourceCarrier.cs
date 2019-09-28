public enum Resource
{
    Gold,
    Iron,
    Wood
}

public interface IResourceCarrier
{
    Resource ResourceCarried { get; }
    int AmountCarried { get; set; }
    int MaxCarryAmount { get; }
}