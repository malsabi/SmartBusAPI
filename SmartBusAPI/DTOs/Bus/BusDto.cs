namespace SmartBusAPI.DTOs.Bus
{
    public class BusDto
    {
        public int ID { get; set; }
        public string BusNumber { get; set; }
        public string CurrentLocation { get; set; }
        public int Capacity { get; set; }
    }
}