namespace SmartBusAPI.DTOs.Auth.Registration
{
    public class StudentRegisterDto : ParentRegisterDto
    {
        public string Image { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public int GradeLevel { get; set; }
        public string Address { get; set; }
        public int BelongsToBusID { get; set; }
        public bool IsAtSchool { get; set; }
        public bool IsAtHome { get; set; }
        public bool IsOnBus { get; set; }
    }
}