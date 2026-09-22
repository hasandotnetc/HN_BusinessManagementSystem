using HN_Backend.Data;
using HN_Backend.DTOs.CustomerGroup;
using HN_Backend.Interface;
using Microsoft.EntityFrameworkCore;

namespace HN_Backend.Repository
{
    public class CustomerGroupRepository : ICustomerGroup
    {
        private readonly ApplicationDbContext _db;
        public CustomerGroupRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<List<CustomerGroupDropdownDto>> GetCustomerGroupDropdownAsync(long companyId)
        {
            var _customerGroup = await _db.CustomerGroups.Where(x => x.CompanyId == companyId).Select(x => new CustomerGroupDropdownDto
            {
                CustomerGroupId = x.CustomerGroupId,
                Name = x.Name
            }).ToListAsync();
            return _customerGroup;
        }
        public async Task CreateCustomerGroupAsync(CustomerGroupCreateDto customerGroupDto)
        {
            var customerGroup = new CustomerGroup
            {
                Name = customerGroupDto.Name,
                Code = customerGroupDto.Code,
                CompanyId = customerGroupDto.CompanyId,
                EntryBy = customerGroupDto.EntryBy
            };
            await _db.CustomerGroups.AddAsync(customerGroup); 
        }
    }
}
