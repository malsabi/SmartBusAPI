namespace SmartBusAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class BusDriverController : BaseController
    {
        private readonly IBusDriverRepository busDriverRepository;
        private readonly IHashProviderService hashProviderService;

        public BusDriverController(IBusDriverRepository busDriverRepository, IHashProviderService hashProviderService)
        {
            this.busDriverRepository = busDriverRepository;
            this.hashProviderService = hashProviderService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await busDriverRepository.GetAllBusDrivers());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            ErrorOr<BusDriver> result;
            BusDriver busDriver = await busDriverRepository.GetBusDriverById(id);
            if (busDriver == null)
            {
                result = Error.NotFound(code: "BusDriverNotFound", description: "No bus driver was found for the given ID.");
            }
            else
            {
                result = busDriver;
            }

            return result.Match
            (
                Ok,
                Problem
            );
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] BusDriver busDriver)
        {
            ErrorOr<string> result;
            if (!ModelState.IsValid)
            {
                result = Error.Validation(code: "InvalidBusDriver", description: "The given bus driver is not valid.");
            }
            else
            {
                busDriver.Password = hashProviderService.ComputeHash(busDriver.Password);
                await busDriverRepository.AddBusDriver(busDriver);
                result = string.Format("Bus driver with given ID [{0}] was added successfully.", busDriver.ID);
            }

            return result.Match
            (
                Ok,
                Problem
            );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] BusDriver busDriver)
        {
            ErrorOr<string> result;
            if (id != busDriver.ID)
            {
                result = Error.Validation(code: "InvalidBusDriverID", description: "The given ID does not match the ID of the bus driver.");
            }
            else if (!ModelState.IsValid)
            {
                result = Error.Validation(code: "InvalidBusDriver", description: "The given bus driver is not valid.");
            }
            else
            {
                busDriver.Password = hashProviderService.ComputeHash(busDriver.Password);
                await busDriverRepository.UpdateBusDriver(busDriver);
                result = string.Format("Bus driver with given ID [{0}] was updated successfully.", busDriver.ID);
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
            BusDriver busDriver = await busDriverRepository.GetBusDriverById(id);
            if (busDriver == null)
            {
                result = Error.NotFound(code: "BusDriverNotFound", description: "No bus driver was found for the given ID.");
            }
            else
            {
                await busDriverRepository.DeleteBusDriver(busDriver);
                result = string.Format("Bus driver with given ID [{0}] was deleted successfully.", busDriver.ID);
            }

            return result.Match
            (
                Ok,
                Problem
            );
        }
    }
}