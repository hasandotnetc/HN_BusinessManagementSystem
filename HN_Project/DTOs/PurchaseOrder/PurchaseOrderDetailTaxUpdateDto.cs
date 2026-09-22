namespace HN_Backend.DTOs.PurchaseOrder
{
    public class PurchaseOrderDetailTaxUpdateDto
    {
        public long PurchaseOrderDetailTaxId { get; set; }
        public long PurchaseOrderDetailId { get; set; }
        public long TaxId { get; set; }
        public decimal? TaxAmount { get; set; }
        public string? TaxOn { get; set; }
        public bool? IsIncluded { get; set; }
    }
}
