using HN_Project.Data;
using HN_Project.Service;
using HN_Project.Mapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
            //if(list.Count == 0)
                //return NotFound("No Data Found");
            return Ok(list);
        }
         
    }
}
