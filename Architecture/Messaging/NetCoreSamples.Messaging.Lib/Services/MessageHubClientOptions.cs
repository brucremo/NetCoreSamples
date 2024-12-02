namespace NetCoreSamples.Messaging.Lib.Services
{
    /// <summary>
    /// MessageHub client options
    /// </summary>
    public class MessageHubClientOptions
    {
        /// <summary>
        /// Endpoint URL
        /// </summary>
        public required string EndpointUrl { get; set; }

        /// <summary>
        /// Default hub
        /// </summary>
        public string? DefaultHub { get; set; }

        /// <summary>
        /// On connection fail retry count
        /// </summary>
        public int OnConnectionFailRetryCount { get; set; } = 10;

        /// <summary>
        /// Retry sleep duration in seconds
        /// </summary>
        public int RetrySleepDurationInSeconds { get; set; } = 10;
    }
}
