namespace SmartBusAPI.Entities
{
    public class Bus
    {
        public int ID { get; set; }
        public string BusNumber { get; set; }
        public string CurrentLocation { get; set; }
        public int Capacity { get; set; }

        public int? DriverID { get; set; }
        public virtual BusDriver BusDriver { get; set; }

        public int MonitorID { get; set; }
        public virtual Administrator Monitor { get; set; }

        public virtual ICollection<Student> Students { get; set; }

        public virtual ICollection<Notification> Notifications { get; set; }
    }
}