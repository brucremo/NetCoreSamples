using NetCoreSamples.Domain.Constants;
using NetCoreSamples.Domain.Events.Order;
using NetCoreSamples.Domain.Models;
using NetCoreSamples.Domain.Projections.Base;

namespace NetCoreSamples.Domain.Projections
{
    /// <summary>
    /// Projection for a stream of Order events, representing the current state of an order.
    /// </summary>
    public class OrderProjection : SingleStreamProjectionBase<OrderProjection, OrderModel, Guid>
    {
        /// <summary>
        /// Creates a new instance of the OrderModel based on the OrderCreatedEvent.
        /// </summary>
        /// <param name="event">The <see cref="OrderCreatedEvent"/>.</param>
        /// <returns>An <see cref="OrderModel"/> representing the state of the order.</returns>
        public OrderModel Create(OrderCreatedEvent @event) =>
            new OrderModel
            {
                Id = @event.OrderId,
                UserId = @event.UserId,
                CreatedOn = @event.CreatedOn,
                LastUpdatedOn = @event.CreatedOn,
                Status = OrderConstants.DefaultOrderStatus
            };

        /// <summary>
        /// Applies the <see cref="OrderShippedEvent"> to the current state of the order.
        /// </summary>
        /// <param name="current">The current state of the order.</param>
        /// <param name="event">The event being applied.</param>
        /// <returns>An <see cref="OrderModel"/> representing the state of the order.</returns>
        public OrderModel Apply(OrderModel current, OrderShippedEvent @event) =>
            current with
            {
                ShippedOn = @event.CreatedOn,
                Status = OrderConstants.ShippedStatus,
                LastUpdatedOn = @event.CreatedOn,
                TrackingNumber = @event.TrackingNumber
            };

        /// <summary>
        /// Applies the <see cref="OrderInTransitEvent"> to the current state of the order.
        /// </summary>
        /// <param name="current">The current state of the order.</param>
        /// <param name="event">The event being applied.</param>
        /// <returns>An <see cref="OrderModel"/> representing the state of the order.</returns>
        public OrderModel Apply(OrderModel current, OrderInTransitEvent @event) =>
            current with
            {
                Status = OrderConstants.InTransitStatus,
                LastUpdatedOn = @event.CreatedOn
            };

        /// <summary>
        /// Applies the <see cref="OrderDeliveredEvent"> to the current state of the order.
        /// </summary>
        /// <param name="current">The current state of the order.</param>
        /// <param name="event">The event being applied.</param>
        /// <returns>An <see cref="OrderModel"/> representing the state of the order.</returns>
        public OrderModel Apply(OrderModel current, OrderDeliveredEvent @event) =>
            current with
            {
                DeliveredOn = @event.CreatedOn,
                Status = OrderConstants.DeliveredStatus,
                LastUpdatedOn = @event.CreatedOn
            };
    }
}
