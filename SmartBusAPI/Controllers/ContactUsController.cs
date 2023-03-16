namespace SmartBusAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class ContactUsController : BaseController
    {
        private readonly IEmailService emailService;

        public ContactUsController(IEmailService emailService)
        {
            this.emailService = emailService;
        }

        [HttpPost]
        public async Task<IActionResult> PostMessage([FromBody] ContactDto contactDto)
        {
            ErrorOr<string> result;
            if (await emailService.SendEmail(contactDto))
            {
                result = "Your message was successfully sent.";
            }
            else
            {
                result = Error.Failure("PostMessage.Failure", "There was a problem sending your message.");
            }
            return result.Match
            (
                Ok,
                Problem
            );
        }
    }
}