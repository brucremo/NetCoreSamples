using NetCoreSamples.Worker.Lib;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using NetCoreSamples.Worker.SampleWorkers.One;
using NetCoreSamples.Worker.SampleWorkers.Two;

namespace NetCoreSamples.Worker
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WorkerApplication.CreateBuilder<WorkerOptions>(args)
                .WithAssembly(Assembly.Load("NetCoreSamples.Worker.SampleWorkers"))
                .UseSerilog();

            builder.ConfigureWorker("WorkerOne", (services, configuration) => 
                services.Configure<WorkerOneOptions>(configuration.GetSection(nameof(WorkerOneOptions))));

            builder.ConfigureWorker("WorkerTwo", (services, configuration) => 
                services.Configure<WorkerTwoOptions>(configuration.GetSection(nameof(WorkerTwoOptions))));

            var app = builder.Build();
            await app.Run();
        }
    }
}
