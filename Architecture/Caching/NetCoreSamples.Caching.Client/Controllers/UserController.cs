using MediatR;
using Microsoft.AspNetCore.Mvc;
using NetCoreSamples.Caching.Application.DTO;
using NetCoreSamples.Caching.Application.Features.User.Requests;

namespace NetCoreSamples.Caching.Client.Controllers
{
    /// <summary>
    /// The calls on this controller will not hit the cache and will always be retrieved from the DB.
    /// This is governed by the request handlers under the Application layer.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private IMediator Mediator { get; }

        public UserController(IMediator mediator)
        {
            this.Mediator = mediator;
        }

        [HttpGet("users")]
        public async Task<IEnumerable<UserDTO>> GetAllUsersAsync()
        {
            return await this.Mediator.Send(new GetUser.All());
        }
    }
}
