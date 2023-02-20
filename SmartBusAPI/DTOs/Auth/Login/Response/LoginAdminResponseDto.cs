namespace SmartBusAPI.DTOs.Auth.Login.Response
{
    public class LoginAdminResponseDto
    {
        public AdminDto AdminDto { get; set; }
        public string AuthToken { get; set; }
    }
}