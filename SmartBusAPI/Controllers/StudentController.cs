using SmartBusAPI.DTOs.Student;

namespace SmartBusAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class StudentController : BaseController
    {
        private readonly IMapper mapper;
        private readonly IStudentRepository studentRepository;

        public StudentController(IMapper mapper, 
                                 IStudentRepository studentRepository)
        {
            this.mapper = mapper;
            this.studentRepository = studentRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await studentRepository.GetAllStudents());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            Student student = await studentRepository.GetStudentById(id);
            if (student == null)
            {
                return NotFound();
            }

            StudentDto studentDto = mapper.Map<StudentDto>(student);
            return Ok(studentDto);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Student student)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await studentRepository.AddStudent(student);
            return CreatedAtAction(nameof(Get), new { id = student.ID }, student);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Student student)
        {
            if (id != student.ID)
            {
                return BadRequest();
            }
            await studentRepository.UpdateStudent(student);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var student = await studentRepository.GetStudentById(id);
            if (student == null)
            {
                return NotFound();
            }
            await studentRepository.DeleteStudent(student);
            return NoContent();
        }
    }
}