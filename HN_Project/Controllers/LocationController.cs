using HN_Backend.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HN_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly LocationService _locationService;
        public LocationController(LocationService locationService)
        {
            _locationService = locationService;
        }
        [HttpGet("GetAllLocationList")]
        public async Task<IActionResult> GetAllLocationListAsync()
        {
            var locations = await _locationService.GetAllLocationListAsync();
            return Ok(locations);
        }
        [HttpGet("GetAllLocationListByCompanyId")]
        public async Task<IActionResult> GetAllLocationListByCompanyIdAsync(long CompanyId) {
            var locations = await _locationService.GetAllLocationListByCompanyIdAsync(CompanyId);
            return Ok(locations);
        }
         

    }
}
