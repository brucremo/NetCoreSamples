﻿namespace NetCoreSamples.Broker.Lib.Services
{
    public partial class NatsBrokerService
    {
        /// <inheritdoc/>
        public async ValueTask PublishAsync<T>(string subject, T data, CancellationToken cancellationToken = default)
        {
            ThrowIfNatsClientIsNotInitialized();

            await NatsClient!.PublishAsync(subject, data, cancellationToken: cancellationToken);
        }

        /// <inheritdoc/>
        public async ValueTask SubscribeAsync<T>(string subject, Func<T?, CancellationToken, ValueTask> handler, CancellationToken cancellationToken = default)
        {
            ThrowIfNatsClientIsNotInitialized();

            await foreach (var message in NatsClient!.SubscribeAsync<T>(subject, cancellationToken: cancellationToken))
            {
                await handler(message.Data, cancellationToken);
            }
        }
    }
}
