using Microsoft.AspNetCore.SignalR.Client;
using NetCoreSamples.Messaging.Lib.Contracts;
using NetCoreSamples.Messaging.Lib.Resiliency;
using NetCoreSamples.Messaging.Lib.Services;
using NetCoreSamples.Worker.Lib;
using Serilog;

namespace NetCoreSamples.Messaging.Worker
{
    public class MessageProcessor : IWorker
    {
        readonly IMessageHubClientService messageHubClient;

        public MessageProcessor(IMessageHubClientService messageHubClientService)
        {
            messageHubClient = messageHubClientService;
        }

        /// <inheritdoc/>
        public async Task Run(CancellationToken? cancellationToken = null)
        {
            await SetupHubConnection();

            // Keep the connection alive until cancellation is requested
            while (!cancellationToken.GetValueOrDefault().IsCancellationRequested)
            {
                await messageHubClient.Connection.StopAsync();
                return;
            }
        }

        async Task SetupHubConnection()
        {
            await RetryPolicies.DefaultAsyncExceptionRetryPolicy<Exception>(2)
                .ExecuteAsync(() =>
                {
                    messageHubClient.Connection.On<TaskRequestedContract>("TaskRequested", async (message) =>
                    {
                        Log.Logger.Information($"Received message - ID: {message.TaskId} | Requestor: {message.RequestorConnectionId} | Content: {message.Content}");
                        await PublishTaskCompletedContract(message.RequestorConnectionId, message.TaskId);
                    });

                    return Task.CompletedTask;
                });
        }

        async Task PublishTaskCompletedContract(string requestorConnectionId, Guid taskId)
        {
            var contract = new TaskCompletedContract
            {
                TaskId = taskId,
                RequestorConnectionId = requestorConnectionId,
                Success = true,
                Message = "Task completed successfully!",
                Title = "Task Completed"
            };

            Log.Logger.Information("Publishing task completion notification...");

            await messageHubClient.Connection
                .SendAsync("BroadcastTaskCompleted", contract);

            Log.Logger.Information($"Published: {contract.ToString()}");
        }
    }
}
