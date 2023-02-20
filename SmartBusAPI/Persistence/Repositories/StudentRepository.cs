namespace SmartBusAPI.Persistence.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly SmartBusContext smartBusContext;

        public StudentRepository(SmartBusContext smartBusContext)
        {
            this.smartBusContext = smartBusContext;
        }

        public async Task<IEnumerable<Student>> GetAllStudents()
        {
            return await smartBusContext.Students.ToListAsync();
        }

        public async Task<Student> GetStudentById(int id)
        {
            return await smartBusContext.Students.FindAsync(id);
        }

        public async Task AddStudent(Student student)
        {
            smartBusContext.Students.Add(student);
            await smartBusContext.SaveChangesAsync();
        }

        public async Task UpdateStudent(Student student)
        {
            smartBusContext.Students.Update(student);
            await smartBusContext.SaveChangesAsync();
        }

        public async Task DeleteStudent(Student student)
        {
            smartBusContext.Students.Remove(student);
            await smartBusContext.SaveChangesAsync();
        }
    }
}