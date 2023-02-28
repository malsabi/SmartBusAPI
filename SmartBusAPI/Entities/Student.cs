namespace SmartBusAPI.Entities
{
    public class Student
    {
        public int ID { get; set; }
        public string Image { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public int GradeLevel { get; set; }
        public string Address { get; set; }
        public int BelongsToBusID { get; set; }
        public DateTime LastSeen { get; set; }
        public bool IsAtSchool { get; set; } = false;
        public bool IsAtHome { get; set; } = true;
        public bool IsOnBus { get; set; } = false;

        public int ParentID { get; set; }
        public int? BusID { get; set; }
    }
}