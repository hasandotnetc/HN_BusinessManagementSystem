using System.ComponentModel.DataAnnotations;

namespace HN_Backend.DTOs.CustomerGroup
{
    public class CustomerGroupCreateDto
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(500)]
        public string Name { get; set; } = null!;
        public string? Code { get; set; }
        public long CompanyId { get; set; }
        public long EntryBy { get; set; }
        public DateTime CreateOn { get; set; }
    }
}
