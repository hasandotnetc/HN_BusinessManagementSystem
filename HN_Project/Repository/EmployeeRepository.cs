using HN_Backend.Data;
using HN_Backend.DTOs.Employee;

//using HN_Backend.Data;

using HN_Project.Interface; 
using Microsoft.EntityFrameworkCore;

namespace HN_Project.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly  ApplicationDbContext _db;
        public EmployeeRepository(ApplicationDbContext db)
        {
            _db= db;
        }
        public async Task<List<EmployeeAutocompleteDto>> GetEmployeeByCodeNamePhone(string objParam,long CompanyId)
        {
            return await _db.Employees.Where(x => (x.Name.Contains(objParam) || x.Code.Contains(objParam) || (x.Phone !=null && x.Phone.Contains(objParam)) && x.CompanyId == CompanyId && x.ActiveStatus == "Y" ))
                .Select(x=> new EmployeeAutocompleteDto
                {
                    EmployeeId = x.EmployeeId,
                    Name = x.Name
                } 
                ).Take(10).ToListAsync();
        }
    }
}
