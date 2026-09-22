using HN_Backend.Data; 
namespace HN_Project.Interface
{
    public interface ICustomerRepository
    {
        Task<List<Customer>> GetCustomerByCodeNameAndPhone(string pramObj,long CompanyId); 
        Task CreateCustomerAsync(Customer customer);
        Task UpdateCustomerAsync(Customer customer);
    }
}
