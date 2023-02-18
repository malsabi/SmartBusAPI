namespace SmartBusAPI.Entities
{
    public class Notification
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public DateTime DateAndTime { get; set; }

        public int? ParentID { get; set; }
        public virtual Parent Parent { get; set; }

        public int? BusID { get; set; }
        public virtual Bus Bus { get; set; }
    }
}