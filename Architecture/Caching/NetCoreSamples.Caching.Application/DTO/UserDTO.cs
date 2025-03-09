namespace NetCoreSamples.Caching.Application.DTO
{
    /// <summary>
    /// The data transfer object for the user.
    /// </summary>
    public class UserDTO : BaseDTO
    {
        /// <summary>
        /// The name of the user.
        /// </summary>
        public required string Name { get; set; }

        /// <summary>
        /// The country of the user.
        /// </summary>
        public string Country { get; set; } = null!;

        /// <summary>
        /// The state/province of the user.
        /// </summary>
        public string StateProvince { get; set; } = null!;
    }
}
