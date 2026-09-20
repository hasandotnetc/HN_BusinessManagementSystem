using System;
using System.Collections.Generic;

namespace HN_Backend.Data;

public partial class CustomerGroup
{
    public long CustomerGroupId { get; set; }

    public string Name { get; set; } = null!;

    public string? Code { get; set; }

    public long CompanyId { get; set; }

    public long EntryBy { get; set; }

    public DateTime CreateOn { get; set; }

    public virtual Company Company { get; set; } = null!;

    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();

    public virtual LoginUser EntryByNavigation { get; set; } = null!;
}
