namespace SmartBusAPI.DTOs.Auth.Registration
{
    public class StudentRegisterDto : ParentRegisterDto
    {
        public int FaceRecognitionID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string SchoolName { get; set; }
        public int GradeLevel { get; set; }
    }
}