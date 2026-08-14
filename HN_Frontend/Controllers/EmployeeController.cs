using HN_Frontend.Models;
using Microsoft.AspNetCore.Mvc;

namespace HN_Frontend.Controllers
{
    public class EmployeeController : Controller
    {
        ///-------DI
        private readonly HttpClient _client;

        public EmployeeController()
        {
            _client = new HttpClient();
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetEmployeeByCodeNameAndPhone(string term)
        {
            var url = $"https://localhost:7008/api/Employee/GetEmployeeByCodeNamePhone/{term}";

            var data = await _client.GetFromJsonAsync<List<EmployeeVM>>(url);

            return Json(data);
        }
    }
}
