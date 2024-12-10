using Microsoft.AspNetCore.SignalR;
using NetCoreSamples.Messaging.Lib.Contracts;
using NetCoreSamples.Messaging.Lib.Hubs.Interfaces;

namespace NetCoreSamples.Messaging.Lib.Hubs
{
    /// <summary>
    /// SignalR Hub for task related messages
    /// </summary>
    public class TaskHub : Hub<ITaskHub>
    {
        /// <summary>
        /// Broadcasts a task requested message to all clients
        /// </summary>
        /// <param name="contract">The <see cref="TaskCompletedContract"/></param>
        /// <returns></returns>
        public async Task BroadcastTaskRequested(TaskRequestedContract contract)
        {
            await Clients.All.TaskRequested(contract);
        }

        /// <summary>
        /// Broadcasts a task completed message to the requestor
        /// </summary>
        /// <param name="contract">The <see cref="TaskCompletedContract"/></param>
        /// <returns></returns>
        public async Task BroadcastTaskCompleted(TaskCompletedContract contract)
        {
            await Clients.Client(contract.RequestorConnectionId)
                .TaskCompleted(contract);
        }
    }
}
