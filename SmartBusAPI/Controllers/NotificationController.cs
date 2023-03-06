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
        public async Task<IActionResult> GetNotifications(int id)
        {
            ErrorOr<IEnumerable<Notification>> result;

            List<Notification> notifications = (List<Notification>)await notificationRepository.GetAllNotifications(id);

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
        public async Task<IActionResult> GetNotificationsStartFrom(DateTime value, int id)
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
        public async Task<IActionResult> GetNotification(int id)
        {
            var notification = await notificationRepository.GetNotificationById(id);

            if (notification == null)
            {
                return NotFound();
            }

            return Ok(notification);
        }

        [HttpPost]
        public async Task<IActionResult> PostNotification(Notification notification)
        {
            await notificationRepository.AddNotification(notification);
            return CreatedAtAction(nameof(GetNotification), new { id = notification.ID }, notification);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutNotification(int id, Notification notification)
        {
            if (id != notification.ID)
            {
                return BadRequest();
            }

            await notificationRepository.UpdateNotification(notification);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNotification(int id)
        {
            var notification = await notificationRepository.GetNotificationById(id);

            if (notification == null)
            {
                return NotFound();
            }

            await notificationRepository.DeleteNotification(notification);

            return NoContent();
        }
    }
}