namespace NetCoreSamples.Broker.Lib.Services
{
    public partial class NatsBrokerService
    {
        /// <inheritdoc/>
        public ValueTask ConsumeAsync(string streamName, Func<byte[], CancellationToken, ValueTask> handler, int batchSize = 100, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public ValueTask CreateStreamAsync(string streamName, string subject)
        {
            throw new NotImplementedException();
        }
    }
}
