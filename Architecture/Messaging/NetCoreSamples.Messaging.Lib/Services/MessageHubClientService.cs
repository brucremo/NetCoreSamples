using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Polly;
using Polly.Retry;
using Serilog;

namespace NetCoreSamples.Messaging.Lib.Services
{
    /// <summary>
    /// MessageHub client service
    /// </summary>
    public class MessageHubClientService : IHostedService
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
            this.Options = options.Value;
            this.Connection = new HubConnectionBuilder()
                .WithUrl($"{this.Options.EndpointUrl}/{this.Options.DefaultHub}", options => {
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

            RetryPolicy = Policy
                .Handle<Exception>()
                .WaitAndRetryAsync(
                    Options.OnConnectionFailRetryCount,
                    retryAttempt => TimeSpan.FromSeconds(Options.RetrySleepDurationInSeconds),
                    (exception, timeSpan, retryCount, context) =>
                    {
                        Log.Logger.Warning($"Connection attempt {retryCount} failed. Retrying in {timeSpan.TotalSeconds} seconds: {exception.Message}");
                    });
        }

        /// <inheritdoc/>
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await RetryPolicy.ExecuteAsync(async () =>
            {
                await Connection.StartAsync(cancellationToken);
                Log.Logger.Information("Connected to the hub successfully.");
            });
        }

        /// <inheritdoc/>
        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await Connection.StopAsync();
        }
    }
}
