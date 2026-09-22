namespace HN_Backend.DTOs.PaginationDto
{
    public class ProductUnitTypeConversionLoadGridDto
    {
        public long ProductId { get; set; }
        public string ProductName { get; set; }
        public long BaseUnitTypeId { get; set; }
        public string BaseUnitTypeName { get; set; } 
        public long UnitTypeId { get; set; }
        public string UnitTypeName { get; set; }
        public decimal ConversionToUnitType { get; set; }
        public bool IsPurchaseAllowed { get; set; }
        public bool IsSaleAllowed { get; set; }
        public bool IsDefaultPurchase { get; set; }
        public bool IsDefaultSale { get; set; }
        public bool IsActive { get; set; } 
    }
}
