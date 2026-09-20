using HN_Backend.Data;
using HN_Backend.DTOs;

namespace HN_Backend.Interface
{
    public interface ICompany
    {
        Task <List<CompanyDropdownDto>> GetAllCompanyListAsync();
        Task <Company> GetCompanyAllInformationAsync(long CompanyId);
    }
}