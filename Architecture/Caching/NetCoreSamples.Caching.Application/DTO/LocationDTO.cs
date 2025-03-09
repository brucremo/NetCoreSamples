namespace NetCoreSamples.Caching.Application.DTO
{
    /// <summary>
    /// The data transfer object for location data.
    /// </summary>
    public class LocationDTO : BaseDTO
    {
        /// <summary>
        /// The name of the country.
        /// </summary>
        public string Country { get; set; } = null!;

        /// <summary>
        /// The state/provinces of the country.
        /// </summary>
        public IEnumerable<string> StateProvinces { get; set; } = null!;
    }
}
