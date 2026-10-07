using HN_Backend.Data;
using HN_Backend.DTOs.PurchaseOrder;

namespace HN_Backend.DTOs.PurchaseIn.CreateDto
{
    public class PurchaseInDetailCreateDto
    { 
        public long ProductId { get; set; } 
        public long UnitType { get; set; } 
        public decimal Quantity { get; set; } 
        public decimal Cost { get; set; } 
        public decimal? AdditionalCost { get; set; } 
        public decimal? PurchaseInAdditionalCost { get; set; } 
        public decimal? DiscountAmount { get; set; } 
        public string? DiscountType { get; set; } 
        public decimal? PurchaseInDiscount { get; set; } 
        public string? PurchaseInDiscountType { get; set; } 
        public long? LocationId { get; set; } 
        public string? BatchNo { get; set; } 
        public string? LotNo { get; set; } 
        public DateTime? ExpiryDate { get; set; } 
        public DateTime? ManufactureDate { get; set; }
        public List<PurchaseInDetailTaxDto> Taxes { get; set; } = new(); 
        public List<PurchaseInDetailSerialDto> PurchaseInDetailSerials { get; set; } = new();
    }
}
