namespace NetCoreSamples.Domain.Events.Order
{
    public record OrderCreatedEvent(Guid OrderId, Guid UserId, DateTime CreatedOn);
}
