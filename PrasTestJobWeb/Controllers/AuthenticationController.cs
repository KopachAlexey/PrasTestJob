using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using PrasTestJobServices.Abstract;
using PrasTestJobWeb.Models;
using PrasTestJobDTO;
using System.Security.Claims;

namespace PrasTestJobWeb.Controllers
{
    public class AuthenticationController : Controller
    {
        const string _mainPageUrl = "/";

        readonly IUserServices _userServices;
        readonly IPasswordHashing _passwordHashing;

        public AuthenticationController(IUserServices userServices, IPasswordHashing passwordHashing)
        {
            _userServices = userServices;
            _passwordHashing = passwordHashing;
        }

        [HttpGet]
        public IActionResult Login(string? returnUrl = null, bool? IsLoginSucces = null)
        {
            var userLoginModel = new UserLoginModel
            {
                ReturnUrl = returnUrl ?? _mainPageUrl,
                IsLoginSucces = IsLoginSucces
            };
            return View(userLoginModel);
        }

        [HttpPost]
        public async Task<IActionResult> LoginProcessing([FromForm] UserLoginModel loginModel)
        {
            //return RedirectToAction(nameof(AdminPanelController.Index), "AdminPanel");
            UserDto? user;
            try
            {
                user = await _userServices.GetUserByLoginAsync(loginModel.Login);
            }
            catch (Exception)
            {
                throw;
            }
            if (user is null || !_passwordHashing.VerifyPassword(loginModel.Password, user.PasswordHas))
                return RedirectToAction(nameof(Login), new { loginModel.ReturnUrl, IsLoginSucces = false });
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Login),
                new Claim(ClaimTypes.Role, user.RoleName),
            };
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = DateTime.UtcNow.AddDays(1)
            };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authProperties);
            if (!String.IsNullOrEmpty(loginModel.ReturnUrl) && Url.IsLocalUrl(loginModel.ReturnUrl))
                return LocalRedirect(loginModel.ReturnUrl);
            else
                return LocalRedirect(_mainPageUrl);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return LocalRedirect(_mainPageUrl);
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }

    }
}
