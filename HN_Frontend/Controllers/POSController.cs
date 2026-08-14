using HN_Frontend.Models;
using HN_Shared.DTOs;
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
            var url = $"https://localhost:7008/api/SearchingByDapper/GetProductByNameCodeSerialModelNoWithCurrentStock/{paramObj}";

            var data = await _client.GetFromJsonAsync<List<CurrentStockProductVM>>(url);

            return Json(data);
        }


        [HttpPost]
        public async Task<IActionResult> SaveInvoice([FromBody] InvoicePOSDto payload)
        {
            if (payload == null || payload.ProductRowsAllInfo.Count == 0)
            {
                return Json(new { success = false, message = "Invalid data payload compiled." });
            }

            try
            {
                var url = "https://localhost:7008/api/PointOfSales/create-invoice";

                var response = await _client.PostAsJsonAsync(url, payload);

                if (!response.IsSuccessStatusCode)
                {
                    var error = await response.Content.ReadAsStringAsync();

                    return Json(new
                    {
                        success = false,
                        message = error
                    });
                }

                var result = await response.Content.ReadFromJsonAsync<List<SaveInvoiceResponseDto>>();

                return Json(new
                {
                    success = true,
                    data = result
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    message = ex.Message
                });
            }
        }



        //[HttpPost]
        //public IActionResult SaveInvoice([FromBody] InvoicePOSDto payload)
        //{
        //    if (payload == null || payload.ProductRowsAllInfo.Count == 0)
        //    {
        //        return Json(new { success = false, message = "Invalid data payload compiled." });
        //    }

        //    try
        //    {
        //        // 1. Process your business logic (save to Invoice Master / Details tables)
        //        // 2. If payload.PaymentOption.Method == "Split", access payload.SplitAmounts list loop elements
        //        var url = $"https://localhost:7008/api/SearchingByDapper/{payload}";

        //         //await _client.GetFromJsonAsync<List<SaveInvoiceResponseDto>>(url);

        //        //return Json(data);
        //        return Json(new { success = true, invoiceId = 10452 });
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { success = false, message = ex.Message });
        //    }
        //}
    }
}
