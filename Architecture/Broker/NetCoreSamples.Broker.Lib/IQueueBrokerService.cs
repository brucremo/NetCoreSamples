namespace NetCoreSamples.Broker.Lib
{
    /// <summary>
    /// Represents a broker service capable of operating as a queue.
    /// </summary>
    public interface IQueueBrokerService : IDisposable, IAsyncDisposable
    {
        /// <summary>
        /// Gets broker service client identifier.
        /// </summary>
        /// <returns>A <see cref="Guid"/> representing the unique client identifier.</returns>
        Guid GetClientIdentifier();

        /// <summary>
        /// Enqueues a request to the broker queue.
        /// </summary>
        /// <typeparam name="T">The type of the message data to be subscribed.</typeparam>
        /// <typeparam name="Y">The type of data to be sent to the request processor.</typeparam>
        /// <param name="subject">The subject.</param>
        /// <param name="data">The request data.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The response from the processor.</returns>
        Task<T?> EnqueueRequestAsync<Y, T>(string subject, Y data, CancellationToken cancellationToken = default);

        /// <summary>
        /// Subscribes to a message from the broker queue.
        /// </summary>
        /// <typeparam name="T">The type of the message data to be subscribed.</typeparam>
        /// <typeparam name="Y">The type of data to be sent to back to the requestor.</typeparam>
        /// <param name="subject">The subject.</param>
        /// <param name="queueName">The name of the queue.</param>
        /// <param name="handler">The handler function to excute when a message is received.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A <see cref="ValueTask"/> representing the asynchronous operation.</returns>
        ValueTask SubscribeToQueueAsync<Y, T>(string subject, string queueName, Func<T?, CancellationToken, Task<Y>> handler, CancellationToken cancellationToken = default);
    }
}
