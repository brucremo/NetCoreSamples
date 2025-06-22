namespace NetCoreSamples.Domain.Events.Order
{
    public record OrderDeliveredEvent(Guid OrderId, DateTime CreatedOn);
}
