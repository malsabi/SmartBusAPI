namespace SmartBusAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class NotificationController : BaseController
    {
        private readonly INotificationRepository notificationRepository;

        public NotificationController(INotificationRepository notificationRepository)
        {
            this.notificationRepository = notificationRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            ErrorOr<IEnumerable<Notification>> result;

            List<Notification> notifications = (List<Notification>)await notificationRepository.GetAllNotifications();

            if (notifications == null || notifications.Count == 0)
            {
                result = Error.NotFound(code: "NotificationNotFound", description: "No notifications were found for the given ID.");
            }
            else
            {
                result = notifications;
            }

            return result.Match
            (
                Ok,
                Problem
            );
        }

        [HttpGet("start-from")]
        public async Task<IActionResult> GetStartFrom(DateTime value, int id)
        {
            ErrorOr<IEnumerable<Notification>> result;

            List<Notification> notifications = (List<Notification>)await notificationRepository.GetNotificationsStartFrom(value, id);

            if (notifications == null || notifications.Count == 0)
            {
                result = Error.NotFound(code: "NotificationNotFound", description: "No notifications were found for the given ID.");
            }
            else
            {
                result = notifications;
            }

            return result.Match
            (
                Ok,
                Problem
            );
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            ErrorOr<Notification> result;
            Notification notification = await notificationRepository.GetNotificationById(id);

            if (notification == null)
            {
                result = Error.NotFound(code: "NotificationNotFound", description: "No notification was found for the given ID.");
            }
            else
            {
                result = notification;
            }

            return result.Match
            (
                Ok,
                Problem
            );
        }

        [HttpPost]
        public async Task<IActionResult> Post(Notification notification)
        {
            ErrorOr<string> result;
            if (!ModelState.IsValid)
            {
                result = Error.Validation(code: "InvalidNotification", description: "The given notification is not valid.");
            }
            else
            {
                await notificationRepository.AddNotification(notification);
                result = string.Format("Notification with given ID [{0}] was added successfully.", notification.ID);
            }

            return result.Match
            (
                Ok,
                Problem
            );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Notification notification)
        {
            ErrorOr<string> result;
            if (id != notification.ID)
            {
                result = Error.Validation(code: "InvalidNotificationID", description: "The given ID does not match the ID of the notification.");
            }
            else if (!ModelState.IsValid)
            {
                result = Error.Validation(code: "InvalidNotification", description: "The given notification is not valid.");
            }
            else
            {
                await notificationRepository.UpdateNotification(notification);
                result = string.Format("Notification with given ID [{0}] was updated successfully.", notification.ID);
            }

            return result.Match
            (
                Ok,
                Problem
            );
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            ErrorOr<string> result;
            Notification notification = await notificationRepository.GetNotificationById(id);

            if (notification == null)
            {
                result = Error.NotFound(code: "NotificationNotFound", description: "No notification was found for the given ID.");
            }
            else
            {
                await notificationRepository.DeleteNotification(notification);
                result = string.Format("Notification with given ID [{0}] was deleted successfully.", notification.ID);
            }

            return result.Match
            (
                Ok,
                Problem
            );
        }
    }
}