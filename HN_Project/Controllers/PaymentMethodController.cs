using HN_Backend.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HN_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentMethodController : ControllerBase
    {
        private readonly PaymentMethodService _paymentMethodService;
        public PaymentMethodController(PaymentMethodService paymentMethodService)
        {
            _paymentMethodService = paymentMethodService;
        }
        [Authorize]
        [HttpGet]
        [Route("GetPaymentMethodDropdownList")]
        public async Task<IActionResult> GetPaymentMethodDropdownList()
        {
            var result = await _paymentMethodService.GetPaymentMethodDropdownList();
            return Ok(result);
        }
    }
}
