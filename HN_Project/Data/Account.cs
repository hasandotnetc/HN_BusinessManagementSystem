using System;
using System.Collections.Generic;

namespace HN_Backend.Data;

public partial class Account
{
    public long AccountsId { get; set; }

    public long SubsidiaryAccountsId { get; set; }

    public long ControlAccountsId { get; set; }

    public long AccountsSubGroupId { get; set; }

    public long AccountsGroupId { get; set; }

    public string Code { get; set; } = null!;

    public string Name { get; set; } = null!;

    public DateTime OpeningDate { get; set; }

    public decimal? OpeningBalance { get; set; }

    public string BalanceType { get; set; } = null!;

    public string? StatementType { get; set; }

    public string? Notes { get; set; }

    public string? ActiveStatus { get; set; }

    public long LocationId { get; set; }

    public long Company { get; set; }

    public long EntryBy { get; set; }

    public DateTime CreateOn { get; set; }

    public virtual AccountsGroup AccountsGroup { get; set; } = null!;

    public virtual AccountsSubGroup AccountsSubGroup { get; set; } = null!;

    public virtual Company CompanyNavigation { get; set; } = null!;

    public virtual ControlAccount ControlAccounts { get; set; } = null!;

    public virtual LoginUser EntryByNavigation { get; set; } = null!;

    public virtual Location Location { get; set; } = null!;

    public virtual ICollection<PostedTransaction> PostedTransactions { get; set; } = new List<PostedTransaction>();

    public virtual SubsidiaryAccount SubsidiaryAccounts { get; set; } = null!;

    public virtual ICollection<VoucherDetail> VoucherDetails { get; set; } = new List<VoucherDetail>();
}
