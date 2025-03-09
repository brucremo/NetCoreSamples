using Microsoft.Extensions.Caching.Distributed;
using Serilog;
using System.Text;
using System.Text.Json;

namespace NetCoreSamples.Caching.Lib
{
    /// <summary>
    /// The Redis cache manager.
    /// </summary>
    public class RedisCacheManager
    {
        private IDistributedCache cache { get; }

        public RedisCacheManager(IDistributedCache cache)
        {
            this.cache = cache;
        }

        /// <summary>
        /// Takes the data object and serializes it to JSON before storing it in the cache.
        /// </summary>
        /// <param name="key">The cache key</param>
        /// <param name="dataObject">The data object</param>
        /// <returns>A <see cref="Task"/> representing the async operation</returns>
        public async Task SetSerializableDataAsync(string key, object dataObject)
        {
            try
            {
                var serializedData = JsonSerializer.Serialize(dataObject);

                await this.cache.SetAsync(key, Encoding.UTF8.GetBytes(serializedData));
            }
            catch (Exception ex)
            {
                Log.Warning($"{ex.Message} | {ex.StackTrace}");
            }
        }

        /// <summary>
        /// Retrieves the data from the cache and deserializes it to the specified type.
        /// </summary>
        /// <typeparam name="T">The data type</typeparam>
        /// <param name="key">The cache key</param>
        /// <returns>The <typeparamref name="T"/> data.</returns>
        public async Task<T?> GetSerializableDataAsync<T>(string key) where T : class
        {
            try
            {
                if (await this.cache.GetAsync(key) is byte[] cachedData)
                {
                    var serializedData = Encoding.UTF8.GetString(cachedData);

                    return JsonSerializer.Deserialize<T>(serializedData);
                }

                return null;
            } 
            catch (Exception ex)
            {
                Log.Warning($"{ex.Message} | {ex.StackTrace}");
                return null;
            }
        }
    }
}
