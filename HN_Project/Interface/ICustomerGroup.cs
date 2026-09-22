using HN_Backend.DTOs.CustomerGroup;

namespace HN_Backend.Interface
{
    public interface ICustomerGroup
    {
        public Task<List<CustomerGroupDropdownDto>> GetCustomerGroupDropdownAsync(long companyId);
        public Task CreateCustomerGroupAsync(CustomerGroupCreateDto customerGroupDto);
    }
}
