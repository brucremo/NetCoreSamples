using Microsoft.Extensions.Hosting;

namespace NetCoreSamples.Broker.Lib.Services.Nats
{
    /// <summary>
    /// Represents a NATS broker service.
    /// </summary>
    public interface INatsBrokerService : IPubSubBrokerService, IStreamBrokerService, IHostedService
    {
    }
}
