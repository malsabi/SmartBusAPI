namespace SmartBusAPI.Controllers
{
    [Route("api/[controller]")]
    public class HealthController : BaseController
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Active");
        }
    }
}