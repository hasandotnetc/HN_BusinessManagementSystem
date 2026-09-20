namespace HN_Backend.DTOs.PaginationDto
{
    public class CustomerPaginationDto
    {
        public long CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerCode { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string ActiveStatus { get; set; }
        public long CustomerGroupId { get; set; }
        public string CustomerGroupName { get; set; } 
        public string Picture { get; set; }
        public string NID { get; set; }
        public string SupplierAvailable { get; set; }
        public bool IsOwnCompanyCustomer { get; set; }
        public decimal OpeningBalance { get; set; }
    }
}
