namespace NetCoreSamples.Broker.Lib.Options
{
    /// <summary>
    /// Options for broker services.
    /// </summary>
    public class BrokerServiceOptions
    {
        /// <summary>
        /// Broker server URL.
        /// </summary>
        public required string ServerUrl { get; set; }

        /// <summary>
        /// The user name.
        /// </summary>
        public required string Username { get; set; }

        /// <summary>
        /// The password.
        /// </summary>
        public required string Password { get; set; }
    }
}
