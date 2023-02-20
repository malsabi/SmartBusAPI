namespace SmartBusAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class BusController : BaseController
    {
        private readonly IBusRepository busRepository;

        public BusController(IBusRepository busRepository)
        {
            this.busRepository = busRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var buses = await busRepository.GetAllBuses();
            return Ok(buses);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var bus = await busRepository.GetBusById(id);
            if (bus == null)
            {
                return NotFound();
            }
            return Ok(bus);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Bus bus)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await busRepository.AddBus(bus);
            return CreatedAtAction(nameof(Get), new { id = bus.ID }, bus);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Bus bus)
        {
            if (id != bus.ID)
            {
                return BadRequest();
            }
            await busRepository.UpdateBus(bus);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var bus = await busRepository.GetBusById(id);
            if (bus == null)
            {
                return NotFound();
            }
            await busRepository.DeleteBus(bus);
            return NoContent();
        }
    }
}