using System.ComponentModel.DataAnnotations;

namespace HN_Project.DTOs
{
    public class ProductVM
    {
        public long ProductId { get; set; }
        [Required(ErrorMessage = "Category is required.")]
        //[StringLength(100)]
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
        //[Range(0.01, 999999)]
        public decimal Price { get; set; }
        //[Range(0.01, 999999)]
        public decimal Discount { get; set; }
        //[Range(0.01, 999999)]
        public decimal Vat { get; set; }
        //[Range(0.01, 999999)]
        public decimal Tax { get; set; }

        public decimal Warranty { get; set; }

        public string? Picture { get; set; }

        //public DateTime? CreateOn { get; set; }
        public IFormFile? ProductImage { get; set; }

        public long EntryBy { get; set; } = 1;

    }
}
