using NetCoreSamples.Domain.Entities;

namespace NetCoreSamples.Caching.Application.Interfaces
{
    /// <summary>
    /// Data Access Object for countries
    /// </summary>
    public interface ICountryDAO
    {
        /// <summary>
        /// Retrieve all countries with their states/provinces included
        /// </summary>
        /// <returns></returns>
        public Task<IEnumerable<Country>> GetAllWithStatesAsync();
    }
}
