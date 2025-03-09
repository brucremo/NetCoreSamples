namespace NetCoreSamples.Broker.Lib
{
    /// <summary>
    /// Stream broker service interface.
    /// </summary>
    public interface IStreamBrokerService : IDisposable, IAsyncDisposable
    {
        /// <summary>
        /// Create a stream with the given name and subject.
        /// </summary>
        /// <param name="streamName">The stream name.</param>
        /// <param name="subject">The subject.</param>
        /// <returns>A <see cref="ValueTask"/> representing the asynchronous operation.</returns>
        ValueTask CreateStreamAsync(string streamName, string subject);

        /// <summary>
        /// Consumes messages from the given stream in batches.
        /// </summary>
        /// <param name="streamName">The stream name.</param>
        /// <param name="handler">The handler function to excute when a message is received.</param>
        /// <param name="batchSize">The batch size.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A <see cref="ValueTask"/> representing the asynchronous operation.</returns>
        ValueTask ConsumeAsync(string streamName, Func<byte[], CancellationToken, ValueTask> handler, int batchSize = 100, CancellationToken cancellationToken = default);
    }
}
