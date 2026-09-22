namespace HN_Backend.DTOs.UnitType
{
    public class UnitTypeConversionCreateDto
    {
        public long ProductId { get; set; }
        public long UnitTypeId { get; set; }
        public decimal ConversionToUnitType { get; set; }
        public bool IsPurchaseAllowed { get; set; }
        public bool IsSaleAllowed { get; set; }
        public bool IsDefaultPurchase { get; set; }
        public bool IsDefaultSale { get; set; }
        public bool IsActive { get; set; } 
    }
}
