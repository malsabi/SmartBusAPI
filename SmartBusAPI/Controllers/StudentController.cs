namespace SmartBusAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class StudentController : BaseController
    {
        private readonly IStudentRepository studentRepository;

        public StudentController(IStudentRepository studentRepository)
        {
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
            ErrorOr<Student> result;
            Student student = await studentRepository.GetStudentById(id);
            if (student == null)
            {
                result = Error.NotFound(code: "InvalidStudentID", description: "The given ID does not exist.");
            }
            else
            {
                result = student;
            }
            return result.Match
            (
                Ok,
                Problem
            );
        }

        [HttpGet("faceRecognition/{id}")]
        public async Task<IActionResult> GetByFaceRecognition(int id)
        {
            ErrorOr<Student> result;
            Student student = await studentRepository.GetStudentByFaceRecognitionID(id);
            if (student == null)
            {
                result = Error.NotFound(code: "InvalidFaceRecognitionID", description: "The given face recognition ID does not exist.");
            }
            else
            {
                result = student;
            }
            return result.Match
            (
                Ok,
                Problem
            );
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Student student)
        {
            ErrorOr<string> result;
            if (!ModelState.IsValid)
            {
                result = Error.Validation(code: "InvalidStudent", description: "The given student is not valid.");
            }
            else
            {
                await studentRepository.AddStudent(student);
                result = string.Format("Student with given ID [{0}] was added successfully.", student.ID);
            }

            return result.Match
            (
                Ok,
                Problem
            );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Student student)
        {
            ErrorOr<string> result;
            if (id != student.ID)
            {
                result = Error.Validation(code: "InvalidStudentID", description: "The given ID does not match the student ID.");
            }
            else if (!ModelState.IsValid)
            {
                result = Error.Validation(code: "InvalidStudent", description: "The given student is not valid.");
            }
            else
            {
                await studentRepository.UpdateStudent(student);
                result = string.Format("Student with given ID [{0}] was updated successfully.", student.ID);
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
            Student student = await studentRepository.GetStudentById(id);
            if (student == null)
            {
                result = Error.NotFound(code: "InvalidStudentID", description: "The given ID does not exist.");
            }
            else
            {
                await studentRepository.DeleteStudent(student);
                result = string.Format("Student with given ID [{0}] was deleted successfully.", student.ID);
            }

            return result.Match
            (
                Ok,
                Problem
            );
        }
    }
}