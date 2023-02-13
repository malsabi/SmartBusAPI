namespace SmartBusAPI.Common.Interfaces.Services
{
    public interface IHashProviderService
    {
        string ComputeHash(string input);
        bool VerifyHash(string input, string hash);
    }
}