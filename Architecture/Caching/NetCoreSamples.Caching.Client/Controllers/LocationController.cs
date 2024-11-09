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
        private IMediator Mediator { get; }

        public LocationController(IMediator mediator)
        {
            this.Mediator = mediator;
        }

        [HttpGet("countrystates")]
        public async Task<IEnumerable<LocationDTO>> GetAllCountryStatesAsync()
        {
            return await this.Mediator.Send(new GetLocation.AllCountryStates());
        }
    }
}
