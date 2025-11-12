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
        const string _mainPageUrl = "";

        readonly IUserServices _userServices;
        readonly IPasswordHashing _passwordHashing;

        public AuthenticationController(IUserServices userServices, IPasswordHashing passwordHashing)
        {
            _userServices = userServices;
            _passwordHashing = passwordHashing;
        }

        [HttpGet]
        public IActionResult Login(string? returnUrl = null, bool? loginResult = null)
        {
            ViewBag.ReturnUrl = returnUrl ?? _mainPageUrl;
            ViewBag.LoginResult = loginResult;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoginProcessing([FromForm] UserLoginModel loginModel)
        {
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
                return RedirectToAction(nameof(Login), new { loginModel.Login, LoginResult = false});
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Login),
                new Claim(ClaimTypes.Role, user.RoleName),
            };
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = false,
                ExpiresUtc = DateTime.UtcNow.AddMonths(1)
            };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authProperties);
            if (!String.IsNullOrEmpty(loginModel.ReturnUrl) && Url.IsLocalUrl(loginModel.ReturnUrl))
                return LocalRedirect(loginModel.ReturnUrl);
            else
                return Redirect(_mainPageUrl);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect(_mainPageUrl);
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }

    }
}
