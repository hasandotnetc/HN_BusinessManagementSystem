namespace HN_Backend.DTOs.UnitType
{
    public class UnitTypeConversionUpdateDto
    {
        public long ProductUnitTypeConversionId { get; set; } 
        public long ProductId { get; set; } 
        public long UnitTypeId { get; set; } 
        public decimal ConversionToUnitType { get; set; } 
        public bool IsPurchaseAllowed { get; set; } 
        public bool IsSaleAllowed { get; set; } 
        public bool IsDefaultPurchase { get; set; } 
        public bool IsDefaultSale { get; set; } 
        public bool IsActive { get; set; } 
        public long? UpdateBy { get; set; } 
        public DateTime? UpdateDate { get; set; } 
    }
}
