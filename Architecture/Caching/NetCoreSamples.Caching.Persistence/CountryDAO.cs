using Microsoft.EntityFrameworkCore;
using NetCoreSamples.Caching.Application.Interfaces;
using NetCoreSamples.Domain;
using NetCoreSamples.Domain.Entities;

namespace NetCoreSamples.Caching.Persistence
{
    public class CountryDAO : ICountryDAO
    {
        private SampleDbContext Context { get; }

        public CountryDAO(SampleDbContext context)
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
