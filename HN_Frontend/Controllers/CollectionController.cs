using Microsoft.AspNetCore.Mvc;

namespace HN_Frontend.Controllers
{
    public class CollectionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
