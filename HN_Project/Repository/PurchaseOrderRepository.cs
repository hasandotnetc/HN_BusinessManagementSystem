using HN_Backend.Data;
using HN_Backend.DTOs.PurchaseOrder;
using HN_Backend.Interface;

namespace HN_Backend.Repository
{
    public class PurchaseOrderRepository : IPurchaseOrder
    {
        private readonly ApplicationDbContext _db;
        public PurchaseOrderRepository(ApplicationDbContext db)
        {
            _db=db;
        }
        public async Task CreatePurchaseOrder(PurchaseOrder purchaseOrderDto)
        {
             await _db.PurchaseOrders.AddAsync(purchaseOrderDto);
        }
        public async Task CreatePurchaseOrderDetail(PurchaseOrderDetail purchaseOrderDetailDto)
        {
            await _db.PurchaseOrderDetails.AddAsync(purchaseOrderDetailDto);
        }
        public async Task CreatePurchaseOrderDetailTax(PurchaseOrderDetailTax purchaseOrderDetailTaxDto)
        {
            await _db.PurchaseOrderDetailTaxes.AddAsync(purchaseOrderDetailTaxDto);
        }

        public async Task UpdatePurchaseOrder(PurchaseOrder purchaseOrderDto)
        {
             _db.PurchaseOrders.Update(purchaseOrderDto);
        }
        public async Task UpdatePurchaseOrderDetail(PurchaseOrderDetail purchaseOrderDetailDto)
        {
             _db.PurchaseOrderDetails.Update(purchaseOrderDetailDto);
        }
        public async Task UpdatePurchaseOrderDetailTax(PurchaseOrderDetailTax purchaseOrderDetailTaxDto)
        {
             _db.PurchaseOrderDetailTaxes.Update(purchaseOrderDetailTaxDto);
        }
    }
}
