namespace SmartBusAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class ParentController : BaseController
    {
        private readonly IParentRepository parentRepository;

        public ParentController(IParentRepository parentRepository)
        {
            this.parentRepository = parentRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await parentRepository.GetAllParents());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var parent = await parentRepository.GetParentById(id);
            if (parent == null)
            {
                return NotFound();
            }
            return Ok(parent);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Parent parent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await parentRepository.AddParent(parent);
            return CreatedAtAction(nameof(Get), new { id = parent.ID }, parent);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Parent parent)
        {
            if (id != parent.ID)
            {
                return BadRequest();
            }
            await parentRepository.UpdateParent(parent);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var parent = await parentRepository.GetParentById(id);
            if (parent == null)
            {
                return NotFound();
            }
            await parentRepository.DeleteParent(parent);
            return NoContent();
        }
    }
}