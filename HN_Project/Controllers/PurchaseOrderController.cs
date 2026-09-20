using HN_Backend.DTOs.PurchaseOrder;
using HN_Backend.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HN_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseOrderController : ControllerBase
    {
        private readonly PurchaseOrderService _purchaseOrderService;
        public PurchaseOrderController(PurchaseOrderService purchaseOrderService)
        {
            _purchaseOrderService = purchaseOrderService;
        }

        [Authorize]
        [HttpPost("CreatePurchaseOrder")]
        public async Task<IActionResult> CreatePurchaseOrder([FromBody] PurchaseOrderCreateDto dto)
        {
            var result = await _purchaseOrderService.CreatePurchaseOrder(dto);
            if (!result.Success)
                return BadRequest();
            return Ok(result);
        }


    }
}
