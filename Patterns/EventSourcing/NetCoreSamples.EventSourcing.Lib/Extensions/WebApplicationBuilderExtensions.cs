using Marten;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using JasperFx;

namespace NetCoreSamples.EventSourcing.Lib.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="WebApplicationBuilder"/>.
    /// </summary>
    public static class WebApplicationBuilderExtensions
    {
        /// <summary>
        /// Configures Marten for the web application.
        /// </summary>
        /// <param name="builder">The <see cref="WebApplicationBuilder"/>.</param>
        /// <param name="storeConfigurationAction">The custom action containing used to configure the store.</param>
        public static void ConfigureMarten(this WebApplicationBuilder builder, Action<StoreOptions> storeConfigurationAction)
        {
            builder.Services.AddMarten(options =>
            {
                if (builder.Environment.IsDevelopment())
                {
                    options.AutoCreateSchemaObjects = AutoCreate.All;
                }

                options.UseSystemTextJsonForSerialization();

                storeConfigurationAction.Invoke(options);
            })
                .UseLightweightSessions();
        }
    }
}
