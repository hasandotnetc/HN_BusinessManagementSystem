namespace HN_Shared.DTOs
{
    public class CustomerVM
    {
        public long CustomerId { get; set; }
        //public string Code { get; set; } 

        public decimal OpeningBalance { get; set; }

        public decimal TotalSales { get; set; }

        public decimal DueAmount { get; set; }
    }
}