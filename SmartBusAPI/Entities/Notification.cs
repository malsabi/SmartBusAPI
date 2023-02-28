namespace SmartBusAPI.Entities
{
    public class Notification
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public DateTime Timestamp { get; set; }
        public bool IsOpened { get; set; }

        public int? ParentID { get; set; }
        public int? BusID { get; set; }
    }
}