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

        /// <summary>
        /// The timer for the worker
        /// </summary>
        Timer? Timer { get; set; }

        public Publisher(IPubSubBrokerService brokerService)
        {
            broker = brokerService;
        }

        public Task Run(CancellationToken cancellationToken = default)
        {
            Timer = new Timer(async _ =>
                await PublishSomethingAsync(cancellationToken),
                null,
                TimeSpan.FromSeconds(30),
                TimeSpan.FromSeconds(30));

            return Task.CompletedTask;
        }

        async Task PublishSomethingAsync(CancellationToken cancellationToken = default)
        {
            await broker.PublishAsync(
                "test", 
                new { Message = "Hello, World!" }, 
                cancellationToken);
        }
    }
}
