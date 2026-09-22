using HN_Backend.Data;
using HN_Backend.Models;
using System.ComponentModel.DataAnnotations;

namespace HN_Backend.DTOs.Customer
{
    public class CustomerCreateDto
    {
        [Required]
        [StringLength(500, ErrorMessage = "Name cannot exceed 500 characters.")]
        public string Name { get; set; } = null!; 
        public string? Phone { get; set; }
        [EmailAddress]
        public string? Email { get; set; } 
        public string Address { get; set; } = null!; 
        public string? Nid { get; set; } 
        public decimal OpeningBalance { get; set; } 
        //public decimal TotalSales { get; set; } 
        public IFormFile? CustomerImage { get; set; }
        public string? Picture { get; set; } 
        //public decimal CollectionAmount { get; set; } 
        public string? ActiveStatus { get; set; } 
        public long? SupplierId { get; set; }
        public bool IsOwnCompanyCustomer { get; set; }
        public long CustomerGroupId { get; set; }
        public bool CombineWithSupplier { get; set; } 


    }
}
