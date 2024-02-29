using Microsoft.EntityFrameworkCore;
using NetCoreSamples.Caching.Application.Interfaces;
using NetCoreSamples.Caching.Domain;
using NetCoreSamples.Caching.Domain.Entities;

namespace NetCoreSamples.Caching.Persistence
{
    public class CountryDAO : ICountryDAO
    {
        private CachingSampleDbContext Context { get; }

        public CountryDAO(CachingSampleDbContext context)
        {
            this.Context = context;
        }

        public async Task<IEnumerable<Country>> GetAllWithStatesAsync()
        {
            return await this.Context.Countries
                .Include(x => x.StateProvinces)
                .ToListAsync();
        }
    }
}
