using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Northwind.Business.Interfaces.Services;
using Northwind.Core.Domain;
using Northwind.Web.Models;

namespace Northwind.Web.Controllers
{
    public class AccountController : Controller
    {
        private const string HomeControllerName = "Home";
        private const string AccountControllerName = "Account";
        private const string IndexActionName = "Index";
        private const string InvalidLoginAttempt = "Invalid login attempt.";

        private readonly IAccountsService _accountsService;
        private readonly IMapper _mapper;

        public AccountController(
            IAccountsService accountsService,
            IMapper mapper)
        {
            _accountsService = accountsService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Register()
        {
            await FillViewBagWithExternalLoginsAsync();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterModel userModel)
        {
            if (ModelState.IsValid)
            {
                var callbackUrl = Url.Action(nameof(ConfirmEmail), AccountControllerName, null, Request.Scheme);

                var registerResult = await _accountsService.RegisterUserAsync(_mapper.Map<User>(userModel), callbackUrl);
                if (registerResult.EmailConfirmationRequired)
                {
                    return RedirectToAction(nameof(RegisterConfirmation));
                }

                foreach (var error in registerResult.RegistrationErrors)
                {
                    ModelState.AddModelError(string.Empty, error);
                }
            }


            await FillViewBagWithExternalLoginsAsync();

            return View(userModel);
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(code))
            {
                return RedirectToAction(IndexActionName, HomeControllerName);
            }

            var confirmed = await _accountsService.ConfirmEmailAsync(userId, code);

            return View(new ConfirmEmailModel { IsConfirmed = confirmed });
        }

        [HttpGet]
        public IActionResult RegisterConfirmation()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Login()
        {
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            await FillViewBagWithExternalLoginsAsync();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginModel userModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _accountsService.LoginAsync(_mapper.Map<User>(userModel), lockout: true);
                if (result.Succeeded)
                {
                    return RedirectToAction(IndexActionName, HomeControllerName);
                }

                if (result.IsLockedOut)
                {
                    return RedirectToAction(nameof(Lockout));
                }

                ModelState.AddModelError(string.Empty, InvalidLoginAttempt);
            }

            return View();
        }

        [HttpPost]
        public IActionResult ExternalLogin(string provider, string returnUrl = null)
        {
            var redirectUrl = Url.Action(nameof(ExternalLoginCallback), AccountControllerName, values: new { returnUrl });
            var properties = _accountsService.GetAuthenticationProperties(provider, redirectUrl);

            return new ChallengeResult(provider, properties);
        }

        [HttpGet]
        public async Task<IActionResult> ExternalLoginCallback(string remoteError = null)
        {
            if (remoteError != null)
            {
                ModelState.AddModelError(string.Empty, $"Error from external provider: {remoteError}");
                await FillViewBagWithExternalLoginsAsync();

                return View(nameof(Login));
            }

            ExternalLoginResult externalLoginResult = null;
            try
            {
                externalLoginResult = await _accountsService.ExternalLoginAsync();
                if (externalLoginResult.SignInSucceeded || externalLoginResult.CreateUserSucceeded)
                {
                    return RedirectToAction(IndexActionName, HomeControllerName);
                }

                if (externalLoginResult.IsLockedOut)
                {
                    return RedirectToAction(nameof(Lockout));
                }
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
            }

            await FillViewBagWithExternalLoginsAsync();
            foreach (var error in externalLoginResult.CreateUserErrors)
            {
                ModelState.AddModelError(string.Empty, error);
            }

            return View(nameof(Login));
        }

        [HttpGet]
        public IActionResult Lockout()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Logout()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Logout(string returnUrl)
        {
            await _accountsService.LogoutAsync();

            return LocalRedirect(returnUrl);
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var callbackUrl = Url.Action(nameof(ResetPassword), AccountControllerName, null, Request.Scheme);

                    await _accountsService.SendResetEmailAsync(email, callbackUrl);

                    return RedirectToAction(nameof(ForgotPasswordConfirmation));
                }
                catch (ArgumentException e)
                {
                    ModelState.AddModelError(string.Empty, e.Message);
                }
            }

            return RedirectToAction(IndexActionName, HomeControllerName);
        }

        [HttpGet]
        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ResetPassword(string code)
        {
            var model = new ResetPasswordModel { Code = code };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel resetPasswordModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _accountsService.ResetPasswordAsync(_mapper.Map<User>(resetPasswordModel));
                    if (result.Succeeded)
                    {
                        return RedirectToAction(IndexActionName, HomeControllerName);
                    }

                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(error.Code, error.Description);
                    }

                    return RedirectToAction(nameof(ForgotPasswordConfirmation));
                }
                catch (ArgumentException e)
                {
                    ModelState.AddModelError(string.Empty, e.Message);
                }
            }

            return View();
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }

        private async Task FillViewBagWithExternalLoginsAsync()
        {
            ViewBag.ExternalLogins = (await _accountsService.GetExternalLoginsAsync()).ToList();
        }
    }
}
