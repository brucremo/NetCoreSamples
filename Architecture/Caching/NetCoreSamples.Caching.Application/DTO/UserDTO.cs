namespace NetCoreSamples.Caching.Application.DTO
{
    public class UserDTO : BaseDTO
    {
        public required string Name { get; set; }
        public string Country { get; set; } = null!;
        public string StateProvince { get; set; } = null!;
    }
}
