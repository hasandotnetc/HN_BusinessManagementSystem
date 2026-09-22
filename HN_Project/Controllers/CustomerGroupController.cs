using HN_Backend.DTOs.CustomerGroup;
using HN_Backend.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace HN_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerGroupController : ControllerBase
    {
        private readonly CustomerGroupService _customerGroupService;
        public CustomerGroupController(CustomerGroupService customerGroupService)
        {
            _customerGroupService = customerGroupService;
        }

        [Authorize]
        [HttpGet]
        [Route("GetCustomerGroupDropdown")]
        public async Task<IActionResult> GetCustomerGroupDropdown()
        {
            var customerGroups = await _customerGroupService.GetCustomerGroupDropdownAsync();
            return Ok(customerGroups);
        }
        [Authorize]
        [HttpPost]
        [Route("CreateCustomerGroup")]
        public async Task<IActionResult> CreateCustomerGroup([FromBody] CustomerGroupCreateDto customerGroupDto)
        { 
            var result = await _customerGroupService.CreateCustomerGroupAsync(customerGroupDto);
            if (!result.Success)
                return BadRequest(result);
            return Ok(result);
        }
    }
}
