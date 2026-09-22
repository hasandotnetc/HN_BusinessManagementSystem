using System.ComponentModel.DataAnnotations;

namespace HN_Backend.DTOs.Supplier
{
    public class SupplierCreateDto
    {
        [StringLength(500)]
        public string Name { get; set; } = null!;
        public string? Phone { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        public string Address { get; set; } = null!;
        public IFormFile? ImageFile { get; set; }
        public decimal OpeningBalance { get; set; }

    }
}
