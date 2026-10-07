using HN_Backend.Data;

namespace HN_Backend.Interface
{
    public interface IPurchase
    {
        Task CreatePurchaseIn(PurchaseIn purchaseIn);
        Task CreatePurchaseInDetail(PurchaseInDetail purchaseInDetail);
        Task CreatePurchaseInDetailSerial(PurchaseInDetailSerial purchaseInDetailSerial);
        Task CreatePurchaseInDetailTax(PurchaseInDetailTax purchaseInDetailTax);
    }
}
