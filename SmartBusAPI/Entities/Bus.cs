namespace SmartBusAPI.Entities
{
    public class Bus
    {
        public int ID { get; set; }
        public int LicenseNumber { get; set; }
        public int Capacity { get; set; }
        public string CurrentLocation { get; set; }
        public bool IsInService { get; set; }

        public virtual ICollection<Student> Students { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
        public virtual BusDriver BusDriver { get; set; }
    }
}