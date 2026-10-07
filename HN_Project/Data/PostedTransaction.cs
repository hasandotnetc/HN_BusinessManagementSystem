using System;
using System.Collections.Generic;

namespace HN_Backend.Data;

public partial class PostedTransaction
{
    public long PostedTransactionId { get; set; }

    public Guid VoucherId { get; set; }

    public Guid VoucherDetailId { get; set; }

    public DateTime Date { get; set; }

    public long AccountsId { get; set; }

    public decimal? Amount { get; set; }

    public string? BalanceType { get; set; }

    public long LocationId { get; set; }

    public long Company { get; set; }

    public long EntryBy { get; set; }

    public DateTime CreateOn { get; set; }

    public virtual Account Accounts { get; set; } = null!;

    public virtual Company CompanyNavigation { get; set; } = null!;

    public virtual LoginUser EntryByNavigation { get; set; } = null!;

    public virtual Location Location { get; set; } = null!;

    public virtual Voucher Voucher { get; set; } = null!;

    public virtual VoucherDetail VoucherDetail { get; set; } = null!;
}
