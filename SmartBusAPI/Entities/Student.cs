namespace SmartBusAPI.Entities
{
    public class Student
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Grade { get; set; }
        public string School { get; set; }
        public int ParentID { get; set; }
        public int BusID { get; set; }

        public Parent Parent { get; set; }
        public Bus Bus { get; set; }
    }
}