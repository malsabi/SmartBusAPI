namespace SmartBusAPI.Entities
{
    public class Parent
    {
        public int ID { get; set; }
        public string ParentFirstName { get; set; }
        public string ParentLastName { get; set; }
        public string ParentEmail { get; set; }
        public string ParentPhoneNumber { get; set; }
        public string ParentAddress { get; set; }
        public string Password { get; set; }

        public virtual ICollection<Student> Students { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
    }
}
