using System.Threading.Tasks;

namespace HomeAssets.Domain.Interfaces
{
    public interface IMailService
    {
        void SendEmail(string toEmail, string subject, string body);
    }
}