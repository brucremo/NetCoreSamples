using Microsoft.EntityFrameworkCore;
using NetCoreSamples.Caching.Application.Interfaces;
using NetCoreSamples.Domain;
using NetCoreSamples.Domain.Entities;

namespace NetCoreSamples.Caching.Persistence
{
    /// <inheritdoc />
    public class CountryDAO : ICountryDAO
    {
        /// <summary>
        /// The database context.
        /// </summary>
        readonly SampleDbContext context;

        public CountryDAO(SampleDbContext context)
        {
            this.context = context;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<Country>> GetAllWithStatesAsync()
        {
            return await context.Countries
                .Include(x => x.StateProvinces)
                .ToListAsync();
        }
    }
}
