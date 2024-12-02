using Microsoft.AspNetCore.SignalR;
using NetCoreSamples.Messaging.Lib.Contracts;
using NetCoreSamples.Messaging.Lib.Hubs.Interfaces;

namespace NetCoreSamples.Messaging.Lib.Hubs
{
    public class TaskHub : Hub<ITaskHub>
    {
        /// <summary>
        /// Broadcasts a task completed message to the requestor
        /// </summary>
        /// <param name="contract">The <see cref="TaskCompletedContract"/></param>
        /// <returns></returns>
        public async Task BroadcastTaskCompleted(TaskCompletedContract taskCompletion)
        {
            await this.Clients.Client(taskCompletion.RequestorConnectionId)
                .TaskCompleted(taskCompletion);
        }
    }
}
