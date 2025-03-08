using Microsoft.Extensions.Options;
using NATS.Client.Core;
using NATS.Net;
using NetCoreSamples.Broker.Lib.Options;
using NetCoreSamples.Broker.Lib.Services.Nats;

namespace NetCoreSamples.Broker.Lib.Services
{
    /// <summary>
    /// NATS broker service implementation.
    /// </summary>
    public partial class NatsBrokerService : INatsBrokerService
    {
        /// <summary>
        /// The broker service options.
        /// </summary>
        readonly BrokerServiceOptions options;

        /// <summary>
        /// The NATS client.
        /// </summary>
        INatsClient? NatsClient { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="NatsBrokerService"/> class.
        /// </summary>
        public NatsBrokerService(IOptions<BrokerServiceOptions> options)
        {
            this.options = options.Value;
        }

        /// <inheritdoc />
        public void Dispose()
        {
            DisposeAsync().AsTask().Wait();
        }

        /// <inheritdoc />
        public async ValueTask DisposeAsync()
        {
            if (NatsClient is not null)
            {
                await NatsClient.DisposeAsync();
            }
        }

        /// <inheritdoc />
        public Task StartAsync(CancellationToken cancellationToken)
        {
            NatsClient = GetNatsClient();
            return Task.CompletedTask;
        }

        /// <inheritdoc />
        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await DisposeAsync();
        }

        INatsClient GetNatsClient()
        {
            var natsOptions = new NatsOpts
            {
                Url = options.ServerUrl,
                AuthOpts = NatsAuthOpts.Default with
                {
                    Username = options.Username,
                    Password = options.Password,
                }
            };

            return new NatsClient(natsOptions);
        }

        void ThrowIfNatsClientIsNotInitialized()
        {
            if (NatsClient is null)
            {
                throw new InvalidOperationException("NATS client is not initialized.");
            }
        }
    }
}
