using HN_Backend.Data;
using HN_Backend.Interface;

namespace HN_Backend.Repository
{
    public class PurchaseRepository : IPurchase
    {
        private readonly ApplicationDbContext _db;
        public PurchaseRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task CreatePurchaseIn(PurchaseIn _pIn)
        {
            await _db.PurchaseIns.AddAsync(_pIn);
        }
        public async Task CreatePurchaseInDetail(PurchaseInDetail _pInDetail)
        {
            await _db.PurchaseInDetails.AddAsync(_pInDetail);
        }
        public async Task CreatePurchaseInDetailSerial(PurchaseInDetailSerial _pInDetailSerial)
        {
            await _db.PurchaseInDetailSerials.AddAsync(_pInDetailSerial);
        }
        public async Task CreatePurchaseInDetailTax(PurchaseInDetailTax _pInDetailTax)
        {
            await _db.PurchaseInDetailTaxes.AddAsync(_pInDetailTax);
        }
    }
}
