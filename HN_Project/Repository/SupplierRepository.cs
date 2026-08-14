using HN_Backend.Data;
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

        public async Task<List<Supplier>> GetSupplierByCodeNamePhone(string objParam)
        {
            return await _db.Suppliers
                .Where(s => s.Code.Contains(objParam) || s.Name.Contains(objParam) || (s.Phone != null && s.Phone.Contains(objParam)))
                .Take(10).ToListAsync();
        }
        public async Task<string> SaveSupplier(Supplier supplier)
        { 
            _db.Suppliers.Add(supplier); 
            await _db.SaveChangesAsync();
            return supplier.Code;
        }
    }
}
