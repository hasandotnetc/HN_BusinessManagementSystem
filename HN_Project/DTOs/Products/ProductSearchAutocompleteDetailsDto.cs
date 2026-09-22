namespace HN_Backend.DTOs.Products
{
    public class ProductSearchAutocompleteDetailsDto
    {
        public long ProductId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Model { get; set; }
        public string SerialAvailable { get; set; }
        public string Picture { get; set; }
        public decimal StockUnit { get; set; }
    }
}
