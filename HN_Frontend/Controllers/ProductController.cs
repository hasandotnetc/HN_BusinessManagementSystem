using Microsoft.AspNetCore.Mvc;

namespace HN_Frontend.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
