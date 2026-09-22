using HN_Backend.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HN_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly CompanyService companyService;
        public CompanyController(CompanyService companyService)
        {
            this.companyService = companyService;
        }

        [HttpGet("GetAllCompanyList")]
        public async Task<IActionResult> GetAllCompanyListAsync()
        {
            var companies = await companyService.GetAllCompanyListAsync();
            return Ok(companies);
        }
        [Authorize]
        [HttpGet("GetCompanyAllInformation")]
        public async Task<IActionResult> GetCompanyAllInformationAsync()
        {
            var companies = await companyService.GetCompanyAllInformationAsync();
            return Ok(companies);
        }


    }
}
