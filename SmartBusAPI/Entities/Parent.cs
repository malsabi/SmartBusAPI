namespace SmartBusAPI.Entities
{
    public class Parent
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Password { get; set; }

        public virtual ICollection<Student> Students { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
    }
}