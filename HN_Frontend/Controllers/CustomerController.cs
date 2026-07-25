using HN_Frontend.Models;
using Microsoft.AspNetCore.Mvc;

namespace HN_Frontend.Controllers
{
    //[Route("Customer")]
    public class CustomerController : Controller
    {
        ///-------DI
        private readonly HttpClient _client;

        public CustomerController()
        {
            _client = new HttpClient();
        }


        public IActionResult Index()
        {
            return View();
        }

        //[HttpGet("GetCustomerByCodeNameAndPhone")]
        public async Task<IActionResult> GetCustomerByCodeNameAndPhone(string term = "Mr. Cash Sale")
        {
            var url = $"https://localhost:7008/api/Customer/{term}";  
            //GetAllProductList
            var data = await _client.GetFromJsonAsync<List<CustomerVM>>(url);

            return Json(data);
        }

    }
}
