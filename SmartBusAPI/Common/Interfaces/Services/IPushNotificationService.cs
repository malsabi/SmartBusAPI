namespace SmartBusAPI.Common.Interfaces.Services
{
    public interface IPushNotificationService
    {
        Task<string> SendNotification(string title, string message, string dateSent, int? parentID, int? busID);
    }
}