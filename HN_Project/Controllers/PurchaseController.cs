using HN_Backend.DTOs.PurchaseIn.CreateDto;
using HN_Backend.DTOs.PurchaseOrder;
using HN_Backend.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HN_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseController : ControllerBase
    {
        private readonly PurchaseService _purchaseService;
        public PurchaseController(PurchaseService purchaseService)
        {
            _purchaseService = purchaseService;
        }



        [Authorize]
        [HttpPost("CreatePurchase")]
        public async Task<IActionResult> CreatePurchase([FromBody] PurchaseInCreateDto dto)
        {
            var result = await _purchaseService.CreatePurchase(dto);
            if (!result.Success)
                return BadRequest();
            return Ok(result);
        }

    }
}
