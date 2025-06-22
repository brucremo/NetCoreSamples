using NetCoreSamples.Domain.Extensions;
using NetCoreSamples.Logging.Lib;
using NetCoreSamples.EventSourcing.Lib.Extensions;
using NetCoreSamples.Domain.Projections;

namespace NetCoreSamples.EventSourcing.Client
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            SerilogSetup.ConfigureSerilog(builder.Configuration);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.ConfigureDbContext();
            builder.ConfigureMarten(options =>
            {
                var connectionString = builder.Configuration.GetConnectionString("EventStoreConnection") 
                    ?? throw new InvalidOperationException("Connection string 'EventStoreConnection' not found.");

                options.Connection(connectionString);

                options.DisableNpgsqlLogging = true;

                OrderProjection.RegisterOptions(options);
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
