using System;
using System.Threading.Tasks;
using Actio.Common.Commands;
using Actio.Common.MessageBus;
using Microsoft.AspNetCore.Mvc;

namespace Actio.Api.Controllers
{
    [Route("[controller]")]
    //[ApiController]
    public class ActivitiesController : Controller
    {
        private readonly IMsgBus _bus;

        public ActivitiesController(IMsgBus bus)
        {
            _bus = bus;
        }


        [HttpPost("")]
        public async Task<IActionResult> Post([FromBody] CreateActivity command)
        {
            command.Id = Guid.NewGuid();
            command.CreatedAt = DateTime.UtcNow;

            await _bus.PublishAsync(command);

            return Accepted($"activities/{command.Id}");
        }

    }
}