using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MarkaziaBITStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class HomeController : ControllerBase
    {
        public HomeController()
        {

        }

        [HttpGet]
        [Route("Index")]
        public IActionResult Index()
        {
            return Ok("Welcome to Markazia BIT Store API");
        }
    }
}
