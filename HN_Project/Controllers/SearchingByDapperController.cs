using HN_Backend.DTOs;
using HN_Backend.DTOs.PaginationDto;
using HN_Project.Mapper;
using HN_Project.Service;
using Microsoft.AspNetCore.Authorization;
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

        [HttpGet("GetProductByNameCodeModelNoWithCurrentStock")]
        public async Task<IActionResult> GetProductByNameCodeModelNoWithCurrentStock(string paramObj)
        {
            var list = await _dappService.GetProductByNameCodeModelNoWithCurrentStock(paramObj);
            return Ok(list);
        }

        [HttpGet("GetProductByNameCodeSerialModelNoWithCurrentStock")]
        public async Task<IActionResult> GetProductByNameCodeSerialModelNoWithCurrentStock(string paramObj)
        {
            var list = await _dappService.GetProductByNameCodeSerialModelNoWithCurrentStock(paramObj); 
            return Ok(list);
        }
           
        [HttpGet("GetProductByPaginationRequest")]
        public async Task<IActionResult> GetProductByPaginationRequest([FromQuery] PaginationRequest request)
        {
            var list = await _dappService.GetProductByPaginationRequest(request);
            return Ok(list);
        }

        [Authorize]
        [HttpGet("GetCustomerByPaginationRequest")]
        public async Task<IActionResult> GetCustomerByPaginationRequest([FromQuery] PaginationRequest request)
        {
            var list = await _dappService.GetCustomerByPaginationRequest(request);
            return Ok(list);
        }

        [Authorize]
        [HttpGet("GetProductUnitTypeConversionRatio")]
        public async Task<IActionResult> GetProductUnitTypeConversionRatio([FromQuery] ProductUnitTypeConversionPaginationRequest request)
        {
            var list = await _dappService.GetProductUnitTypeConversionRatio(request);
            return Ok(list);
        }
        [Authorize]
        [HttpGet("GetProductBySearchForDetailAndStock")]
        public async Task<IActionResult> GetProductDetailWithStock(string objParam)
        {
            var list = await _dappService.GetProductUnitTypeConversionRatio(objParam);
            return Ok(list);
        }
    }
}
