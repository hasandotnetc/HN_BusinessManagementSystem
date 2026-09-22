namespace HN_Backend.DTOs
{
    public class ProductPaginationVM
    {
        public long ProductId { get; set; } 
        public string ProductName { get; set; } 
        public string Model { get; set; }
        public string Code { get; set; }
        public long UnitTypeId { get; set; }
        public string UnitTypeName { get; set; }
        public string ProductNote { get; set; }
        public long ProductGroupId { get; set; } 
        public string GroupName { get; set; }
        public long CategoryId { get; set; } 
        public string CategoryName { get; set; }
        public long BrandId { get; set; } 
        public string BrandName { get; set; }
        public string ProductType { get; set; } 
        public decimal VAT { get; set; }
        public string IsVatPercentageOrAmount { get; set; } 
        public decimal Warranty { get; set; } 
        public string SerialAvailable { get; set; }
        public string ActiveStatus { get; set; } 
        public string ImageUrl { get; set; }
    }
}
