using HN_Project.Interface;
using HN_Backend.Models;

namespace HN_Project.Service
{
    
    public class CustomerService
    {
        private readonly ICustomerRepository _customerRepo;
        public CustomerService(ICustomerRepository customerRepo)
        {
            _customerRepo = customerRepo;
        }
        public async Task<List<Customer>> GetCustomerByCodeNameAndPhone(string objParam) 
        {
            return await _customerRepo.GetCustomerByCodeNameAndPhone(objParam);
        }
    }
}
 