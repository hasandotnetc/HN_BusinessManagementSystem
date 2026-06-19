using HN_Frontend.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;
//using System.Net.Http.Json; 
//using HN_FrontEnd.Models;

namespace HN_Frontend.Controllers
{
    public class POSController : Controller
    {
        //public IActionResult Index()
        //{
        //    return View();
        //}
        private readonly HttpClient _client;

        public POSController()
        {
            _client = new HttpClient();
        }

        public async Task<IActionResult> Index()
        {
            //var url = $"https://localhost:7008/api/SearchingByDapper/{paramObj}";

            //var data = await _client.GetFromJsonAsync<List<CurrentStockProductVM>>(url);

            return View();
        }

        public async Task<IActionResult> GetProductList(string paramObj = "a")
        {
            var url = $"https://localhost:7008/api/SearchingByDapper/{paramObj}";

            var data = await _client.GetFromJsonAsync<List<CurrentStockProductVM>>(url);

            return Json(data);
        }


        //public async Task<IActionResult> Index()
        //{
        //    using var client = new HttpClient();
        //    //var products = await client.GetFromJsonAsync<object>();
        //    var products = await client.GetFromJsonAsync<List<CurrentStockProductVM>>(
        //        "https://localhost:7008/api/Product"
        //    );

        //    return View(products);
        //}
    }
}
