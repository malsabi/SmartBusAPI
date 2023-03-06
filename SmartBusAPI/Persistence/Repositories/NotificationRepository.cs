namespace SmartBusAPI.Persistence.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly SmartBusContext smartBusContext;

        public NotificationRepository(SmartBusContext smartBusContext)
        {
            this.smartBusContext = smartBusContext;
        }

        public async Task<IEnumerable<Notification>> GetAllNotifications(int id)
        {
            return await smartBusContext.Notifications.Where(n => n.BusID == id).ToListAsync();
        }

        public async Task<IEnumerable<Notification>> GetNotificationsStartFrom(DateTime value, int id)
        {
            return await smartBusContext.Notifications.Where(n => n.BusID == id && n.Timestamp > value).ToListAsync();
        }

        public async Task<Notification> GetNotificationById(int id)
        {
            return await smartBusContext.Notifications.FindAsync(id);
        }

        public async Task AddNotification(Notification notification)
        {
            smartBusContext.Notifications.Add(notification);
            await smartBusContext.SaveChangesAsync();
        }

        public async Task UpdateNotification(Notification notification)
        {
            smartBusContext.Notifications.Update(notification);
            await smartBusContext.SaveChangesAsync();
        }

        public async Task DeleteNotification(Notification notification)
        {
            smartBusContext.Notifications.Remove(notification);
            await smartBusContext.SaveChangesAsync();
        }
    }
}