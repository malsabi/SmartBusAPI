namespace SmartBusAPI.DTOs.Auth.Registration
{
    public class DriverRegisterDto : RegisterDto
    {
        public string DriverID { get; set; }
        public string PhoneNumber { get; set; }
        public string Country { get; set; }
    }
}