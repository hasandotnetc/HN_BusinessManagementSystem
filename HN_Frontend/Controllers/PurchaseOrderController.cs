using Microsoft.AspNetCore.Mvc;

namespace HN_Frontend.Controllers
{
    public class PurchaseOrderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
