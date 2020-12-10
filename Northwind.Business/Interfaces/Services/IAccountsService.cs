using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Northwind.Core.Domain;

namespace Northwind.Business.Interfaces.Services
{
    public interface IAccountsService
    {
        IEnumerable<User> GetUsers();

        Task<RegisterUserResult> RegisterUserAsync(User user, string callbackUrl);

        Task<bool> ConfirmEmailAsync(string userId, string code);

        Task<IEnumerable<AuthenticationScheme>> GetExternalLoginsAsync();

        Task<SignInResult> LoginAsync(User user, bool lockout);

        AuthenticationProperties GetAuthenticationProperties(string provider, string redirectUrl);

        Task<ExternalLoginResult> ExternalLoginAsync();

        Task LogoutAsync();

        Task SendResetEmailAsync(string email, string callbackUrl);

        Task<IdentityResult> ResetPasswordAsync(User user);
    }
}
