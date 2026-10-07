namespace HN_Backend.DTOs.PurchaseIn.GetDto
{
    public class PurchaseInApprovalLoadingDto
    {
        public Guid PurchaseInId { get; set; }
        public string PurchaseInNo { get; set; } = string.Empty;
        public string SupplierName { get; set; } = string.Empty;
        public decimal TotalAmount { get; set; }
        public string LocationName { get; set; } = string.Empty;

        // Child details collection
        public List<PurchaseInDetailApprovalLoadingDto> Details { get; set; } = new();
    }
}
