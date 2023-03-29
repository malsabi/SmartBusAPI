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
            ErrorOr<Bus> result;
            Bus bus = await busRepository.GetBusById(id);
            if (bus == null)
            {
                result = Error.NotFound(code: "BusNotFound", description: "No bus was found for the given ID.");
            }
            else
            {
                result = bus;
            }

            return result.Match
            (
                Ok,
                Problem
            );
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Bus bus)
        {
            ErrorOr<string> result;
            if (!ModelState.IsValid)
            {
                result = Error.Validation(code: "InvalidBus", description: "The given bus is not valid.");
            }
            else
            {
                await busRepository.AddBus(bus);
                result = string.Format("Bus with given ID [{0}] was added successfully.", bus.ID);
            }

            return result.Match
            (
                Ok,
                Problem
            );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Bus bus)
        {
            ErrorOr<string> result;
            if (id != bus.ID)
            {
                result = Error.Validation(code: "InvalidBusID", description: "The given ID does not match the ID of the bus.");
            }
            else if (!ModelState.IsValid)
            {
                result = Error.Validation(code: "InvalidBus", description: "The given bus is not valid.");
            }
            else
            {
                await busRepository.UpdateBus(bus);
                result = string.Format("Bus with given ID [{0}] was updated successfully.", bus.ID);
            }
            return result.Match
            (
                Ok,
                Problem
            );
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            ErrorOr<string> result;
            Bus bus = await busRepository.GetBusById(id);
            if (bus == null)
            {
                result = Error.NotFound(code: "BusNotFound", description: "No bus was found for the given ID.");
            }
            else
            {
                await busRepository.DeleteBus(bus);
                result = string.Format("Bus with given ID [{0}] was deleted successfully.", bus.ID);
            }

            return result.Match
            (
                Ok,
                Problem
            );
        }
    }
}