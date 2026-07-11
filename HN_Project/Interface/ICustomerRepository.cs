using HN_Backend.Models; 
namespace HN_Project.Interface
{
    public interface ICustomerRepository
    {
        Task<List<Customer>> GetCustomerByCodeNameAndPhone(string pramObj);
    }
}
