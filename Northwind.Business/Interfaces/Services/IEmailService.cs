using System.Threading.Tasks;

namespace Northwind.Business.Interfaces.Services
{
    public interface IEmailService
    {
        Task SendAsync(string email, string subject, string htmlMessage);
    }
}