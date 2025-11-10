using Microsoft.AspNetCore.Mvc;

namespace PrasTestJobWeb.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
