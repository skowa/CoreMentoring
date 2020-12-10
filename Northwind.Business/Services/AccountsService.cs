using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Northwind.Business.Interfaces.Services;
using Northwind.Core.Constants;
using Northwind.Core.Domain;

namespace Northwind.Business.Services
{
    public class AccountsService : IAccountsService
    {
        private const string ErrorLoadingExternalLogin = "Error loading external login information.";
        private const string EmailNotExist = "User with this email doesn't exist";

        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IEmailService _emailService;
        private readonly IMapper _mapper;

        public AccountsService(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IEmailService emailService,
            IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
            _mapper = mapper;
        }

        public IEnumerable<User> GetUsers()
        {
            return _userManager.Users.ProjectTo<User>(_mapper.ConfigurationProvider).ToList();
        }

        public async Task<RegisterUserResult> RegisterUserAsync(User user, string callbackUrl)
        {
            var registerUserResult = new RegisterUserResult();
            var identity = _mapper.Map<IdentityUser>(user);

            var result = await _userManager.CreateAsync(identity, user.Password);
            if (result.Succeeded)
            {
                if (_userManager.Options.SignIn.RequireConfirmedAccount)
                {
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(identity);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    callbackUrl = $"{callbackUrl}?userId={identity.Id}&code={code}";

                    await _emailService.SendAsync(user.Email, EmailsContent.ConfirmEmailSubject, string.Format(EmailsContent.ConfirmEmailMessage, HtmlEncoder.Default.Encode(callbackUrl)));

                    registerUserResult.EmailConfirmationRequired = true;
                }
                else
                {
                    await _signInManager.SignInAsync(identity, isPersistent: false);
                }
            }
            else
            {
                registerUserResult.RegistrationErrors = result.Errors.Select(error => error.Description);
            }

            return registerUserResult;
        }

        public async Task<bool> ConfirmEmailAsync(string userId, string code)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new ArgumentException($"{nameof(userId)} is not correct user id");
            }

            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            var result = await _userManager.ConfirmEmailAsync(user, code);

            return result.Succeeded;
        }

        public Task<IEnumerable<AuthenticationScheme>> GetExternalLoginsAsync()
        {
            return _signInManager.GetExternalAuthenticationSchemesAsync();
        }

        public async Task<SignInResult> LoginAsync(User user, bool lockout)
        {
            var result = await _signInManager.PasswordSignInAsync(user.Email, user.Password, user.RememberMe, lockout);

            return result;
        }

        public AuthenticationProperties GetAuthenticationProperties(string provider, string redirectUrl)
        {
            return _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
        }

        public async Task<ExternalLoginResult> ExternalLoginAsync()
        {
            var loginInfo = await _signInManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                throw new ArgumentException(ErrorLoadingExternalLogin);
            }

            var signInResult = await _signInManager.ExternalLoginSignInAsync(loginInfo.LoginProvider, loginInfo.ProviderKey, isPersistent: false, bypassTwoFactor: true);

            var externalLoginResult = new ExternalLoginResult { SignInSucceeded = signInResult.Succeeded, IsLockedOut = signInResult.IsLockedOut };

            var email = loginInfo.Principal.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Email)?.Value;
            var user = new IdentityUser { UserName = email, Email = email, EmailConfirmed = true };

            var createResult = await _userManager.CreateAsync(user);
            externalLoginResult.CreateUserSucceeded = createResult.Succeeded;
            if (createResult.Succeeded)
            {
                var addLoginResult = await _userManager.AddLoginAsync(user, loginInfo);
                if (addLoginResult.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false, loginInfo.LoginProvider);
                }
            }

            externalLoginResult.CreateUserErrors = createResult.Errors.Select(error => error.Description).ToList();

            return externalLoginResult;
        }

        public Task LogoutAsync()
        {
            return _signInManager.SignOutAsync();
        }

        public async Task SendResetEmailAsync(string email, string callbackUrl)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null || !await _userManager.IsEmailConfirmedAsync(user))
            {
                throw new ArgumentException(EmailNotExist);
            }

            var code = await _userManager.GeneratePasswordResetTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            callbackUrl = $"{callbackUrl}?code={code}";

            await _emailService.SendAsync(email, EmailsContent.ResetPasswordSubject, string.Format(EmailsContent.ResetPasswordMessage, HtmlEncoder.Default.Encode(callbackUrl)));
        }

        public async Task<IdentityResult> ResetPasswordAsync(User user)
        {
            var identity = await _userManager.FindByEmailAsync(user.Email);
            if (identity == null)
            {
                throw new ArgumentException(EmailNotExist);
            }

            var result = await _userManager.ResetPasswordAsync(identity, Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(user.Code)), user.Password);

            return result;
        }
    }
}
