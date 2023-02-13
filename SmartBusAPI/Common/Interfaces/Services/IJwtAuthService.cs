namespace SmartBusAPI.Common.Interfaces.Services
{
    public interface IJwtAuthService
    {
        string GenerateAuthToken(string firstName, string lastName, string role);
    }
}