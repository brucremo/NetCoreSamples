using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using NetCoreSamples.Messaging.Lib.Resiliency;
using Polly.Retry;
using Serilog;

namespace NetCoreSamples.Messaging.Lib.Services
{
    /// <summary>
    /// MessageHub client service
    /// </summary>
    public class MessageHubClientService : IMessageHubClientService
    {
        /// <summary>
        /// The hub connection
        /// </summary>
        public HubConnection Connection { get; private set; }

        /// <summary>
        /// MessageHub client options
        /// </summary>
        readonly MessageHubClientOptions Options;

        /// <summary>
        /// Retry policy
        /// </summary>
        readonly AsyncRetryPolicy RetryPolicy;

        public MessageHubClientService(IOptions<MessageHubClientOptions> options, IHostEnvironment environment)
        {
            Options = options.Value;
            Connection = new HubConnectionBuilder()
                .WithUrl($"{Options.EndpointUrl}/{Options.DefaultHub}", options => {
                    options.UseDefaultCredentials = true;
                    options.HttpMessageHandlerFactory = (msg) =>
                    {
                        if (environment.IsDevelopment() && msg is HttpClientHandler clientHandler)
                        {
                            // bypass SSL certificate in development environment
                            clientHandler.ServerCertificateCustomValidationCallback +=
                                (sender, certificate, chain, sslPolicyErrors) => { return true; };
                        }

                        return msg;
                    };
                })
                .WithAutomaticReconnect()
                .Build();

            Connection.Closed += async (ex) =>
            {
                Log.Logger.Warning($"Connection closed: {ex?.Message}");
                await Connection.StartAsync();
            };

            Connection.Reconnecting += (ex) =>
            {
                Log.Logger.Warning($"Reconnecting: {ex?.Message}");
                return Task.CompletedTask;
            };

            Connection.Reconnected += (connectionId) =>
            {
                Log.Logger.Information($"Reconnected to the hub. Connection ID: {connectionId}");
                return Task.CompletedTask;
            };

            RetryPolicy = RetryPolicies.DefaultAsyncExceptionRetryPolicy<Exception>(Options.RetrySleepDurationInSeconds, Options.OnConnectionFailRetryCount);
        }

        /// <inheritdoc/>
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await TryStartConnection();
        }

        /// <inheritdoc/>
        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await Connection.StopAsync();
        }

        async Task TryStartConnection()
        {
            await RetryPolicy.ExecuteAsync(async () =>
            {
                try
                {
                    await Connection.StartAsync();

                    Log.Logger.Information("Connected to the hub successfully.");
                }
                catch (Exception ex)
                {
                    Log.Logger.Error($"Failed to connect to the hub", ex);
                    throw;
                }
            });
        }
    }
}
