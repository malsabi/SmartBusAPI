namespace SmartBusAPI.Hubs
{
    public class NotificationHub : Hub
    {
        public async Task JoinBusGroup(int busId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, $"bus-{busId}");
        }

        public async Task LeaveBusGroup(int busId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"bus-{busId}");
        }

        public async Task JoinParentGroup(int parentId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, $"parent-{parentId}");
        }

        public async Task LeaveParentGroup(int parentId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"parent-{parentId}");
        }

        public async Task SendNotificationToBusGroup(int busId, string message)
        {
            await Clients.Group($"bus-{busId}").SendAsync("ReceiveNotification", message);
        }

        public async Task SendNotificationToParentGroup(int parentId, string message)
        {
            await Clients.Group($"parent-{parentId}").SendAsync("ReceiveNotification", message);
        }
    }
}