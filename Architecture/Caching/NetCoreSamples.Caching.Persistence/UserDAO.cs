using NetCoreSamples.Caching.Application.Interfaces;
using NetCoreSamples.Domain.Entities;
using NetCoreSamples.Domain;
using Microsoft.EntityFrameworkCore;

namespace NetCoreSamples.Caching.Persistence
{
    public class UserDAO : IUserDAO
    {
        private SampleDbContext Context { get; }

        public UserDAO(SampleDbContext context)
        {
            this.Context = context;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await this.Context.Users
                .Include(x => x.StateProvince)
                    .ThenInclude(x => x.Country)
                .ToListAsync();
        }
    }
}
