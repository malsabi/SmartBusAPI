namespace SmartBusAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class ParentController : BaseController
    {
        private readonly IParentRepository parentRepository;
        private readonly IHashProviderService hashProviderService;

        public ParentController(IParentRepository parentRepository, IHashProviderService hashProviderService)
        {
            this.parentRepository = parentRepository;
            this.hashProviderService = hashProviderService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await parentRepository.GetAllParents());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            ErrorOr<Parent> result;
            Parent parent = await parentRepository.GetParentById(id);
            if (parent == null)
            {
                result = Error.NotFound(code: "InvalidParentID", description: "The given Parent ID does not exists.");
            }
            else
            {
                result = parent;
            }
            return result.Match
            (
                Ok,
                Problem
            );
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Parent parent)
        {
            ErrorOr<string> result;

            if (!ModelState.IsValid)
            {
                result = Error.Validation(code: "InvalidParent", description: "The given parent is not valid.");
            }
            else
            {
                Parent currentParent = await parentRepository.GetParentByEmail(parent.Email);
                if (currentParent != null)
                {
                    result = Error.Conflict(code: "DuplicateEmailParent", description: "The given email already exists");
                }
                else
                {
                    parent.Password = hashProviderService.ComputeHash(parent.Password);
                    await parentRepository.AddParent(parent);
                    result = string.Format("Parent with given ID [{0}] was added successfully.", parent.ID);
                }
            }

            return result.Match
            (
                Ok,
                Problem
            );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Parent parent)
        {
            ErrorOr<string> result;
            if (id != parent.ID)
            {
                result = Error.Validation(code: "InvalidParentID", description: "The given ID does not match the parent ID.");
            }
            else if (!ModelState.IsValid)
            {
                result = Error.Validation(code: "InvalidParent", description: "The given parent is not valid.");
            }
            else
            {
                Parent currentParent = await parentRepository.GetParentById(id);

                if (currentParent == null)
                {
                    result = Error.NotFound(code: "InvalidParentID", description: "The given Parent ID does not exists.");
                }
                else
                {
                    if (currentParent.Password != parent.Password)
                    {
                        parent.Password = hashProviderService.ComputeHash(parent.Password);
                    }
                    await parentRepository.UpdateParent(parent);
                    result = string.Format("Parent with given ID [{0}] was updated successfully.", parent.ID);
                }
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
            Parent parent = await parentRepository.GetParentById(id);
            if (parent == null)
            {
                result = Error.NotFound(code: "InvalidParentID", description: "The given Parent ID does not exists.");
            }
            else
            {
                await parentRepository.DeleteParent(parent);
                result = string.Format("Parent with given ID [{0}] was deleted successfully.", parent.ID);
            }
            return result.Match
            (
                Ok,
                Problem
            );
        }
    }
}