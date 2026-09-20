using HN_Backend.Data;
using HN_Backend.DTOs;
using HN_Backend.DTOs.Supplier;
using HN_Backend.Interface;
using Microsoft.EntityFrameworkCore;

namespace HN_Backend.Repository
{
    public class SupplierRepository : ISupplier
    {
        private readonly ApplicationDbContext _db;
        public SupplierRepository(ApplicationDbContext db)
        {
            this._db = db;
        }

        public async Task<List<SupplierAutocompleteDto>> GetSupplierByCodeNamePhone(string objParam, long CompanyId)
        {
            return await _db.Suppliers.Where(s => s.Code.Contains(objParam) || s.Name.Contains(objParam) || (s.Phone != null && s.Phone.Contains(objParam)) 
            && s.CompanyId == CompanyId &&  s.ActiveStatus == "Y")
                .Select(x=> new SupplierAutocompleteDto
                {
                    SupplierId = x.SupplierId,
                    Name = x.Name
                }).Take(10).ToListAsync();
        }
        public async Task<Supplier> GetSupplierByIdAsync(long supplierId,long CompanyId)
        {
            return await _db.Suppliers.FirstOrDefaultAsync(s => s.SupplierId == supplierId && s.CompanyId == CompanyId);
        }

        public async Task<SupplierSelectedInformationDto> GetSupplierInformationById(long supplierId, long CompanyId)
        {
            var supplier = await _db.Suppliers.Where(x=>x.SupplierId == supplierId && x.CompanyId == CompanyId)
                .Select(x=> new SupplierSelectedInformationDto
                {
                    Name = x.Name,
                    Code = x.Code,
                    Phone = x.Phone,
                    Address = x.Address,
                    Picture = x.Picture
                }).FirstOrDefaultAsync();
            return supplier;
        }

        public async Task CreateSupplierAsync(Supplier supplier)
        { 
            await _db.Suppliers.AddAsync(supplier);  
        }
        public async Task UpdateSupplierAsync(Supplier supplier)
        {
             _db.Suppliers.Update(supplier);
        }
    }
}
