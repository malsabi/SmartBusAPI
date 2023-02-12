namespace SmartBusAPI.Entities
{
    public class Bus
    {
        public int ID { get; set; }
        public string BusNumber { get; set; }
        public string CurrentLocation { get; set; }
        public int Capacity { get; set; }

        public int DriverID { get; set; }
        public BusDriver BusDriver { get; set; }

        public int MonitorID { get; set; }
        public Administrator Monitor { get; set; }

        public ICollection<Student> Students { get; set; }

        public ICollection<Notification> Notifications { get; set; }
    }
}