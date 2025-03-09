namespace NetCoreSamples.Caching.Lib.Options
{
    /// <summary>
    /// Represents a Redis endpoint for configuration
    /// </summary>
    public class RedisEndpoint
    {
        /// <summary>
        /// The Redis server host name or IP address
        /// </summary>
        public required string Host { get; set; }

        /// <summary>
        /// The Redis port
        /// </summary>
        public required int Port { get; set; }
    }
}
