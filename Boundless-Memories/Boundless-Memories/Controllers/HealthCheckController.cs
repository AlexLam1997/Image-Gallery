using Microsoft.AspNetCore.Mvc;

namespace Memories.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthCheckController : BaseController
    {
        [HttpGet]
        public string Get()
        {
            return "Health ok, thanks for the check";
        }
    }
}
