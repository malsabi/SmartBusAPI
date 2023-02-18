using SmartBusAPI.DTOs.Bus;

namespace SmartBusAPI.DTOs.Auth.Login
{
    public class LoginDriverResponseDto
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string DriverID { get; set; }
        public string PhoneNumber { get; set; }
        public string Country { get; set; }
        public BusDto BusDto { get; set; }
        public string Token { get; set; }
    }
}