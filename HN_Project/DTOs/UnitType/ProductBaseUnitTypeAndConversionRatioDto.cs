namespace HN_Backend.DTOs.UnitType
{
    public class ProductBaseUnitTypeAndConversionRatioDto
    {
        public long ProductId { get; set; }
        public long BaseUnitTypeId { get; set; }
        public string BaseUnitTypeName { get; set; }
        public decimal BaseUnitConversionRatio { get; set; }
    }
}
