using MediatR;
using NetCoreSamples.Caching.Application.DTO;

namespace NetCoreSamples.Caching.Application.Features.Location.Requests
{
    /// <summary>
    /// The requests for the GetLocation feature.
    /// </summary>
    public class GetLocation
    {
        /// <summary>
        /// The request to get all countries and their states.
        /// </summary>
        public class AllCountryStates : IRequest<IEnumerable<LocationDTO>>
        {
        }
    }
}
