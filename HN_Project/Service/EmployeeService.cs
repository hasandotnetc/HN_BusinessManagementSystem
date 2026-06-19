using HN_Project.Interface;
using HN_Project.Models;

namespace HN_Project.Service
{
    
    public class EmployeeService
    {
        private readonly IEmployeeRepository _empRepo;
        public  EmployeeService(IEmployeeRepository empRepo)
        {
            _empRepo = empRepo;
        }
        public async Task<List<Employee>> GetEmployeeByCodeNamePhone(string objParam)
        {
            return await _empRepo.GetEmployeeByCodeNamePhone(objParam);
        }
    }
}
