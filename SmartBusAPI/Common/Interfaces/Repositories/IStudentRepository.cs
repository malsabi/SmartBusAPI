namespace SmartBusAPI.Common.Interfaces.Repositories
{
    public interface IStudentRepository
    {
        public Task<IEnumerable<Student>> GetAllStudents();
        public Task<Student> GetStudentById(int id);
        public Task AddStudent(Student student);
        public Task UpdateStudent(Student student);
        public Task DeleteStudent(Student student);
    }
}