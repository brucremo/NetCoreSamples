using NetCoreSamples.Logging.Lib;
using NetCoreSamples.Messaging.Lib.Hubs;

namespace NetCoreSamples.Messaging.SignalR
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            SerilogSetup.ConfigureSerilog(builder.Configuration);

            builder.Services.AddControllers();
            builder.Services.AddOpenApi();
            builder.Services.AddSignalR();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapHub<TaskHub>("/tasks");

            app.MapControllers();

            app.Run();
        }
    }
}
