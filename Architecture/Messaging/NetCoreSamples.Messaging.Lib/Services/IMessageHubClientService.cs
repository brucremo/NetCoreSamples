using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Hosting;

namespace NetCoreSamples.Messaging.Lib.Services
{
    /// <summary>
    /// A Service for managing SignalR Hub connections
    /// </summary>
    public interface IMessageHubClientService : IHostedService
    {
        /// <summary>
        /// Gets the SignalR HubConnection
        /// </summary>
        /// <returns>The <see cref="HubConnection"/></returns>
        public HubConnection Connection { get; }
    }
}
