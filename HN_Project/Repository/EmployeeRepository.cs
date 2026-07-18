using HN_Backend.Models;
 
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
        public async Task<List<Employee>> GetEmployeeByCodeNamePhone(string objParam)
        {
            return await _db.Employees.Where(x => x.Name.Contains(objParam) || x.Code.Contains(objParam) || (x.Phone !=null && x.Phone.Contains(objParam))).Take(10).ToListAsync();
        }
    }
}
