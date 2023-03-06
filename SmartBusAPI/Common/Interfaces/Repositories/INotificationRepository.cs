namespace SmartBusAPI.Common.Interfaces.Repositories
{
    public interface INotificationRepository
    {
        public Task<IEnumerable<Notification>> GetAllNotifications(int id);
        public Task<IEnumerable<Notification>> GetNotificationsStartFrom(DateTime value, int id);
        public Task<Notification> GetNotificationById(int id);
        public Task AddNotification(Notification notification);
        public Task UpdateNotification(Notification notification);
        public Task DeleteNotification(Notification notification);
    }
}