using HN_Backend.DTOs.PurchaseOrder;

namespace HN_Backend.DTOs.PurchaseIn.CreateDto
{
    public class PurchaseInCreateDto
    {  
        public string? ReferenceNo { get; set; } 
        public long SupplierId { get; set; } 
        public DateTime Date { get; set; } 
        public long PaymentMethodId { get; set; } 
        public string? Remarks { get; set; } 
        public decimal? AdditionalCost { get; set; } 
        public decimal? Amount { get; set; } 
        public decimal? Discount { get; set; } 
        public string? DiscountType { get; set; } 
        public decimal TotalAmount { get; set; } 
        public decimal? PreviousDeu { get; set; }  
        public long LocationId { get; set; } 
        public long Company { get; set; } 
        public long EntryBy { get; set; }
        public List<PurchaseInDetailCreateDto> PurchaseInDetails { get; set; } = new List<PurchaseInDetailCreateDto>();
    }
}
