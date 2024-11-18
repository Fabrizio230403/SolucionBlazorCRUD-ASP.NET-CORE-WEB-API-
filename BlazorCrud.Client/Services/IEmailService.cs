using System.Threading.Tasks;

namespace BlazorCrud.Client.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(string toEmail, string subject, string body);
    }
}
