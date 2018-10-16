using Microsoft.AspNetCore.Mvc;

namespace Actio.Api.Controllers
{
    [Route("")]
    //[ApiController]
    public class HomeController : Controller
    {
        [HttpGet("")]
        public IActionResult Get() => Content(@"Hello from Actio API!");
    }
}