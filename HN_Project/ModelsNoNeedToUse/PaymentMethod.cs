using System;
using System.Collections.Generic;

namespace HN_Backend.Models;

public partial class PaymentMethod
{
    public long PaymentMethodId { get; set; }

    public string Name { get; set; } = null!;

    public string? Picture { get; set; }

    public long LocationId { get; set; }

    public long EntryBy { get; set; }

    public DateTime CreateOn { get; set; }

    public virtual LoginUser EntryByNavigation { get; set; } = null!;

    public virtual Location Location { get; set; } = null!;

    public virtual ICollection<SalesOrder> SalesOrders { get; set; } = new List<SalesOrder>();
}
