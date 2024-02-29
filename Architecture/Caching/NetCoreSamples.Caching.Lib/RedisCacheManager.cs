using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using System.Text;
using System.Text.Json;

namespace NetCoreSamples.Caching.Lib
{
    /// <summary>
    /// The Redis cache manager.
    /// </summary>
    public class RedisCacheManager
    {
        private IDistributedCache Cache { get; }
        private ILogger<RedisCacheManager> Logger { get; }

        public RedisCacheManager(IDistributedCache cache, ILogger<RedisCacheManager> logger)
        {
            this.Cache = cache;
            this.Logger = logger;
        }

        /// <summary>
        /// Takes the data object and serializes it to JSON before storing it in the cache.
        /// </summary>
        /// <param name="key">The cache key</param>
        /// <param name="dataObject">The data object</param>
        /// <returns></returns>
        public async Task SetSerializableDataAsync(string key, object dataObject)
        {
            try
            {
                var serializedData = JsonSerializer.Serialize(dataObject);

                await this.Cache.SetAsync(key, Encoding.UTF8.GetBytes(serializedData));
            }
            catch (Exception ex)
            {
                this.Logger.LogWarning($"{ex.Message} | {ex.StackTrace}");
            }
        }

        /// <summary>
        /// Retrieves the data from the cache and deserializes it to the specified type.
        /// </summary>
        /// <typeparam name="T">The data type</typeparam>
        /// <param name="key">The cache key</param>
        /// <returns></returns>
        public async Task<T?> GetSerializableDataAsync<T>(string key) where T : class
        {
            try
            {
                if (await this.Cache.GetAsync(key) is byte[] cachedData)
                {
                    var serializedData = Encoding.UTF8.GetString(cachedData);

                    return JsonSerializer.Deserialize<T>(serializedData);
                }

                return null;
            } 
            catch (Exception ex)
            {
                this.Logger.LogWarning($"{ex.Message} | {ex.StackTrace}");
                return null;
            }
        }
    }
}
