namespace Message.Abstraction
{
    public interface IMailService
    {
        Task<bool> SendEmailAsync(string fromEmail, string password, string smtp, int port, string body, string toEmailAddress, string subject);
    }
}