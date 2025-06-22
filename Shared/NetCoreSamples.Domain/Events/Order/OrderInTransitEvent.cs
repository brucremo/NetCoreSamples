namespace NetCoreSamples.Domain.Events.Order
{
    public record OrderInTransitEvent(Guid OrderId, DateTime CreatedOn, DateTime EstimatedArrivalTime);
}
