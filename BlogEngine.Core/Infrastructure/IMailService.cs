
namespace BlogEngine.Core.Infrastructure
{
    public interface IMailService
    {
        bool Send(string to, string from, string subject, string body);

        void SendRegisterEmail(string to, string username, string confirmationToken);
    }
}
