namespace HN_Backend.DTOs
{
    public class BrandVM
    {
        public long BrandId { get; set; } 
        public string Code { get; set; } 
        public string Name { get; set; } = null!; 
        public string? Picture { get; set; }  
        public IFormFile? BrandImage { get; set; } 
    }
}
