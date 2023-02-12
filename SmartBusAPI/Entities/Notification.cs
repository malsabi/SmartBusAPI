namespace SmartBusAPI.Entities
{
    public class Notification
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public DateTime DateAndTime { get; set; }

        public int ParentID { get; set; }
        public Parent Parent { get; set; }

        public int BusID { get; set; }
        public Bus Bus { get; set; }
    }
}