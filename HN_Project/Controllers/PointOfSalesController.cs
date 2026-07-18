using HN_Backend.Interface;
using HN_Backend.Service;
using HN_Shared.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HN_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PointOfSalesController : ControllerBase
    {
        private readonly PointOfSalesService _posService;
        private readonly ILogger<PointOfSalesController> _logger; 
        // Dependency injection handles injecting the service and logger automatically
        public PointOfSalesController(PointOfSalesService posService, ILogger<PointOfSalesController> logger )
        {
            _posService = posService;
            _logger = logger; 
        }

        [HttpPost("create-invoice")] // Maps to: POST api/PointOfSales/create-invoice
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(List<SaveInvoiceResponseDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateInvoice([FromBody] InvoicePOSDto payload)
        {
            // 1. Validate payload
            if (payload == null)
            {
                return BadRequest("Payload cannot be null.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Returns built-in validation errors
            }

            try
            {
                _logger.LogInformation("Processing new POS transaction for Customer ID: {CustomerId}", payload.Customer?.CustomerId);

                // 2. Call the transactional service layer
                var result = await _posService.CreatePointOfSalesAsync(payload);

                // 3. Return 201 Created status containing your DTO response
                return CreatedAtAction(nameof(CreateInvoice), result);
            }
            catch (ArgumentException ex)
            {
                // Catch any deliberate business/validation rules broken in service
                _logger.LogWarning(ex, "Business validation failed during POS checkout.");
                return BadRequest(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                // Catch database or other unexpected system failures safely
                _logger.LogError(ex, "An error occurred while creating Point of Sales invoice.");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { Message = "An internal error occurred while processing your request." });
            }
        }
    }
}
