using System;
using System.Collections.Generic;

namespace HN_Backend.Data;

public partial class Voucher
{
    public Guid VoucherId { get; set; }

    public string VoucherNo { get; set; } = null!;

    public DateTime Date { get; set; }

    public string? Description { get; set; }

    public string? VoucherType { get; set; }

    public string? ReferenceNo { get; set; }

    public long PrepareBy { get; set; }

    public bool? Posted { get; set; }

    public long? PostedBy { get; set; }

    public DateTime? PostedDate { get; set; }

    public bool? Cancelled { get; set; }

    public long? CancelledBy { get; set; }

    public DateTime? CancelledDate { get; set; }

    public string? Approved { get; set; }

    public long? ApprovedBy { get; set; }

    public DateTime? ApprovedDate { get; set; }

    public long LocationId { get; set; }

    public long Company { get; set; }

    public long EntryBy { get; set; }

    public DateTime CreateOn { get; set; }

    public virtual LoginUser? ApprovedByNavigation { get; set; }

    public virtual LoginUser? CancelledByNavigation { get; set; }

    public virtual Company CompanyNavigation { get; set; } = null!;

    public virtual LoginUser EntryByNavigation { get; set; } = null!;

    public virtual Location Location { get; set; } = null!;

    public virtual LoginUser? PostedByNavigation { get; set; }

    public virtual ICollection<PostedTransaction> PostedTransactions { get; set; } = new List<PostedTransaction>();

    public virtual LoginUser PrepareByNavigation { get; set; } = null!;

    public virtual ICollection<PurchaseIn> PurchaseIns { get; set; } = new List<PurchaseIn>();

    public virtual ICollection<VoucherDetail> VoucherDetails { get; set; } = new List<VoucherDetail>();
}
