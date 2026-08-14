using HN_Backend.Data;

namespace HN_Backend.Interface
{
    public interface ISupplier
    {
        public Task<List<Supplier>> GetSupplierByCodeNamePhone(string objParam);
        public Task<string> SaveSupplier(Supplier supplier);
    }
}
