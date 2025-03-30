using NetCoreSamples.Broker.Lib;
using NetCoreSamples.Broker.Worker.SampleContracts;
using NetCoreSamples.Worker.Lib;
using Serilog;

namespace NetCoreSamples.Broker.Worker.Workers
{
    /// <summary>
    /// Represents a worker that consumes message from a queue in the broker service.
    /// </summary>
    public class QueueConsumer : IWorker
    {
        /// <summary>
        /// The queue broker service
        /// </summary>
        readonly IQueueBrokerService broker;

        public QueueConsumer(IQueueBrokerService brokerService)
        {
            broker = brokerService;
        }

        public Task Run(CancellationToken cancellationToken = default)
        {
            _ = new Timer(async _ =>
                await ConsumeQueueRequestAsync(cancellationToken),
                null,
                TimeSpan.FromSeconds(1),
                TimeSpan.FromSeconds(1));

            return Task.CompletedTask;
        }

        async Task ConsumeQueueRequestAsync(CancellationToken cancellationToken = default)
        {
            var queueName = "queue1";
            var clientIdentifier = broker.GetClientIdentifier();

            var validResponse = new Message 
            { 
                Id = Random.Shared.Next(), 
                Text = $"{clientIdentifier} - Here's your response " 
            };

            var invalidResponse = new Message
            {
                Id = Random.Shared.Next(),
                Text = $"{clientIdentifier} - Wth I got invalid data from you!"
            };

            await broker.SubscribeToQueueAsync<Message, Message>(
                "queue-subject1",
                queueName,
                (data, cancellationToken) =>
                {
                    if (data is null)
                    {
                        Log.Logger.Warning("No data received.");
                        return Task.FromResult(invalidResponse);
                    }

                    Log.Logger.Information($"Received message from subscription -> {data.Id} - {data.Text}");

                    validResponse.Text += $"- {data.Id}";

                    return Task.FromResult(validResponse);
                },
                cancellationToken);
        }
    }
}
