using JasperFx.Core;
using JasperFx.Events;
using Marten;
using Microsoft.AspNetCore.Mvc;
using NetCoreSamples.Domain.Events.Order;
using NetCoreSamples.Domain.Models;
using NetCoreSamples.EventSourcing.Application.Commands.Order;

namespace NetCoreSamples.EventSourcing.Client.Controllers
{
    /// <summary>
    /// Controller for managing orders in the Event Sourcing application.
    /// </summary>
    [Route("api/[controller]")]
    public class OrderController : Controller
    {
        /// <summary>
        /// Session for interacting with the Marten document store.
        /// </summary>
        readonly IDocumentSession DocumentSession;

        /// <summary>
        /// Provides the current date and time, used for event timestamps.
        /// </summary>
        readonly TimeProvider DateTimeProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderController"/> class.
        /// </summary>
        /// <param name="documentSession">Marten's <see cref="IDocumentSession"/>.</param>
        /// <param name="timeProvider">The <see cref="TimeProvider"/>.</param>
        public OrderController(IDocumentSession documentSession, TimeProvider timeProvider)
        {
            DocumentSession = documentSession;
            DateTimeProvider = timeProvider;
        }

        [HttpPost]
        public async Task<OrderModel?> CreateOrderAsync([FromBody] CreateOrderCommand command, CancellationToken cancellationToken)
        {
            var id = CombGuidIdGeneration.NewGuid();

            var @event = new OrderCreatedEvent(id, command.UserId, DateTimeProvider.GetUtcNow().Date);

            DocumentSession.Events.StartStream<OrderModel>(id, @event);
            await DocumentSession.SaveChangesAsync(token: cancellationToken);

            return await DocumentSession.Events
                .AggregateStreamAsync<OrderModel>(id, token: cancellationToken);
        }
    }
}
