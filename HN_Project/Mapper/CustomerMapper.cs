using HN_Backend.Data;
using HN_Backend.DTOs.Customer;

namespace HN_Project.Mapper
{
    public static class CustomerMapper
    {
        public static CustomerVM CustomerVMMapper(Customer customer)
        {
            return new CustomerVM()
            {
                CustomerId = customer.CustomerId,
                Code = customer.Code,
                Name = customer.Name,
                Phone = customer.Phone,
                Email = customer.Email,
                Address = customer.Address,
                OpeningBalance = customer.OpeningBalance,
                TotalSales = customer.TotalSales,
                //DueAmount = customer.DueAmount,
                Picture = customer.Picture,
            };
        }
    }
}
