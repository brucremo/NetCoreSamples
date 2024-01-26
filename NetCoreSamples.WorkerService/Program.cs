using NetCoreSamples.Logging.Lib;
using NetCoreSamples.Worker.Lib.Extensions;

namespace NetCoreSamples.WorkerService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = Host.CreateApplicationBuilder(args);

            SerilogSetup.ConfigureSerilog(builder.Configuration);

            builder.ConfigureWorkers();

            builder.Services.AddHostedService<ServiceWorker>();

            var host = builder.Build();
            host.Run();
        }
    }
}