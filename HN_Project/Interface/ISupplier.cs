using HN_Backend.Data;
using HN_Backend.DTOs.Supplier;

namespace HN_Backend.Interface
{
    public interface ISupplier
    {
        public Task<List<SupplierAutocompleteDto>> GetSupplierByCodeNamePhone(string objParam,long CompanyId);
        public Task CreateSupplierAsync(Supplier supplier);
        public Task<Supplier> GetSupplierByIdAsync(long supplierId, long companyId);
        public Task<SupplierSelectedInformationDto> GetSupplierInformationById(long supplierId, long companyId);
        public Task UpdateSupplierAsync(Supplier supplier);
    }
}
