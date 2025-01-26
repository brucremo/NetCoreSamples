using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace NetCoreSamples.DependencyInjection.Lib.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="IServiceCollection"/>.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds a hosted service implementation to the service collection.
        /// </summary>
        /// <typeparam name="TService">The service interface.</typeparam>
        /// <typeparam name="TImplementation">The service implementation.</typeparam>
        /// <param name="services">The <see cref="IServiceCollection"/></param>
        public static void AddInterfaceHostedService<TService, TImplementation>(this IServiceCollection services)
            where TService : class, IHostedService
            where TImplementation : class, TService, IHostedService
        {
            services.AddSingleton<TService, TImplementation>();
            services.AddHostedService(p => p.GetRequiredService<TService>());
        }

        /// <summary>
        /// Configures a service with a configuration section using the type name as the section name.
        /// </summary>
        /// <typeparam name="T">The type to be configured</typeparam>
        /// <param name="services">The <see cref="IServiceCollection"/></param>
        /// <param name="configuration">The <see cref="IConfiguration"/></param>
        /// <returns>The <see cref="IServiceCollection"/></returns>
        public static IServiceCollection ConfigureOptionsType<T>(this IServiceCollection services, IConfiguration configuration)
            where T : class
        {
            services.Configure<T>(configuration.GetSection(typeof(T).Name));
            return services;
        }
    }
}
