using NetCoreSamples.Caching.Application.Interfaces;
using NetCoreSamples.Caching.Domain.Entities;
using NetCoreSamples.Caching.Domain;
using Microsoft.EntityFrameworkCore;

namespace NetCoreSamples.Caching.Persistence
{
    public class UserDAO : IUserDAO
    {
        private CachingSampleDbContext Context { get; }

        public UserDAO(CachingSampleDbContext context)
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
