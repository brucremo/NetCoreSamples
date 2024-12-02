namespace NetCoreSamples.Messaging.Lib.Contracts
{
    /// <summary>
    /// The base contract
    /// </summary>
    public abstract class BaseContract<T> where T : class
    {
        /// <summary>
        /// The requestor connection id
        /// </summary>
        public required string RequestorConnectionId { get; set; }

        /// <summary>
        /// The task id
        /// </summary>
        public required Guid TaskId { get; set; }

        /// <summary>
        /// Gets the contract data for storage.
        /// </summary>
        /// <returns>The contract</returns>
        public Task<T?> GetContract()
        {
            return Task.FromResult(this as T);
        }
    }
}
