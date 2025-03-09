using NetCoreSamples.Broker.Lib;
using NetCoreSamples.Worker.Lib;
using Serilog;

namespace NetCoreSamples.Broker.Worker.Workers
{
    /// <summary>
    /// Represents a worker that processes requests from the broker service.
    /// </summary>
    public class Processor : IWorker
    {
        /// <summary>
        /// The message hub client service
        /// </summary>
        readonly IPubSubBrokerService broker;

        public Processor(IPubSubBrokerService brokerService)
        {
            broker = brokerService;
        }

        /// <inheritdoc />
        public async Task Run(CancellationToken cancellationToken = default)
        {
            var subscribeTask1 = broker.SubscribeAsync<object>(
                "subject1",
                (data, cancellationToken) =>
                {
                    Log.Logger.Information($"Received message from subscription 1: {data}");
                    return default;
                },
                cancellationToken).AsTask();

            var subscribeTask2 = broker.SubscribeAsync<object>(
                "subject2",
                (data, cancellationToken) =>
                {
                    Log.Logger.Information($"Received message from subscription 2: {data}");
                    return default;
                },
                cancellationToken).AsTask();

            await Task.WhenAll(subscribeTask1, subscribeTask2);
        }
    }
}
