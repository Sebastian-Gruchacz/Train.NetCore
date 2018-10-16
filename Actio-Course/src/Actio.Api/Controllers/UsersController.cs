using System.Threading.Tasks;
using Actio.Common.Commands;
using Actio.Common.MessageBus;
using Microsoft.AspNetCore.Mvc;

namespace Actio.Api.Controllers
{
    [Route("[controller]")]
    //[ApiController]
    public class UsersController : Controller
    {
        private readonly IMsgBus _bus;

        public UsersController(IMsgBus bus)
        {
            _bus = bus;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Post([FromBody] CreateUser command)
        {
            await _bus.PublishAsync(command);

            return Accepted();
        }
    }
}