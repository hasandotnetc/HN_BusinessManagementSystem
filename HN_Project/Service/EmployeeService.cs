using HN_Backend.Data;
using HN_Backend.DTOs.Employee;
using HN_Backend.Helpers;
using HN_Project.Interface;

namespace HN_Project.Service
{
    
    public class EmployeeService
    {
        private readonly IEmployeeRepository _empRepo;
        private readonly CurrentSessionData _currentSessionData;
        public  EmployeeService(IEmployeeRepository empRepo, CurrentSessionData currentSessionData)
        {
            _empRepo = empRepo;
            _currentSessionData = currentSessionData;
        }
        public async Task<List<EmployeeAutocompleteDto>> GetEmployeeByCodeNamePhone(string objParam)
        {
            long companyId = _currentSessionData.CompanyId;
            return await _empRepo.GetEmployeeByCodeNamePhone(objParam, companyId);
        }
    }
}
