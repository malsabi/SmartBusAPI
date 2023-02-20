namespace SmartBusAPI.DTOs.Auth.Login.Response
{
    public class LoginParentResponseDto
    {
        public ParentDto ParentDto { get; set; }
        public string AuthToken { get; set; }
    }
}