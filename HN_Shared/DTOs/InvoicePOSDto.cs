 
namespace HN_Shared.DTOs
{
    public class InvoicePOSDto
    {
        public CustomerVM Customer { get; set; }
        public EmployeeVM SalesPerson { get; set; }
        public PaymentOptionDto PaymentOption { get; set; }
        public SummaryDto Summary { get; set; }
        public List<ProductRowDto> ProductRowsAllInfo { get; set; }
        public List<SplitAmountDto> SplitAmounts { get; set; }
    }
}
