using NetCoreSamples.Messaging.Lib.Contracts;

namespace NetCoreSamples.Messaging.Lib.Hubs.Interfaces
{
    /// <summary>
    /// Interface for the TaskHub
    /// </summary>
    public interface ITaskHub
    {
        /// <summary>
        /// Broadcasts a task completed message to the requestor
        /// </summary>
        /// <param name="contract">The <see cref="TaskCompletedContract"/></param>
        /// <returns></returns>
        Task TaskCompleted(TaskCompletedContract contract);

        /// <summary>
        /// Broadcasts a task requested message to all clients
        /// </summary>
        /// <param name="contract">The <see cref="TaskRequestedContract"/></param>
        /// <returns></returns>
        Task TaskRequested(TaskRequestedContract contract);
    }
}
