namespace NetCoreSamples.Broker.Worker.SampleContracts
{
    /// <summary>
    /// A sample class representing a message.
    /// </summary>
    internal class Message
    {
        /// <summary>
        /// The message identifier.
        /// </summary>
        public required int Id { get; set; }

        /// <summary>
        /// The message text.
        /// </summary>
        public required string Text { get; set; }
    }
}
