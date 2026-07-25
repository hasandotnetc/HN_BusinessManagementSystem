using HN_Backend.DTOs;
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
            return Ok(list);
        }

        //[HttpGet("GetProductByPaginationRequest/{paramObj}")]
        [HttpGet("GetProductByPaginationRequest")]
        public async Task<IActionResult> GetProductByPaginationRequest([FromQuery] PaginationRequest request)
        {
            var list = await _dappService.GetProductByPaginationRequest(request);
            return Ok(list);
        }
    }
}
