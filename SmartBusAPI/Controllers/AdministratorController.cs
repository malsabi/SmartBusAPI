namespace SmartBusAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class AdministratorController : BaseController
    {
        private readonly IAdministratorRepository administratorRepository;
        private readonly IHashProviderService hashProviderService;

        public AdministratorController(IAdministratorRepository administratorRepository, IHashProviderService hashProviderService)
        {
            this.administratorRepository = administratorRepository;
            this.hashProviderService = hashProviderService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await administratorRepository.GetAdministrators());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            ErrorOr<Administrator> result;
            Administrator administrator = await administratorRepository.GetAdministratorById(id);
            if (administrator == null)
            {
                result = Error.NotFound(code: "AdministratorNotFound", description: "No administrator was found for the given ID.");
            }
            else
            {
                result = administrator;
            }

            return result.Match
            (
                Ok,
                Problem
            );
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Administrator administrator)
        {
            ErrorOr<string> result;
            if (!ModelState.IsValid)
            {
                result = Error.Validation(code: "InvalidAdministrator", description: "The given administrator is not valid.");
            }
            else
            {
                administrator.Password = hashProviderService.ComputeHash(administrator.Password);
                await administratorRepository.AddAdministrator(administrator);
                result = string.Format("Administrator with given ID [{0}] was added successfully.", administrator.ID);
            }

            return result.Match
            (
                Ok,
                Problem
            );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Administrator administrator)
        {
            ErrorOr<string> result;
            if (id != administrator.ID)
            {
                result = Error.Validation(code: "InvalidAdministratorID", description: "The given ID does not match the ID of the administrator.");
            }
            else if (!ModelState.IsValid)
            {
                result = Error.Validation(code: "InvalidAdministrator", description: "The given administrator is not valid.");
            }
            else
            {
                administrator.Password = hashProviderService.ComputeHash(administrator.Password);
                await administratorRepository.UpdateAdministrator(administrator);
                result = string.Format("Administrator with given ID [{0}] was updated successfully.", administrator.ID);
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
            Administrator administrator = await administratorRepository.GetAdministratorById(id);
            if (administrator == null)
            {
                result = Error.NotFound(code: "AdministratorNotFound", description: "No administrator was found for the given ID.");
            }
            else
            {
                await administratorRepository.DeleteAdministrator(administrator);
                result = string.Format("Administrator with given ID [{0}] was deleted successfully.", administrator.ID);
            }

            return result.Match
            (
                Ok,
                Problem
            );
        }
    }
}