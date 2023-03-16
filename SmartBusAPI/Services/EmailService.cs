namespace SmartBusAPI.Services
{
    public class EmailService : IEmailService
    {
        public async Task<bool> SendEmail(ContactDto contactDto)
        {
            if (string.IsNullOrEmpty(contactDto.FirstName) ||
                string.IsNullOrEmpty(contactDto.LastName) ||
                string.IsNullOrEmpty(contactDto.Email) ||
                string.IsNullOrEmpty(contactDto.Message))
            {
                return false;
            }
            try
            {
                using SmtpClient client = new(MailServerConsts.Host, MailServerConsts.Port);
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential(MailServerConsts.Username, MailServerConsts.Password);
                using MailMessage message = new()
                {
                    From = new MailAddress(contactDto.Email),
                    To = { new MailAddress(MailServerConsts.Username) },
                    Subject = "Contact form submission",
                    Body = $"First Name: {contactDto.FirstName}\n" +
                           $"Last Name: {contactDto.LastName}\n" +
                           $"Email: {contactDto.Email}\n" +
                           $"Message: {contactDto.Message}"
                };
                await client.SendMailAsync(message);
                return true;
            }
            catch (SmtpException)
            {
                return false;
            }
        }
    }
}