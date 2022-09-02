using MimeKit;
using MailKit.Net.Smtp;
using Message.Abstraction;

namespace Message.Implementation
{
    public class MailService : IMailService
    {
        public async Task<bool> SendEmailAsync(string fromEmail, string password, string smtp, int port, string body, string toEmailAddress, string subject = null)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(MailboxAddress.Parse(fromEmail));
                message.To.Add(MailboxAddress.Parse(toEmailAddress));
                if (subject is not null)
                {
                    message.Subject = subject;
                }

                message.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = body };

                using (SmtpClient client = new())
                {
                    client.Connect(smtp, port, false);
                    client.Authenticate(fromEmail, password);
                    await client.SendAsync(message);
                    client.Disconnect(true);
                }
                return true;
            }
            catch (Exception ეხ)
            {
                return false;
            }
        }
    }
}