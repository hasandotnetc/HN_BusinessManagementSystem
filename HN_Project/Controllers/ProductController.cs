using HN_Backend.DTOs;
using HN_Project.DTOs;
using HN_Project.Mapper;
using HN_Project.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HN_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _productServ;
        public ProductController(ProductService productService)
        {
                _productServ = productService;
        }
        [HttpGet("AllProductList")]
        public async Task<IActionResult> GetAllProductList()
        {
            var productList = await _productServ.GetAllProductList(); 
            var _list = productList.ToList(); 
            return Ok(_list);
        }
        [HttpGet("AllProductCategoryList")] 
        public async Task<IActionResult> GetAllProductCategoryList()
        {
            var productCategoryList = await _productServ.GetAllProductCategoryList();
            var _list = productCategoryList.ToList();
            return Ok(_list);
        }
        [HttpGet("AllProductBrandList")] 
        public async Task<IActionResult> GetAllProductBrandList()
        {
            var productBrandList = await _productServ.GetAllProductBrandList();
            var _list = productBrandList.ToList();
            return Ok(_list);
        }
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

        [HttpPost("SaveProduct")]
        public async Task<IActionResult> SaveProduct([FromForm] ProductVM dto)
        {
            await _productServ.SaveProduct(dto); 
            return Ok();
        }
        [HttpPost("SaveProductBrand")]
        public async Task<IActionResult> SaveProductBrand([FromBody] BrandVM dto)
        //public async Task<IActionResult> SaveProductBrand([FromForm] BrandVM dto)
        {
            await _productServ.SaveProductBrand(dto);
            return Ok();
        }
        [HttpPost("SaveProductCategory")] 
        public async Task<IActionResult> SaveProductCategory([FromBody] ProductCategoryVM dto) 
        {
            await _productServ.SaveProductCategory(dto);
            return Ok();
        }
        [HttpPost("SaveProductGroup")] 
        public async Task<IActionResult> SaveProductGroup([FromBody] ProductGroupVM dto)
        {
            await _productServ.SaveProductGroup(dto);
            return Ok();
        }
    }
}
