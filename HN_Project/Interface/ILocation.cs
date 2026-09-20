using HN_Backend.DTOs;

namespace HN_Backend.Interface
{
    public interface ILocation
    {
        public Task<List<LocationDropdownDto>> GetAllLocationListAsync();
        public Task<List<LocationDropdownDto>> GetAllLocationListByCompanyIdAsync(long CompanyId);
    }
}
