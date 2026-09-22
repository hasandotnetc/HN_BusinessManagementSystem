namespace HN_Backend.DTOs.PurchaseOrder
{
    public class PurchaseOrderDetailCreateDto  
    { 
        public long ProductId { get; set; }
        public decimal Quantity { get; set; }
        public long UnitTypeId { get; set; }
        public decimal Cost { get; set; }
        public List<PurchaseOrderDetailTaxCreateDto> Taxes { get; set; }  = new();
    }
}
