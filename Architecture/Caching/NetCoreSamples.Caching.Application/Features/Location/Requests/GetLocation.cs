using MediatR;
using NetCoreSamples.Caching.Application.DTO;

namespace NetCoreSamples.Caching.Application.Features.Location.Requests
{
    public class GetLocation
    {
        public class AllCountryStates : IRequest<IEnumerable<LocationDTO>>
        {
        }
    }
}
