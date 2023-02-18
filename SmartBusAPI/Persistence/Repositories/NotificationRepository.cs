using SmartBusAPI.Hubs;
using SmartBusAPI.Entities;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using SmartBusAPI.Common.Interfaces.Repositories;

namespace SmartBusAPI.Persistence.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly SmartBusContext smartBusContext;
        private readonly IHubContext<NotificationHub> hubContext;

        public NotificationRepository(SmartBusContext smartBusContext,
                                      IHubContext<NotificationHub> hubContext)
        {
            this.smartBusContext = smartBusContext;
            this.hubContext = hubContext;
        }

        public async Task<IEnumerable<Notification>> GetAllNotifications()
        {
            return await smartBusContext.Notifications.ToListAsync();
        }

        public async Task<Notification> GetNotificationById(int id)
        {
            return await smartBusContext.Notifications.FindAsync(id);
        }

        public async Task AddNotification(Notification notification)
        {
            smartBusContext.Notifications.Add(notification);
            await smartBusContext.SaveChangesAsync();
            await hubContext.Clients.All.SendAsync("NewNotificationItemAdded");
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