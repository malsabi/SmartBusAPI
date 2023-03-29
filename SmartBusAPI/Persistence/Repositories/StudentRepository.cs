using SmartBusAPI.Entities;

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
            if (await smartBusContext.Students.FirstOrDefaultAsync(p => p.ID == student.ID) is Student found)
            {
                found.FirstName = student.FirstName;
                found.LastName = student.LastName;
                found.Gender = student.Gender;
                found.GradeLevel = student.GradeLevel;
                found.Address = student.Address;
                found.BelongsToBusID = student.BelongsToBusID;
                found.LastSeen = student.LastSeen;
                found.IsAtSchool = student.IsAtSchool;
                found.IsAtHome = student.IsAtHome;
                found.IsOnBus = student.IsOnBus;
                found.ParentID = student.ParentID;
                found.BusID = student.BusID;
            }
            await smartBusContext.SaveChangesAsync();
        }

        public async Task DeleteStudent(Student student)
        {
            smartBusContext.Students.Remove(student);
            await smartBusContext.SaveChangesAsync();
        }
    }
}