namespace NetCoreSamples.Caching.Application.DTO
{
    public class LocationDTO
    {
        public string Country { get; set; } = null!;
        public IEnumerable<string> StateProvinces { get; set; } = null!;
    }
}
