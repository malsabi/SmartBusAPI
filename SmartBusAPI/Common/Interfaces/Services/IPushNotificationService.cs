namespace SmartBusAPI.Common.Interfaces.Services
{
    public interface IPushNotificationService
    {
        Task<string> SendNotification(string title, string message, int? parentID, int? busID);
    }
}