using HN_Project.DTOs;
using HN_Project.Models;

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
