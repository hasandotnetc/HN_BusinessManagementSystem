using HN_Backend.DTOs.ProductBrand;
using HN_Backend.DTOs.ProductCategory;
using HN_Backend.DTOs.ProductGroup;
using HN_Backend.DTOs.Products;
using HN_Backend.DTOs.UnitType;
using HN_Backend.Helpers;
using HN_Project.Mapper;
using HN_Project.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace HN_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _productServ; 
        public ProductController(ProductService productService )
        {
                _productServ = productService; 
        }

        [Authorize]
        [HttpGet]
        [Route("GetProductSearchAutocompleteList")]
        public async Task<IActionResult> GetProductSearchAutocompleteList(string productNameOrModelOrCode,long GroupId = 0, long BrandId = 0 , long CategoryId = 0)
        {  
            var productList = await _productServ.GetProductSearchAutocompleteList(productNameOrModelOrCode, GroupId, BrandId, CategoryId);
            return Ok(productList);
        }

        [Authorize]
        [HttpGet("AllProductList")]
        public async Task<IActionResult> GetAllProductList()
        {
            var productList = await _productServ.GetAllProductList(); 
            var _list = productList.ToList(); 
            return Ok(_list);
        }
        [Authorize]
        [HttpGet("AllProductCategoryList")]
        public async Task<IActionResult> GetAllProductCategoryList()
        {
            var productCategoryList = await _productServ.GetAllProductCategoryList();
            var _list = productCategoryList.ToList();
            return Ok(_list);
        }
        [Authorize]
        [HttpGet("AllUnitTypeList")] 
        public async Task<IActionResult> GetAllUnitTypeList()
        {
            var productCategoryList = await _productServ.GetAllUnitTypeList();
            var _list = productCategoryList.ToList();
            return Ok(_list);
        }
        [Authorize]
        [HttpGet("AllProductBrandList")] 
        public async Task<IActionResult> GetAllProductBrandList()
        {
            var productBrandList = await _productServ.GetAllProductBrandList();
            var _list = productBrandList.ToList();
            return Ok(_list);
        }
        [Authorize]
        [HttpGet("AllProductGroupList")] 
        public async Task<IActionResult> GetAllProductGroupList()
        {
            var productGroupList = await _productServ.GetAllProductGroupList();
            var _list = productGroupList.ToList();
            return Ok(_list);
        }
        [HttpGet("ProductById/{id}")]
        public async Task<IActionResult> GetProductById(long Id)
        {
            var product = await _productServ.GetProductById(Id); 
            return Ok(product);
        }
        [HttpGet("ProductByName/{Name}")]
        public async Task<IActionResult> GetProductByName(string Name)
        {
            var product = await _productServ.GetProductByName(Name); 
            return Ok(product);
        }
        [Authorize]
        [HttpGet]
        [Route("GetProductUnitTypeConversionRatio")]
        public async Task<IActionResult> GetProductUnitTypeConversionRatio(long ProductId)
        {
            var result = await _productServ.GetProductBaseUnitTypeAndConversionRatio(ProductId);
            return Ok(result);
        }

        [Authorize] 
        [HttpPost("SaveProduct")]
        public async Task<IActionResult> SaveProduct([FromForm] ProductVM dto)
        {
            var result = await _productServ.SaveProduct(dto);
            if (!result.Success)
                return BadRequest(result);
            return Ok(result);
        }
        [Authorize] 
        [HttpPost("SaveProductBrand")]
        public async Task<IActionResult> SaveProductBrand([FromBody] BrandVM dto) 
        {
            var result = await _productServ.SaveProductBrand(dto);
            if (!result.Success)
                return BadRequest(result);
            return Ok(result);
        }
        [Authorize] 
        [HttpPost("SaveProductCategory")] 
        public async Task<IActionResult> SaveProductCategory([FromBody] ProductCategoryVM dto) 
        {
            var result = await _productServ.SaveProductCategory(dto);
            if (!result.Success)
                return BadRequest(result);
            return Ok(result);
        }
        [Authorize]
        [HttpPost("SaveProductGroup")] 
        public async Task<IActionResult> SaveProductGroup([FromBody] ProductGroupVM dto)
        {
            var result = await _productServ.SaveProductGroup(dto);
            if (!result.Success)
                return BadRequest(result);
            return Ok(result); 
        }


        [Authorize]
        [HttpPut("UpdateProduct")]
        public async Task<IActionResult> UpdateProduct([FromForm] ProductVM dto)
        {
            var result = await _productServ.UpdateProduct(dto);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }
        [Authorize]
        [HttpPost]
        [Route("SaveProductUnitTypeConversion")]
        public async Task<IActionResult> SaveProductUnitTypeConversionAsync([FromBody] UnitTypeConversionCreateDto conversionDto)
        {
            var result = await _productServ.SaveProductUnitTypeConversionAsync(conversionDto);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [Authorize]
        [HttpPut]
        [Route("UpdateProductUnitTypeConversionAsync")]
        public async Task<IActionResult> UpdateProductUnitTypeConversionAsync([FromBody] UnitTypeConversionUpdateDto conversionDto)
        {
            var result = await _productServ.UpdateProductUnitTypeConversionAsync(conversionDto);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }


        [Authorize]
        [HttpGet]
        [Route("GetAllUnitTypeAfterProductSelection/{ProductId}")]
        public async Task<IActionResult> GetAllUnitTypeAfterProductSelection(long ProductId)
        {
            var result = await _productServ.GetProductUnitTypeForPurchase(ProductId); 
            return Ok(result);
        }

    }
}
