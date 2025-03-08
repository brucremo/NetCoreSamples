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

        public Task Run(CancellationToken? cancellationToken = null)
        {
            throw new NotImplementedException();
        }
    }
}
