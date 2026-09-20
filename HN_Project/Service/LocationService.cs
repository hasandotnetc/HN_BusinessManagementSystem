using HN_Backend.DTOs;
using HN_Backend.Interface;
using HN_Backend.Repository;

namespace HN_Backend.Service
{
    public class LocationService
    {
        private readonly ILocation _locationRepo;
        public LocationService(ILocation locationRepo)
        {
            _locationRepo = locationRepo;
        }
        public Task<List<LocationDropdownDto>> GetAllLocationListAsync()
        {
            return _locationRepo.GetAllLocationListAsync();
        }
        public Task<List<LocationDropdownDto>> GetAllLocationListByCompanyIdAsync(long CompanyId)
        {
            return _locationRepo.GetAllLocationListByCompanyIdAsync(CompanyId);
        }
    }
}
