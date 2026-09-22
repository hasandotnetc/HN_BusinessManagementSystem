namespace HN_Backend.DTOs.ProductCategory
{
    public class ProductCategoryVM
    {
        public long CategoryId { get; set; } 
        public string? Code { get; set; }  
        public string Name { get; set; } = null!; 
        public string? Picture { get; set; }
        public IFormFile? CategoryImage { get; set; } 
    }
}
