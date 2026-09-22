using HN_Backend.Data;
using HN_Backend.DTOs;
using HN_Backend.Helpers;
using HN_Backend.Interface;
using HN_Backend.Repository;

namespace HN_Backend.Service
{
    public class CompanyService
    {
        private readonly ICompany _companyRepo;
        public readonly CurrentSessionData _currentSessionData;
        public CompanyService(ICompany companyRepo,CurrentSessionData currentSessionData)
        {
            _companyRepo = companyRepo;
            _currentSessionData = currentSessionData;
        }
        public Task<List<CompanyDropdownDto>> GetAllCompanyListAsync()
        {
            return _companyRepo.GetAllCompanyListAsync();
        }
        public Task<Company> GetCompanyAllInformationAsync()
        {
            long compId = _currentSessionData.CompanyId;
            return _companyRepo.GetCompanyAllInformationAsync(_currentSessionData.CompanyId);
        }
    }
}
