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

        public async Task<IActionResult> Index(bool? isUserAddedSucces)
        {
            try
            {
                var newsCount = await _newsServices.GetNewsCountAsync(); 
                var userCount = await _userServices.GetUserCountAsync();
                var adminPanelModel = new AdminPanelModel
                {
                    NewUserModel = new NewUserModel(),
                    NewsCount = newsCount,
                    UsersCount = userCount,
                    IsUserAddedSucces = isUserAddedSucces
                };
                return View(adminPanelModel);
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        public async Task<IActionResult> AddUserProcessing([FromForm] NewUserModel newUserModel)
        {
            try
            {
                var user = await _userServices.GetUserByLoginAsync(newUserModel.Login);
                if (user is not null)
                    return RedirectToAction(nameof(Index), new { isUserAddedSucces = false});
                var createUserDto = new CreateUserDto
                {
                    Login = newUserModel.Login,
                    PasswordHas = _passwordHashing.HashPassword(newUserModel.Password),
                    RoleName = newUserModel.RoleName
                };
                var newUserId = await _userServices.CreateUserAsync(createUserDto);
                return RedirectToAction(nameof(Index), new { isUserAddedSucces = true });
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
