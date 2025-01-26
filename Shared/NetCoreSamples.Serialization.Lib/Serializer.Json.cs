using System.Text.Json;

namespace NetCoreSamples.Serialization.Lib
{
    /// <summary>
    /// The global serializer class.
    /// </summary>
    public static partial class Serializer
    {
        /// <summary>
        /// Serialize object to json string.
        /// </summary>
        /// <typeparam name="T">The destination type</typeparam>
        /// <param name="json">The json string</param>
        /// <param name="options">The serializer options</param>
        /// <returns>An object of type <see cref="T"/></returns>
        public static T? DeserializeJson<T>(this string json, SerializerOptions.Json? options = null)
        {
            return JsonSerializer.Deserialize<T>(json, options?.GetSerializerLibraryOptions());
        }

        /// <summary>
        /// Serialize object to json string.
        /// </summary>
        /// <typeparam name="T">The object type</typeparam>
        /// <param name="obj">The object to serializa</param>
        /// <param name="options">The serializer options</param>
        /// <returns>The json string</returns>
        public static string SerializeJson<T>(this T obj, SerializerOptions.Json? options = null)
        {
            return JsonSerializer.Serialize(obj, options?.GetSerializerLibraryOptions());
        }
    }
}
