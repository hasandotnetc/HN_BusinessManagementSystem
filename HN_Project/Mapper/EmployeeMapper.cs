using HN_Backend.Data;
using HN_Backend.DTOs.Employee;

namespace HN_Project.Mapper
{
    public class EmployeeMapper
    {
        public static EmployeeVM EmployeeVMMapper(Employee employee)
        {
            return new EmployeeVM()
            {
                EmployeeId = employee.EmployeeId,
                Code = employee.Code,
                Name = employee.Name,
                Phone = employee.Phone,
                Email = employee.Email, 
                Picture = employee.Picture,
            };
        }
    }
}
