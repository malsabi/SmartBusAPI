namespace SmartBusAPI.DTOs.Trip
{
    public class UpdateStudentStatusOnBusDto
    {
        public int StudentID { get; set; }
        public int BusID { get; set; }
        public DateTime Timestamp { get; set; }
    }
}