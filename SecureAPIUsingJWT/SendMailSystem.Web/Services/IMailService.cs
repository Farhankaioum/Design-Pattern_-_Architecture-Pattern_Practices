using SendMailSystem.Web.Models;
using System.Threading.Tasks;

namespace SendMailSystem.Web.Services
{
    public interface IMailService
    {
        Task SendEmailAsync(MailRequest mailRequest);
    }
}
