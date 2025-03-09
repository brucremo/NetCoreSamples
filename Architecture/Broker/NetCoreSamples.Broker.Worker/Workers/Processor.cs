using NetCoreSamples.Broker.Lib;
using NetCoreSamples.Worker.Lib;

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

        public Task Run(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
