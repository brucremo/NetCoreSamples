using NetCoreSamples.Messaging.Lib.Contracts;

namespace NetCoreSamples.Messaging.Lib.Hubs.Interfaces
{
    public interface ITaskHub
    {
        /// <summary>
        /// Broadcasts a task completed message to the requestor
        /// </summary>
        /// <param name="contract">The <see cref="TaskCompletedContract"/></param>
        /// <returns></returns>
        Task TaskCompleted(TaskCompletedContract contract);

        /// <summary>
        /// Broadcasts a task started message to the requestor
        /// </summary>
        /// <param name="contract">The <see cref="TaskCompletedContract"/></param>
        /// <returns></returns>
        Task TaskStarted(TaskStartedContract contract);
    }
}
