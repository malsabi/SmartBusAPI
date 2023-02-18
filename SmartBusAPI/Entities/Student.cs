namespace SmartBusAPI.Entities
{
    public class Student
    {
        public int ID { get; set; }
        public int FaceRecognitionID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string SchoolName { get; set; }
        public int GradeLevel { get; set; }

        public int ParentID { get; set; }
        public virtual Parent Parent { get; set; }

        public int? BusID { get; set; }
        public virtual Bus Bus { get; set; }
    }
}