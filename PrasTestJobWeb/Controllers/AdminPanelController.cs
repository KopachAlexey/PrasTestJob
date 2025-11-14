using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PrasTestJobDTO;
using PrasTestJobServices.Abstract;
using PrasTestJobWeb.Models;

namespace PrasTestJobWeb.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminPanelController : Controller
    {
        readonly IUserServices _userServices;
        readonly IPasswordHashing _passwordHashing;
        readonly INewsServices _newsServices;

        public AdminPanelController(IUserServices userServices, IPasswordHashing passwordHashing, INewsServices newsServices)
        {
            _userServices = userServices;
            _passwordHashing = passwordHashing;
            _newsServices = newsServices;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> AddUserProcessing([FromForm] NewUserModel newUser)
        {
            try
            {
                var user = await _userServices.GetUserByLoginAsync(newUser.Login);
                if (user is not null)
                    return RedirectToAction(nameof(Index));
                var createUserDto = new CreateUserDto
                {
                    Login = newUser.Login,
                    PasswordHas = _passwordHashing.HashPassword(newUser.Password),
                    RoleName = newUser.RoleName
                };
                var newUserId = await _userServices.CreateUserAsync(createUserDto);
            }
            catch (Exception)
            {

                throw;
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
