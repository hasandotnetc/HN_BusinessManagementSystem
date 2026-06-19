using HN_Project.Mapper;
using HN_Project.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HN_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchingByDapperController : ControllerBase
    {
        private readonly SearchingByDapperService _dappService;
        public SearchingByDapperController(SearchingByDapperService dapperService)
        {
            _dappService = dapperService;
        }

        [HttpGet("{paramObj}")]
        public async Task<IActionResult> GetProductByNameCodeSerialModelNoWithCurrentStock(string paramObj)
        {
            var list = await _dappService.GetProductByNameCodeSerialModelNoWithCurrentStock(paramObj);

            //var list = data.Select(x => CustomerMapper.CustomerVMMapper(x)).ToList();
            //data.Select(CustomerMapper.CustomerVMMapper).ToList();
            //if (list.Count == 0)
            //    return NotFound("No Data Found");
            return Ok(list);
        }
    }
}
