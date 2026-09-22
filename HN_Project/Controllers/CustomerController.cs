using HN_Project.Service;
using HN_Project.Mapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using HN_Backend.DTOs.Customer;

namespace HN_Project.Controllers
{ 
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    { 
        private readonly CustomerService _customerService; 
        public CustomerController(CustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet("{paramObj}")]
        public async Task<IActionResult> GetCustomerByCodeNameAndPhone(string paramObj)
        {
            var data = await _customerService.GetCustomerByCodeNameAndPhone(paramObj);
            var list = data.Select(x => CustomerMapper.CustomerVMMapper(x)).ToList();  
            return Ok(list);
        }
        [Authorize]
        [HttpPost]
        [Route("CreateCustomer")]
        public async Task<IActionResult> CreateCustomer([FromForm] CustomerCreateDto customerDto)
        {
            var result = await _customerService.CreateCustomerAsync(customerDto);
            if (!result.Success)
                return BadRequest(result);
            return Ok(result);
        }

        [Authorize]
        [HttpPut]
        [Route("UpdateCustomer")]
        public async Task<IActionResult> UpdateCustomer([FromForm] CustomerUpdateDto customerDto)
        {
            var result = await _customerService.UpdateCustomerAsync(customerDto);
            if (!result.Success)
                return BadRequest(result);
            return Ok(result);
        }
    }
}
