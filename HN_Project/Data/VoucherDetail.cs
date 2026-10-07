using System;
using System.Collections.Generic;

namespace HN_Backend.Data;

public partial class VoucherDetail
{
    public Guid VoucherDetailId { get; set; }

    public Guid VoucherId { get; set; }

    public string? Particulars { get; set; }

    public long AccountsId { get; set; }

    public decimal? Debit { get; set; }

    public decimal? Credit { get; set; }

    public virtual Account Accounts { get; set; } = null!;

    public virtual ICollection<PostedTransaction> PostedTransactions { get; set; } = new List<PostedTransaction>();

    public virtual Voucher Voucher { get; set; } = null!;
}
