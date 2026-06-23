using HN_Project.DTOs;
using HN_Project.Mapper;
using HN_Project.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HN_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeService _empService;
        public EmployeeController(EmployeeService empService)
        {
            _empService = empService;
        }

        [HttpGet("{objParam}")]
        public async Task<IActionResult> GetEmployeeByCodeNamePhone(string objParam)
        {
            var getEmployeeList= await _empService.GetEmployeeByCodeNamePhone(objParam);
            //var list = data.Select(x => CustomerMapper.CustomerVMMapper(x)).ToList();

            var _list = getEmployeeList.Select(x => EmployeeMapper.EmployeeVMMapper(x)).ToList();
            //if (_list.Count == 0)
            //    return NotFound("No data found !!");
            return Ok(_list);
        }
    }
}
