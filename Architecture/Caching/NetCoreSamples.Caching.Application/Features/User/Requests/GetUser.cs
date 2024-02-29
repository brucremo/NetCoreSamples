using MediatR;
using NetCoreSamples.Caching.Application.DTO;

namespace NetCoreSamples.Caching.Application.Features.User.Requests
{
    public class GetUser
    {
        public class All : IRequest<IEnumerable<UserDTO>>
        {
        }
    }
}
