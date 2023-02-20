namespace SmartBusAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class AdministratorController : BaseController
    {
        private readonly IAdministratorRepository administratorRepository;

        public AdministratorController(IAdministratorRepository administratorRepository)
        {
            this.administratorRepository = administratorRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await administratorRepository.GetAdministrators());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var administrator = await administratorRepository.GetAdministratorById(id);
            if (administrator == null)
            {
                return NotFound();
            }
            return Ok(administrator);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Administrator administrator)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await administratorRepository.AddAdministrator(administrator);
            return CreatedAtAction(nameof(Get), new { id = administrator.ID }, administrator);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Administrator administrator)
        {
            if (id != administrator.ID)
            {
                return BadRequest();
            }
            await administratorRepository.UpdateAdministrator(administrator);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var administrator = await administratorRepository.GetAdministratorById(id);
            if (administrator == null)
            {
                return NotFound();
            }
            await administratorRepository.DeleteAdministrator(administrator);
            return NoContent(); 
        }
    }
}