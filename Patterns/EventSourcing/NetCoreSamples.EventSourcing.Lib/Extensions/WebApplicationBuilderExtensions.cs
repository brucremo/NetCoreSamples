using Marten;
using Weasel.Core;
using JasperFx.CodeGeneration;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace NetCoreSamples.EventSourcing.Lib.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="WebApplicationBuilder"/>.
    /// </summary>
    public static class WebApplicationBuilderExtensions
    {
        /// <summary>
        /// Configure Marten for the application.
        /// </summary>
        /// <param name="builder">The <see cref="WebApplicationBuilder"/></param>
        public static void ConfigureMarten(this WebApplicationBuilder builder)
        {
            builder.Services.AddMarten(options =>
            {
                // If we're running in development mode, let Marten just take care
                // of all necessary schema building and patching behind the scenes
                if (builder.Environment.IsDevelopment())
                {
                    options.AutoCreateSchemaObjects = AutoCreate.All;
                }

                // Establish the connection string to your Marten database
                options.Connection(builder.Configuration.GetMartenConnectionString());

                options.GeneratedCodeMode = TypeLoadMode.Auto;

                // Specify that we want to use STJ as our serializer
                options.UseSystemTextJsonForSerialization();

                options.DisableNpgsqlLogging = true;
            })
                .UseLightweightSessions();
        }

        /// <summary>
        /// Get the connection string for Marten from the configuration.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <returns>The connection string for the Marten database.</returns>
        static string GetMartenConnectionString(this IConfiguration configuration)
        {
            var userName = configuration.GetValue<string>("POSTGRES_USER");
            var password = configuration.GetValue<string>("POSTGRES_PASSWORD");
            var additionalOptions = configuration.GetValue<string>("POSTGRES_OPTIONS") ?? "Command Timeout=5";

            var databaseName = configuration.GetValue<string>("DATABASE_NAME") ?? "postgres";
            var host = configuration.GetValue<string>("DATABASE_HOST");

            return $"Host={host};Port=5432;Username={userName};Password={password};Database={databaseName};{additionalOptions}";
        }
    }
}
