using NetCoreSamples.Broker.Lib;
using NetCoreSamples.Worker.Lib;

namespace NetCoreSamples.Broker.Worker.Workers
{
    /// <summary>
    /// Represents a worker that generates requests to the broker service.
    /// </summary>
    public class Publisher : IWorker
    {
        /// <summary>
        /// The message hub client service
        /// </summary>
        readonly IPubSubBrokerService broker;

        public Publisher(IPubSubBrokerService brokerService)
        {
            broker = brokerService;
        }

        /// <inheritdoc />
        public Task Run(CancellationToken cancellationToken = default)
        {
            _ = new Timer(async _ =>
                await broker.PublishAsync("subject1", new { Message = "This is a pub to subject1!" }, cancellationToken),
                null,
                TimeSpan.FromSeconds(2),
                TimeSpan.FromSeconds(2));

            _ = new Timer(async _ =>
                await broker.PublishAsync("subject2", new { Message = "This is a pub to subject2!", Data = "The data here is different from sub1" }, cancellationToken),
                null,
                TimeSpan.FromSeconds(10),
                TimeSpan.FromSeconds(10));

            return Task.CompletedTask;
        }
    }
}
