using System;
using System.Collections.Generic;

namespace HN_Backend.Data;

public partial class PurchaseOrder
{
    public long PurchaseOrderId { get; set; }

    public string PurchaseOrderNo { get; set; } = null!;

    public string? OrderRefNo { get; set; }

    public DateTime Podate { get; set; }

    public string PurchaseOrderType { get; set; } = null!;

    public string? OrderType { get; set; }

    public long LocationId { get; set; }

    public long CompanyId { get; set; }

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

    public decimal TotalAmount { get; set; }

    public string ApprovalStatus { get; set; } = null!;

    public long? ApprovedBy { get; set; }

    public DateTime? ApprovedDate { get; set; }

    public bool Cancelled { get; set; }

    public long? CancelledBy { get; set; }

    public DateTime? CancelledDate { get; set; }

    public string? CancelRemarks { get; set; }

    public string? TermsAndCondition { get; set; }

    public string? PaymentTerms { get; set; }

    public string? Remarks { get; set; }

    public long EntryBy { get; set; }

    public DateTime CreateOn { get; set; }

    public long? UpdateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public virtual LoginUser? ApprovedByNavigation { get; set; }

    public virtual LoginUser? CancelledByNavigation { get; set; }

    public virtual Company Company { get; set; } = null!;

    public virtual Location? DeliveryToNavigation { get; set; }

    public virtual Employee? Employee { get; set; }

    public virtual LoginUser EntryByNavigation { get; set; } = null!;

    public virtual Location Location { get; set; } = null!;

    public virtual PaymentMethod? PaymentMethod { get; set; }

    public virtual ICollection<PurchaseOrderDetail> PurchaseOrderDetails { get; set; } = new List<PurchaseOrderDetail>();

    public virtual Supplier Supplier { get; set; } = null!;

    public virtual LoginUser? UpdateByNavigation { get; set; }
}
