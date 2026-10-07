namespace HN_Backend.DTOs.PurchaseIn.CreateDto
{
    public class PurchaseInDetailTaxDto
    {
        public long TaxId { get; set; } 
        public decimal? TaxAmount { get; set; } 
        public string? TaxOn { get; set; }
    }
}
