namespace HN_Backend.DTOs
{
    public class ProductPaginationVM
    {
        public long ProductId { get; set; }

        public string ProductName { get; set; }

        public string Model { get; set; }

        public string GroupName { get; set; }

        public string CategoryName { get; set; }

        public string BrandName { get; set; }

        public decimal Price { get; set; }

        public string StockType { get; set; }

        public string ImageUrl { get; set; }
    }
}
