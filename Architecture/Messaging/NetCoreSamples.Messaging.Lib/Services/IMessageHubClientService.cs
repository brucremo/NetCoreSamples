using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Hosting;

namespace NetCoreSamples.Messaging.Lib.Services
{
    public interface IMessageHubClientService : IHostedService
    {
        /// <summary>
        /// Gets the SignalR HubConnection
        /// </summary>
        /// <returns>The <see cref="HubConnection"/></returns>
        public HubConnection Connection { get; }
    }
}
