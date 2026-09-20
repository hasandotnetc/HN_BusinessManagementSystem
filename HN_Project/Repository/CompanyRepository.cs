using HN_Backend.Data;
using HN_Backend.DTOs;
using HN_Backend.Interface;
using Microsoft.EntityFrameworkCore;

namespace HN_Backend.Repository
{
    public class CompanyRepository : ICompany
    {
        private readonly ApplicationDbContext _db;
        public CompanyRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task <List<CompanyDropdownDto>> GetAllCompanyListAsync()
        {
            var companyList = _db.Companies.Select(c => new CompanyDropdownDto
            {
                CompanyId = c.CompanyId,
                Name = c.Name
            }).ToListAsync();
            return await companyList;
        }

        public async Task<Company> GetCompanyAllInformationAsync(long CompanyId)
        {
            return await _db.Companies.FirstOrDefaultAsync(x => x.CompanyId == CompanyId);  
            //return companyList;
        }

    }
}
