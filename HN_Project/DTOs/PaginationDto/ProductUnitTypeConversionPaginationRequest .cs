namespace HN_Backend.DTOs.PaginationDto
{
    public class ProductUnitTypeConversionPaginationRequest:PaginationRequest
    {
        public long? GroupId { get; set; }
        public long? CategoryId { get; set; }
        public long? BrandId { get; set; }
        public long? ProductId { get; set; }
        public long? UnitTypeId { get; set; }
    }
}
