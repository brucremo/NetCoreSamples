using Microsoft.AspNetCore.SignalR.Client;
using NetCoreSamples.Messaging.Lib.Contracts;
using NetCoreSamples.Messaging.Lib.Services;
using NetCoreSamples.Worker.Lib;
using Serilog;

namespace NetCoreSamples.Messaging.Workers
{
    public class MessagePublisherWorker : IWorker
    {
        private IMessageHubClientService MessageHubClient { get; set; }

        public MessagePublisherWorker(IMessageHubClientService messageHubClientService)
        {
            MessageHubClient = messageHubClientService;
        }

        public async Task Run()
        {
            Log.Logger.Information("Publishing start task message after delay...");
            await Task.Delay(5000);
            await MessageHubClient.GetHubConnection().SendAsync(
                "BroadcastTaskCompleted",
                new TaskStartedContract
                {
                    RequestorConnectionId = MessageHubClient.GetHubConnection().ConnectionId,
                    TaskId = Guid.NewGuid(),
                });
        }
    }
}
