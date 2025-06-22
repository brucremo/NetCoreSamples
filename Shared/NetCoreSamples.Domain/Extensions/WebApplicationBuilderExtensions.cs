using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetCoreSamples.Domain.Entities;

namespace NetCoreSamples.Domain.Extensions
{
    /// <summary>
    /// Extension methods for configuring domain services for a WebApplicationBuilder.
    /// </summary>
    public static class WebApplicationBuilderExtensions
    {
        /// <summary>
        /// Configures the DbContext for the application.
        /// </summary>
        /// <param name="builder">The <see cref="WebApplicationBuilder"/>.</param>
        public static void ConfigureDbContext(this WebApplicationBuilder builder)
        {
            var connectionString = builder.Configuration
                .GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            builder.Services.AddDbContext<SampleDbContext>(options => options.UseSqlServer(connectionString));
        }
    }
}
