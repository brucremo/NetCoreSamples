using Microsoft.Extensions.Hosting;

namespace NetCoreSamples.Broker.Lib
{
    /// <summary>
    /// Represents a broker service capable of publishing and subscribing to messages.
    /// </summary>
    public interface IPubSubBrokerService : IDisposable, IAsyncDisposable
    {
        /// <summary>
        /// Publishes a message to the broker.
        /// </summary>
        /// <typeparam name="T">The type of the message data to be published.</typeparam>
        /// <param name="subject">The subject.</param>
        /// <param name="data">The message data.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A <see cref="ValueTask"/> representing the asynchronous operation.</returns>
        ValueTask PublishAsync<T>(string subject, T data, CancellationToken cancellationToken = default);

        /// <summary>
        /// Subscribes to a message from the broker.
        /// </summary>
        /// <typeparam name="T">The type of the message data to be subscribed.</typeparam>
        /// <param name="subject">The subject.</param>
        /// <param name="handler">The handler function to excute when a message is received.</param>
        /// <param name="cancellationToken"></param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A <see cref="ValueTask"/> representing the asynchronous operation.</returns>
        ValueTask SubscribeAsync<T>(string subject, Func<T?, CancellationToken, ValueTask> handler, CancellationToken cancellationToken = default);
    }
}
