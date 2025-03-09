using NetCoreSamples.Caching.Application.Enumerations;

namespace NetCoreSamples.Caching.Application.DTO
{
    /// <summary>
    /// The base data transfer object.
    /// </summary>
    public class BaseDTO
    {
        /// <summary>
        /// The data source where the data is coming from.
        /// </summary>
        public DataSource DataSource { get; set; }
    }
}
