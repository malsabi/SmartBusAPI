namespace SmartBusAPI.DTOs.Auth
{
    public class ParentRegisterDto : RegisterDto
    {
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
    }
}