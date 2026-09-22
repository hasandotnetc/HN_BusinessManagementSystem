using HN_Backend.Data;
using HN_Backend.DTOs.Employee;

namespace HN_Project.Interface
{
    public interface IEmployeeRepository
    {
        public Task<List<EmployeeAutocompleteDto>> GetEmployeeByCodeNamePhone(string objParam,long CompanyId);
    }
}
