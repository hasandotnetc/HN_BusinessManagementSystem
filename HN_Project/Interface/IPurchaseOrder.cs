using HN_Backend.Data;
using HN_Backend.DTOs.PurchaseOrder;

namespace HN_Backend.Interface
{
    public interface IPurchaseOrder
    {
        Task CreatePurchaseOrder(PurchaseOrder purchase);
        Task CreatePurchaseOrderDetail(PurchaseOrderDetail purchaseOrderDetail);
        Task CreatePurchaseOrderDetailTax(PurchaseOrderDetailTax purchaseOrderDetailTax);
        Task UpdatePurchaseOrder(PurchaseOrder purchase);
        Task UpdatePurchaseOrderDetail(PurchaseOrderDetail purchaseOrderDetail);
        Task UpdatePurchaseOrderDetailTax(PurchaseOrderDetailTax purchaseOrderDetailTax);
    }
}
