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
        public async Task<ActionResult<IEnumerable<Notification>>> GetNotifications()
        {
            return Ok(await notificationRepository.GetAllNotifications());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Notification>> GetNotification(int id)
        {
            var notification = await notificationRepository.GetNotificationById(id);

            if (notification == null)
            {
                return NotFound();
            }

            return Ok(notification);
        }

        [HttpPost]
        public async Task<ActionResult<Notification>> PostNotification(Notification notification)
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