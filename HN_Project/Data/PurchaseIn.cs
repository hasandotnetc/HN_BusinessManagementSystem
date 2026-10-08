using System;
using System.Collections.Generic;

namespace HN_Backend.Data;

public partial class PurchaseIn
{
    public Guid PurchaseInId { get; set; }

    public string PurchaseInNo { get; set; } = null!;

    public string? ReferenceNo { get; set; }

    public long SupplierId { get; set; }

    public DateTime Date { get; set; }

    public long PaymentMethodId { get; set; }

    public string? Remarks { get; set; }

    public decimal? AdditionalCost { get; set; }

    public decimal? Amount { get; set; }

    public decimal? Discount { get; set; }

    public string? DiscountType { get; set; }

    public decimal TotalAmount { get; set; }

    public decimal? PreviousDeu { get; set; }

    public string? Approved { get; set; }

    public long? ApprovedBy { get; set; }

    public DateTime? ApprovedTime { get; set; }

    public string? Cancelled { get; set; }

    public long? CancelledBy { get; set; }

    public DateTime? CancelledTime { get; set; }

    public string? Updated { get; set; }

    public long? UpdatedBy { get; set; }

    public DateTime? UpdatedTime { get; set; }

    public Guid? VoucherId { get; set; }

    public long LocationId { get; set; }

    public long CompanyId { get; set; }

    public long EntryBy { get; set; }

    public DateTime CreateOn { get; set; }

    public virtual LoginUser? ApprovedByNavigation { get; set; }

    public virtual LoginUser? CancelledByNavigation { get; set; }

    public virtual Company Company { get; set; } = null!;

    public virtual LoginUser EntryByNavigation { get; set; } = null!;

    public virtual Location Location { get; set; } = null!;

    public virtual PaymentMethod PaymentMethod { get; set; } = null!;

    public virtual ICollection<PurchaseInDetailSerial> PurchaseInDetailSerials { get; set; } = new List<PurchaseInDetailSerial>();

    public virtual ICollection<PurchaseInDetailTax> PurchaseInDetailTaxes { get; set; } = new List<PurchaseInDetailTax>();

    public virtual ICollection<PurchaseInDetail> PurchaseInDetails { get; set; } = new List<PurchaseInDetail>();

    public virtual Supplier Supplier { get; set; } = null!;

    public virtual LoginUser? UpdatedByNavigation { get; set; }

    public virtual Voucher? Voucher { get; set; }
}
