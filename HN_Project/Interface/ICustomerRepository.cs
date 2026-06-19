using HN_Project.Models;

namespace HN_Project.Interface
{
    public interface ICustomerRepository
    {
        Task<List<Customer>> GetCustomerByCodeNameAndPhone(string pramObj);
    }
}
