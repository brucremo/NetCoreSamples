using MediatR;
using NetCoreSamples.Caching.Application.DTO;
using NetCoreSamples.Caching.Application.Interfaces;
using NetCoreSamples.Caching.Application.Features.User.Requests;

namespace NetCoreSamples.Caching.Application.Features.User.Handlers
{
    /// <summary>
    /// The handlers for the GetUser requests.
    /// </summary>
    public class GetUserHandlers
    {
        /// <summary>
        /// Handles the request to get all users.
        /// </summary>
        public class AllUsers :
            IRequestHandler<GetUser.All, IEnumerable<UserDTO>>
        {
            /// <summary>
            /// The user data access object.
            /// </summary>
            readonly IUserDAO userDAO;

            public AllUsers(IUserDAO userDao)
            {
                this.userDAO = userDao;
            }

            /// <summary>
            /// Handles the request to get all users.
            /// The data is always retrieved from the database and not stored in the cache.
            /// </summary>
            /// <param name="request">The <see cref="GetUser.All"/> request.</param>
            /// <param name="cancellationToken">The <see cref="CancellationToken"/></param>
            /// <returns>A list of <see cref="UserDTO"/></returns>
            public async Task<IEnumerable<UserDTO>> Handle(GetUser.All request, CancellationToken cancellationToken)
            {
                var data = await userDAO.GetAllAsync();

                return data.Select(u =>
                    new UserDTO
                    {
                        Name = u.Username,
                        Country = u.StateProvince.Country.Name,
                        StateProvince = u.StateProvince.Name
                    });
            }
        }
    }
}
