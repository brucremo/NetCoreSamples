using MediatR;
using NetCoreSamples.Caching.Application.DTO;

namespace NetCoreSamples.Caching.Application.Features.User.Requests
{
    /// <summary>
    /// The requests for the GetUser feature.
    /// </summary>
    public class GetUser
    {
        /// <summary>
        /// The request to get all users.
        /// </summary>
        public class All : IRequest<IEnumerable<UserDTO>>
        {
        }
    }
}
