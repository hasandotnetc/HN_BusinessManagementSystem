using HN_Backend.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HN_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VatTaxController : ControllerBase
    {
        private readonly VatTaxService _taxService;
        public VatTaxController(VatTaxService taxService)
        {
            _taxService = taxService;
        }
        [HttpGet]
        [Route("GetTaxes")]
        public async Task<IActionResult> GetTaxes() 
        {
            var taxes = await _taxService.GetTaxes();
            return Ok(taxes);
        }
    }
}
