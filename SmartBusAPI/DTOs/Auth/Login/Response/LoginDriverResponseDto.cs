namespace SmartBusAPI.DTOs.Auth.Login.Response
{
    public class LoginDriverResponseDto
    {
        public BusDriverDto BusDriverDto { get; set; }
        public BusDto BusDto { get; set; }
        public string AuthToken { get; set; }
    }
}