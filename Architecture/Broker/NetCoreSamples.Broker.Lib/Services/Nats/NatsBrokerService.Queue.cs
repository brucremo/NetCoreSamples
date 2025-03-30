namespace NetCoreSamples.Broker.Lib.Services.Nats
{
    public partial class NatsBrokerService
    {
        /// <inheritdoc/>
        public Guid GetClientIdentifier() => options.ClientIdentifier;

        /// <inheritdoc/>
        public async Task<T?> EnqueueRequestAsync<Y, T>(string subject, Y data, CancellationToken cancellationToken = default)
        {
            var responseMessage = await NatsClient.RequestAsync<Y, T>(subject, data, cancellationToken: cancellationToken);

            return responseMessage.Data;
        }

        /// <inheritdoc/>
        public async ValueTask SubscribeToQueueAsync<Y, T>(string subject, string queueName, Func<T?, CancellationToken, Task<Y>> handler, CancellationToken cancellationToken = default)
        {
            await foreach (var message in NatsClient.SubscribeAsync<T>(subject, queueGroup: queueName, cancellationToken: cancellationToken))
            {
                var replyData = await handler(message.Data, cancellationToken);
                await message.ReplyAsync(replyData, cancellationToken: cancellationToken);
            }
        }
    }
}
