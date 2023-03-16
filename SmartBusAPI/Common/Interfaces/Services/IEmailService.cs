namespace SmartBusAPI.Common.Interfaces.Services
{
    public interface IEmailService
    {
        Task<bool> SendEmail(ContactDto contactDto);
    }
}