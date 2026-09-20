using HN_Backend.Data;

namespace HN_Backend.DTOs.UnitType
{
    public class UnitTypeConversionProductSetupCreateDto
    { 
        public long ProductId { get; set; } 
        public long UnitTypeId { get; set; } 
        public decimal ConversionToUnitType { get; set; } 
        public bool IsPurchaseAllowed { get; set; } 
        public bool IsSaleAllowed { get; set; } 
        public bool IsDefaultPurchase { get; set; } 
        public bool IsDefaultSale { get; set; } 
        public bool IsActive { get; set; } 
        public long EntryBy { get; set; } 
        public DateTime EntryDate { get; set; } 
        public long CompanyId { get; set; } 
        public virtual Product? Product { get; set; } 
    }
}
