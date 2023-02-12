namespace SmartBusAPI.DTOs.Auth
{
    public class StudentRegisterDto : RegisterDto
    {
        public string ParentEmail { get; set; }
        public string SchoolName { get; set; }
        public int GradeLevel { get; set; }
    }
}