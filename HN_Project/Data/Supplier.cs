using System;
using System.Collections.Generic;

namespace HN_Backend.Data;

public partial class Supplier
{
    public long SupplierId { get; set; }

    public string Code { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public string? Address { get; set; }

    public string? Picture { get; set; }

    public DateTime CreateOn { get; set; }

    public long EntryBy { get; set; }

    public virtual ICollection<CurrentStock> CurrentStocks { get; set; } = new List<CurrentStock>();

    public virtual LoginUser EntryByNavigation { get; set; } = null!;
}
