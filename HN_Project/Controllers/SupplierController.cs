using HN_Backend.DTOs;
using HN_Backend.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HN_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private readonly SupplierService _supplierServ;
        public SupplierController(SupplierService supplierService)
        {
                _supplierServ = supplierService;
        }
        //[HttpGet("GetSupplierByNameCodePhone")]
        //public async Task<IActionResult> GetSupplierByNameCodePhone(string objParam)
        //{
        //    var supplierList = await _supplierServ.GetSupplierByCodeNamePhone(objParam); 
        //    var _list = supplierList.ToList(); 
        //    return Ok(_list);
        //}

        [Authorize]
        [HttpGet]
        [Route("GetSupplierByNameCodePhone")]
        public async Task<IActionResult> GetSupplierByNameCodePhone(string objParam)
        {
            var supplierList = await _supplierServ.GetSupplierByCodeNamePhone(objParam); 
            return Ok(supplierList); 
        }

        [Authorize]
        [HttpGet]
        [Route("GetSupplierInformation/{SupplierId}")]
        public async Task<IActionResult> GetSupplierInformation(long SupplierId)
        {
            var supplierInfo = await _supplierServ.GetSupplierInformationById(SupplierId);
            if (supplierInfo == null)
            {
                return NotFound(new { message = "Supplier not found." });
            }
            return Ok(supplierInfo);
        } 

        //[HttpPost("SaveSupplier")]
        //public async Task<IActionResult>  SaveSupplier([FromForm] SupplierVM vm)
        //{
        //    var supplierCode  = await _supplierServ.SaveSupplier(vm);
        //    return Ok(new
        //    {
        //        success = true,
        //        message = "Supplier saved successfully.",
        //        code = supplierCode
        //    });
        //    //return Ok(new { message = "Supplier saved successfully." });
        //}
    }
}
