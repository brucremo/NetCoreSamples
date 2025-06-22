namespace NetCoreSamples.Domain.Events.Order
{
    public record OrderShippedEvent(Guid OrderId, string? TrackingNumber, DateTime CreatedOn);
}
