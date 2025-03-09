using MediatR;
using Microsoft.AspNetCore.Mvc;
using NetCoreSamples.Caching.Application.DTO;
using NetCoreSamples.Caching.Application.Features.Location.Requests;

namespace NetCoreSamples.Caching.Client.Controllers
{
    /// <summary>
    /// The calls on this controller will hit the DB first and then the cache on subsequent calls.
    /// This is governed by the request handlers under the Application layer.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class LocationController : Controller
    {
        /// <summary>
        /// The mediator for sending requests to the application layer.
        /// </summary>
        private IMediator mediator { get; }

        public LocationController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        /// <summary>
        /// Gets all countries and their states.
        /// </summary>
        /// <returns>A list of <see cref="LocationDTO"/></returns>
        [HttpGet("countrystates")]
        public async Task<IEnumerable<LocationDTO>> GetAllCountryStatesAsync()
        {
            return await this.mediator.Send(new GetLocation.AllCountryStates());
        }
    }
}
