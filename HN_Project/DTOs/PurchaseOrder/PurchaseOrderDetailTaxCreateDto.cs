namespace HN_Backend.DTOs.PurchaseOrder
{
    public class PurchaseOrderDetailTaxCreateDto
    {
        public long? TaxId { get; set; } 
        public decimal? TaxAmount { get; set; } 
        public string? TaxOn { get; set; } 
        public bool? IsIncluded { get; set; }
    }
}
