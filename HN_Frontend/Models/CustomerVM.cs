namespace HN_Frontend.Models
{
    public class CustomerVM
    {
        public long CustomerId { get; set; }
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; } 
        public decimal OpeningBalance { get; set; } 
        public decimal TotalSales { get; set; } 
        public decimal DueAmount { get; set; } 
        public string? Picture { get; set; }

    }
}
