using System;
using System.Collections.Generic;

namespace HN_Backend.Data;

public partial class ControlAccount
{
    public long ControlAccountsId { get; set; }

    public long AccountsSubGroupId { get; set; }

    public long AccountsGroupId { get; set; }

    public string Name { get; set; } = null!;

    public long LocationId { get; set; }

    public long Company { get; set; }

    public long EntryBy { get; set; }

    public DateTime CreateOn { get; set; }

    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();

    public virtual AccountsGroup AccountsGroup { get; set; } = null!;

    public virtual AccountsSubGroup AccountsSubGroup { get; set; } = null!;

    public virtual Company CompanyNavigation { get; set; } = null!;

    public virtual LoginUser EntryByNavigation { get; set; } = null!;

    public virtual Location Location { get; set; } = null!;

    public virtual ICollection<SubsidiaryAccount> SubsidiaryAccounts { get; set; } = new List<SubsidiaryAccount>();
}
