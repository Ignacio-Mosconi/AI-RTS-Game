public enum Resource
{
    Gold,
    Iron,
    Wood
}

public interface IResourceCarrier
{
    Resource ResourceCarried { get; }
    int AmountCarried { get; }
    int MaxCarryAmount { get; }
}