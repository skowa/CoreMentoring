using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Northwind.Business.Interfaces.Services;
using Northwind.Web.Constants;
using Northwind.Web.Models;

namespace Northwind.Web.Controllers
{
    public class AccountController : Controller
    {
        private const string HomeControllerName = "Home";
        private const string AccountControllerName = "Account";
        private const string IndexActionName = "Index";
        private const string InvalidLoginAttempt = "Invalid login attempt.";
        private const string EmailNotExist = "User with this email doesn't exist";

        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IEmailService _emailService;
        private readonly IMapper _mapper;

        public AccountController(
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

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterModel userModel)
        {
            if (ModelState.IsValid)
            {
                var user = _mapper.Map<IdentityUser>(userModel);
                var result = await _userManager.CreateAsync(user, userModel.Password);
                if (result.Succeeded)
                {
                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                        code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                        var callbackUrl = Url.Action(nameof(ConfirmEmail), AccountControllerName, new { userId = user.Id, code }, Request.Scheme);

                        await _emailService.SendAsync(user.Email, EmailsContent.ConfirmEmailSubject, string.Format(EmailsContent.ConfirmEmailMessage, HtmlEncoder.Default.Encode(callbackUrl)));

                        return RedirectToAction(nameof(RegisterConfirmation));
                    }

                    await _signInManager.SignInAsync(user, isPersistent: false);

                    return RedirectToAction(IndexActionName, HomeControllerName);
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(code))
            {
                return RedirectToAction(IndexActionName, HomeControllerName);
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return RedirectToAction(IndexActionName, HomeControllerName);
            }

            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            var result = await _userManager.ConfirmEmailAsync(user, code);

            return View(new ConfirmEmailModel { IsConfirmed = result.Succeeded });
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

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginModel userModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(userModel.Email, userModel.Password, userModel.RememberMe, lockoutOnFailure: true);
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
            await _signInManager.SignOutAsync();

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
                var user = await _userManager.FindByEmailAsync(email);
                if (user == null || !await _userManager.IsEmailConfirmedAsync(user))
                {
                    ModelState.AddModelError(email, EmailNotExist);
                }

                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                var callbackUrl = Url.Action(nameof(ResetPassword), AccountControllerName, new { code }, Request.Scheme);

                await _emailService.SendAsync(email, EmailsContent.ResetPasswordSubject, string.Format(EmailsContent.ResetPasswordMessage, HtmlEncoder.Default.Encode(callbackUrl)));

                return RedirectToAction(nameof(ForgotPasswordConfirmation));
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
            if (!ModelState.IsValid)
            {
                return View();
            }

            var user = await _userManager.FindByEmailAsync(resetPasswordModel.Email);
            if (user == null)
            {
                ModelState.AddModelError(resetPasswordModel.Email, EmailNotExist);
            }

            var result = await _userManager.ResetPasswordAsync(user, Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(resetPasswordModel.Code)), resetPasswordModel.Password);
            if (result.Succeeded)
            {
                return RedirectToAction(IndexActionName, HomeControllerName);
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(error.Code, error.Description);
            }

            return View();
        }
    }
}
