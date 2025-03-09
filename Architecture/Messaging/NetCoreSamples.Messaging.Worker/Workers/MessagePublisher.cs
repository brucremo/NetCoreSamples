using Microsoft.AspNetCore.SignalR.Client;
using NetCoreSamples.Messaging.Lib.Contracts;
using NetCoreSamples.Messaging.Lib.Resiliency;
using NetCoreSamples.Messaging.Lib.Services;
using NetCoreSamples.Worker.Lib;
using Serilog;

namespace NetCoreSamples.Messaging.Worker
{
    /// <summary>
    /// Worker that publishes messages to the message hub and processes the task confirmation messages received back from the MessageHub
    /// </summary>
    public class MessagePublisher : IWorker
    {
        /// <summary>
        /// The message hub client service
        /// </summary>
        readonly IMessageHubClientService messageHubClient;

        /// <summary>
        /// The unpublished task started contracts
        /// </summary>
        readonly Dictionary<Guid, TaskRequestedContract> unpublishedTaskStartedContracts = new();

        public MessagePublisher(IMessageHubClientService messageHubClientService)
        {
            messageHubClient = messageHubClientService;
        }

        /// <inheritdoc/>
        public async Task Run(CancellationToken cancellationToken = default)
        {
            await SetupHubConnection();

            while (!cancellationToken.IsCancellationRequested)
            {
                await RetryPolicies.DefaultAsyncExceptionRetryPolicy<Exception>(5)
                    .ExecuteAsync(async () =>
                    {
                        AddContractTaskToPublish();

                        try
                        {
                            await PublishStartTaskContracts();
                        }
                        catch (Exception ex)
                        {
                            Log.Logger.Error("Error publishing contract: ", ex);
                        }
                    });

                await Task.Delay(2000, cancellationToken);
            }

            await messageHubClient.Connection.StopAsync();
        }

        async Task PublishStartTaskContracts()
        {
            foreach (var (key, contract) in unpublishedTaskStartedContracts)
            {
                Log.Logger.Information("Publishing task request...");

                await messageHubClient.Connection
                    .SendAsync("BroadcastTaskRequested", contract);

                unpublishedTaskStartedContracts.Remove(key);

                Log.Logger.Information($"Published: {contract.ToString()}");
            }
        }

        async Task SetupHubConnection()
        {
            await RetryPolicies.DefaultAsyncExceptionRetryPolicy<Exception>(2)
                .ExecuteAsync(() =>
                {
                    messageHubClient.Connection.On<TaskCompletedContract>("TaskCompleted", (message) =>
                    {
                        Log.Logger.Information($"Received TaskCompleted message - ID: {message.TaskId} | Title: {message.Title} | Message: {message.Message}");
                    });

                    return Task.CompletedTask;
                });
        }

        void AddContractTaskToPublish()
        {
            var contractGuid = Guid.NewGuid();

            unpublishedTaskStartedContracts.Add(contractGuid,
                new TaskRequestedContract
                {
                    RequestorConnectionId = messageHubClient.Connection.ConnectionId ?? 
                        throw new InvalidOperationException("Cannot publish a task without connection ID!"),
                    TaskId = contractGuid,
                    Content = "Task requested! Someone please process this!"
                });
        }
    }
}
