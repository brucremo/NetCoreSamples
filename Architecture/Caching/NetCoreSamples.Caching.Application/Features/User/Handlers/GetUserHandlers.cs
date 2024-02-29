using MediatR;
using NetCoreSamples.Caching.Application.DTO;
using NetCoreSamples.Caching.Application.Interfaces;
using NetCoreSamples.Caching.Application.Features.User.Requests;

namespace NetCoreSamples.Caching.Application.Features.User.Handlers
{
    public class GetUserHandlers
    {
        public class AllUsers :
            IRequestHandler<GetUser.All, IEnumerable<UserDTO>>
        {
            private IUserDAO UserDAO { get; }

            public AllUsers(IUserDAO userDao)
            {
                this.UserDAO = userDao;
            }

            /// <summary>
            /// Handles the request to get all users.
            /// The data is always retrieved from the database and not stored in the cache.
            /// </summary>
            /// <param name="request"></param>
            /// <param name="cancellationToken"></param>
            /// <returns></returns>
            public async Task<IEnumerable<UserDTO>> Handle(GetUser.All request, CancellationToken cancellationToken)
            {
                var data = await this.UserDAO.GetAllAsync();

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
