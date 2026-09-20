using HN_Backend.Data;
using HN_Backend.DTOs;
using HN_Backend.Interface;
using Microsoft.EntityFrameworkCore;

namespace HN_Backend.Repository
{
    public class LocationRepository : ILocation
    {
        private readonly ApplicationDbContext _db;
        public LocationRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<List<LocationDropdownDto>> GetAllLocationListAsync()
        {
            var locationList = _db.Locations.Select(l => new LocationDropdownDto
            {
                LocationId = l.LocationId,
                Name = l.Name
            }).ToListAsync();
            return await locationList;
        }
        public async Task<List<LocationDropdownDto>> GetAllLocationListByCompanyIdAsync(long CompanyId)
        {
            var locationList = _db.Locations.Where(x=>x.CompanyId == CompanyId).Select(l => new LocationDropdownDto
            {
                LocationId = l.LocationId,
                Name = l.Name
            }).ToListAsync();
            return await locationList;
        }
    }
}
