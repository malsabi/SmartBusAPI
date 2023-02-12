namespace SmartBusAPI.Entities
{
    public class Administrator
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public ICollection<Bus> Buses { get; set; }
    }
}