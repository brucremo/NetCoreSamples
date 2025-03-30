using NetCoreSamples.Broker.Lib;
using NetCoreSamples.Broker.Worker.SampleContracts;
using NetCoreSamples.Worker.Lib;
using Serilog;

namespace NetCoreSamples.Broker.Worker.Workers
{
    /// <summary>
    /// Represents a worker that creates queues and tasks in the broker service.
    /// </summary>
    public class QueueProducer : IWorker
    {
        /// <summary>
        /// The queue broker service
        /// </summary>
        readonly IQueueBrokerService broker;

        public QueueProducer(IQueueBrokerService brokerService)
        {
            broker = brokerService;
        }

        public Task Run(CancellationToken cancellationToken = default)
        {
            _ = new Timer(async _ =>
                await EnqueueRequestAsync(cancellationToken),
                null,
                TimeSpan.FromSeconds(2),
                TimeSpan.FromSeconds(2));

            return Task.CompletedTask;
        }

        async Task EnqueueRequestAsync(CancellationToken cancellationToken = default)
        {
            var subjectName = "queue-subject1";
            var clientIdentifier = broker.GetClientIdentifier();

            var message = new Message
            {
                Id = Random.Shared.Next(),
                Text = $"Workers on {subjectName} - Here's a message!"
            };

            var response = await broker.EnqueueRequestAsync<Message, Message>(
                subjectName,
                message,
                cancellationToken);

            if (response is null)
            {
                Log.Logger.Warning($"No response for Message {message.Id}");
                return;
            }

            Log.Logger.Information($"Response for Message {message.Id} -> ID: {response.Id} - {response.Text}");
        }
    }
}
