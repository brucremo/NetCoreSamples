using NetCoreSamples.Caching.Application.Interfaces;
using NetCoreSamples.Domain.Entities;
using NetCoreSamples.Domain;
using Microsoft.EntityFrameworkCore;

namespace NetCoreSamples.Caching.Persistence
{
    /// <inheritdoc />
    public class UserDAO : IUserDAO
    {
        /// <summary>
        /// The database context.
        /// </summary>
        readonly SampleDbContext context;

        public UserDAO(SampleDbContext context)
        {
            this.context = context;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await context.Users
                .Include(x => x.StateProvince)
                    .ThenInclude(x => x.Country)
                .ToListAsync();
        }
    }
}
