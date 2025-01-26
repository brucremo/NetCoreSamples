using System.Text.Json;

namespace NetCoreSamples.Serialization.Lib
{
    /// <summary>
    /// The serializer options.
    /// </summary>
    public partial class SerializerOptions
    {
        /// <summary>
        /// The json serializer options.
        /// </summary>
        public class Json
        {
            /// <summary>
            /// The serializer options converted to <see cref="JsonSerializerOptions"/>.
            /// </summary>
            /// <returns>The <see cref="JsonSerializerOptions"/></returns>
            internal JsonSerializerOptions GetSerializerLibraryOptions()
            {
                return new JsonSerializerOptions();
            }
        }
    }
}
