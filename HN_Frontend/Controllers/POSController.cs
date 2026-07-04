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

        [HttpPost]
        public IActionResult SaveInvoice([FromBody] PosSaleDto payload)
        {
            if (payload == null || payload.ProductRowsAllInfo.Count == 0)
            {
                return Json(new { success = false, message = "Invalid data payload compiled." });
            }

            try
            {
                // 1. Process your business logic (save to Invoice Master / Details tables)
                // 2. If payload.PaymentOption.Method == "Split", access payload.SplitAmounts list loop elements

                return Json(new { success = true, invoiceId = 10452 });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        // Support Data Transfer Objects (DTOs)
        public class PosSaleDto
        {
            public CustomerVM Customer { get; set; }
            public EmployeeVM SalesPerson { get; set; }
            public PaymentOptionDto PaymentOption { get; set; }
            public SummaryDto Summary { get; set; }
            public List<ProductRowDto> ProductRowsAllInfo { get; set; }
            public List<SplitAmountDto> SplitAmounts { get; set; }
        }

        public class ProductRowDto
        {
            public int ProductId { get; set; }
            public string ProductName { get; set; }
            public string ProductCode { get; set; }
            public string SerialNo { get; set; }
            public string Model { get; set; }
            public decimal UnitPrice { get; set; }
            public int Quantity { get; set; }
            public decimal RowDiscount { get; set; }
            public decimal TotalPrice { get; set; }
        }

        public class SplitAmountDto
        {
            public string Method { get; set; }
            public decimal Amount { get; set; }
            public string Ref1 { get; set; } // Mobile / Card Last 4 digits / Cheque No
            public string Ref2 { get; set; } // Bank Name (For Card or Cheque)
        }


        public class PaymentOptionDto
        { 
            public string Method { get; set; } 
            public decimal ReceiveAmount { get; set; } 
            public decimal DueAmount { get; set; } 
            public decimal ChangeAmount { get; set; } 
            public string MobileNo { get; set; } 
            public string CardNo { get; set; } 
            public string ChequeNo { get; set; } 
            public string BankName { get; set; }
        }

        public class SummaryDto
        { 
            public decimal SubTotal { get; set; } 
            public decimal Discount { get; set; } 
            public decimal TaxPercent { get; set; } 
            public decimal GrandTotal { get; set; }
        }
    }
}
