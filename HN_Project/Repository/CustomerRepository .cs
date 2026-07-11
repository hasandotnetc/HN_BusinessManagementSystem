using HN_Backend.Models;
using HN_Project.Data;
using HN_Project.DTOs;
using HN_Project.Interface; 
using HN_Project.Repository;
using Microsoft.EntityFrameworkCore;

namespace HN_Project.Repository
{
    public class CustomerRepository  :ICustomerRepository
    {
        private readonly ApplicationDbContext _context;
        public CustomerRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<Customer>> GetCustomerByCodeNameAndPhone(string pramObj)
        {
            return await _context.Customers.Where(x=>(x.Code.Contains(pramObj)
                || x.Name.Contains(pramObj)|| (x.Phone !=null && x.Phone.Contains(pramObj)))).Take(10).ToListAsync(); 
        }
    }
} 