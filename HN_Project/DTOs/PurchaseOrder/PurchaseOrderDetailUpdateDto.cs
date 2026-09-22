namespace HN_Backend.DTOs.PurchaseOrder
{
    public class PurchaseOrderDetailUpdateDto
    {
        public long PurchaseOrderDetailId { get; set; }
        public long PurchaseOrderId { get; set; }
        public long ProductId { get; set; }
        public decimal Quantity { get; set; }
        public long UnitTypeId { get; set; }
        public decimal Cost { get; set; } 

    }
}
