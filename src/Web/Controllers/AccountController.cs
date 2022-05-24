using Core.DTOs.Account;
using Core.Interfaces;
using Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IEmailSender _emailSender;

        public AccountController(UserManager<AppUser> userManager, IEmailSender emailSender, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _emailSender = emailSender;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var name = model.Email.Split("@").First();

                var user = new AppUser { Name = name, UserName = model.Email, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    #region EmailConfirmation
                    var emailConfiramtionToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                    var emailBody = Url.Action(
                        action: "ConfirmEmail",
                        controller: "Account",
                        values: new { userName = user.UserName, token = emailConfiramtionToken },
                        protocol: Request.Scheme );

                    await _emailSender.SendAsync(user.Email, "Eamil Confirmation", emailBody);
                    #endregion

                    return RedirectToAction("Index", "Home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string userName, string token)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(token))
                return NotFound();

            var user = await _userManager.FindByNameAsync(userName);
            if(user == null)
                return NotFound();

            var result = await _userManager.ConfirmEmailAsync(user, token);

            return Content(result.Succeeded ? "Email confirmed" : "Email not confirmed");
        }

        [HttpGet]
        public IActionResult Login(string? returnUrl = null)
        {
            ViewBag.ReturnUrl = returnUrl;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, true);

                if (result.Succeeded)
                {
                    return LocalRedirect(string.IsNullOrEmpty(returnUrl) ? Url.Content("~/") : returnUrl);
                }

                if (result.IsLockedOut)
                {
                    ModelState.AddModelError(string.Empty, "اکانت شما به دلیل پنج بار ورود ناموفق به مدت پنج دقیق قفل شده است");

                    ViewBag.ReturnUrl = returnUrl;
                    return View(model);
                }

                ModelState.AddModelError("", "رمزعبور یا نام کاربری اشتباه است");
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }
    }
}
