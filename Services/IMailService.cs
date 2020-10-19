

using System.Threading.Tasks;
using DigiSign.Models;

namespace DigiSign.Services
{
    public interface IMailService
    {
        Task SendEmailAsync(MailRequest mailRequest);

        Task<bool> Send(EmailTo email_to, string subject, MailBasic view_data, string view_name = "Basic", string from = null, string[] attachments = null);
    }
    
}