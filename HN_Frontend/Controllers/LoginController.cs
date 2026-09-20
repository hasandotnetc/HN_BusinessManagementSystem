using Microsoft.AspNetCore.Mvc;

namespace HN_Frontend.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
