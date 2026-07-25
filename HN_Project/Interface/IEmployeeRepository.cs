using HN_Backend.Data; 

namespace HN_Project.Interface
{
    public interface IEmployeeRepository
    {
        public Task<List<Employee>> GetEmployeeByCodeNamePhone(string objParam);
    }
}
