using System.ComponentModel.DataAnnotations;

namespace HN_Backend.DTOs.Customer
{
    public class CustomerUpdateDto
    {
        public long CustomerId { get; set; }
        [Required]         
        public string Code { get; set; } = null!;
        [StringLength(500,ErrorMessage ="Name can not more than 500 char.")]
        public string Name { get; set; } = null!; 
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string Address { get; set; } = null!;
        public string? Nid { get; set; }
        public string? Picture { get; set; }
        public IFormFile? ProductImage { get; set; }
        public string? ActiveStatus { get; set; }
        public bool IsOwnCompanyCustomer { get; set; }
        public long CustomerGroupId { get; set; }
    }
}
