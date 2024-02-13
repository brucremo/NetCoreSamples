using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetCoreSamples.Caching.Lib.Options;
using StackExchange.Redis;

namespace NetCoreSamples.Caching.Lib.Extensions
{
    public static class WebApplicationBuilderExtensions
    {
        public static void ConfigureRedisCluster(this WebApplicationBuilder builder)
        {
            var redisEndpoints = builder.Configuration.GetSection("Redis:EndPoints").Get<List<RedisEndpoint>>();

            var configOptions = new ConfigurationOptions 
            { 
                Password = builder.Configuration["Redis:Password"]
            };

            foreach (var endpoint in redisEndpoints)
            {
                configOptions.EndPoints.Add(endpoint.Host, endpoint.Port);
            }

            builder.Services.AddStackExchangeRedisCache(options =>
            {
                options.ConfigurationOptions = configOptions;
                options.InstanceName = $"{builder.Configuration["Redis:InstanceName"]}:";
            });
        }
    }
}
