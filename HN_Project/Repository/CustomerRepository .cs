using HN_Backend.Data;
 
using HN_Project.DTOs;
using HN_Project.Interface; 
using HN_Project.Repository;
using Microsoft.EntityFrameworkCore;

namespace HN_Project.Repository
{
    public class CustomerRepository  :ICustomerRepository
    {
        private readonly ApplicationDbContext _db;
        public CustomerRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<List<Customer>> GetCustomerByCodeNameAndPhone(string pramObj,long companyId)
        {
            return await _db.Customers.Where(x=>(x.Code.Contains(pramObj) || x.Name.Contains(pramObj)|| (x.Phone !=null && x.Phone.Contains(pramObj)) && x.CompanyId == companyId)).Take(10).ToListAsync(); 
        }
        public async Task CreateCustomerAsync(Customer customer)
        {
            await _db.Customers.AddAsync(customer);
        }
        public async Task UpdateCustomerAsync(Customer customer)
        {
             _db.Customers.Update(customer);
        }
    }
} 