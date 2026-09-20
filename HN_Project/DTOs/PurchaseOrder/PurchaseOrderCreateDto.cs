using HN_Backend.Data;

namespace HN_Backend.DTOs.PurchaseOrder
{
    public class PurchaseOrderCreateDto  
    {
        public string? OrderRefNo { get; set; }
        public DateTime Podate { get; set; }
        public string PurchaseOrderType { get; set; } = null!;
        public string? OrderType { get; set; }
        public long SupplierId { get; set; }
        public string? SupplierContactPerson { get; set; }
        public string? SupplierContactNo { get; set; }
        public string? SupplierAddress { get; set; }
        public long? EmployeeId { get; set; }
        public long? DeliveryTo { get; set; }
        public string? ShipToAddress { get; set; }
        public string? ShipmentMode { get; set; }
        public string? PartialShipment { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public long? PaymentMethodId { get; set; }
        public DateTime? ExpectedPaymentReleaseDate { get; set; }
        public string? TermsAndCondition { get; set; } 
        public string? PaymentTerms { get; set; }
        public string? Remarks { get; set; }
        public List<PurchaseOrderDetailCreateDto> Details { get; set; } = new List<PurchaseOrderDetailCreateDto>();

        //public virtual ICollection<PurchaseOrderDetail> PurchaseOrderDetails { get; set; } = new List<PurchaseOrderDetail>();

    }
}
