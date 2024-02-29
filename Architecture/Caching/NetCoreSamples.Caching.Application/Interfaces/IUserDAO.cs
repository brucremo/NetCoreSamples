using NetCoreSamples.Caching.Domain.Entities;

namespace NetCoreSamples.Caching.Application.Interfaces
{
    /// <summary>
    /// Data Access Object for users
    /// </summary>
    public interface IUserDAO
    {
        /// <summary>
        /// Retrieve all users with their state/province and country included
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<User>> GetAllAsync();
    }
}
