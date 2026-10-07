namespace HN_Backend.DTOs.PurchaseIn.GetDto
{
    public class PurchaseInDetailApprovalLoadingDto
    {
        public Guid PurchaseInDetailId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public decimal Quantity { get; set; }
        public decimal Cost { get; set; }
        public decimal TotalCost => Quantity * Cost;  
    }
}
