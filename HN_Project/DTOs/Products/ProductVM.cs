using System.ComponentModel.DataAnnotations;

namespace HN_Backend.DTOs.Products
{
    public class ProductVM
    {
        public long ProductId { get; set; }
        [Required(ErrorMessage = "Category is required.")] 
        public long CategoryId { get; set; }
        [Required(ErrorMessage = "Brand is required.")] 
        public long BrandId { get; set; }
        [Required(ErrorMessage = "Group is required.")]
        public long GroupId { get; set; }  
        public string? Code { get; set; }
        [Required(ErrorMessage = "Product is required.")]
        [StringLength(500)]
        public string Name { get; set; } = null!;
        public string? Model { get; set; } 
        public string? SerialAvailable { get; set; }  
        public decimal VAT { get; set; } 
        public decimal Warranty { get; set; } 
        public string? Picture { get; set; }
        public IFormFile? ProductImage { get; set; }
        public long EntryBy { get; set; } 
        public long UnitTypeId { get; set; }
        public string? ProductType { get; set; }
        public string? Note { get; set; }
        public string? VatMode { get; set; }
        public string? IsActive { get;set; } 

    }
}
