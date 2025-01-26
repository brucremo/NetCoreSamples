using NetCoreSamples.Serialization.Lib;

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

        /// <inheritdoc/>
        public override string ToString()
        {
            return Serializer.SerializeJson(this as T);
        }
    }
}
