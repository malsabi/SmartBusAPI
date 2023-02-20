namespace SmartBusAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class BusDriverController : BaseController
    {
        private readonly IBusDriverRepository busDriverRepository;

        public BusDriverController(IBusDriverRepository busDriverRepository)
        {
            this.busDriverRepository = busDriverRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await busDriverRepository.GetAllBusDrivers());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var busDriver = await busDriverRepository.GetBusDriverById(id);
            if (busDriver == null)
            {
                return NotFound();
            }
            return Ok(busDriver);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] BusDriver busDriver)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await busDriverRepository.AddBusDriver(busDriver);
            return CreatedAtAction(nameof(Get), new { id = busDriver.ID }, busDriver);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] BusDriver busDriver)
        {
            if (id != busDriver.ID)
            {
                return BadRequest();
            }
            await busDriverRepository.UpdateBusDriver(busDriver);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var busDriver = await busDriverRepository.GetBusDriverById(id);
            if (busDriver == null)
            {
                return NotFound();
            }
            await busDriverRepository.DeleteBusDriver(busDriver);
            return NoContent();
        }
    }
}