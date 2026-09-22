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

    public string Address { get; set; } = null!;

    public string? Picture { get; set; }

    public DateTime CreateOn { get; set; }

    public long EntryBy { get; set; }

    public DateTime? UpdateOn { get; set; }

    public long? UpdateBy { get; set; }

    public decimal OpeningBalance { get; set; }

    public long? CompanyId { get; set; }

    public string? ActiveStatus { get; set; }

    public virtual Company? Company { get; set; }

    public virtual ICollection<CurrentStock> CurrentStocks { get; set; } = new List<CurrentStock>();

    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();

    public virtual LoginUser EntryByNavigation { get; set; } = null!;

    public virtual ICollection<PurchaseOrder> PurchaseOrders { get; set; } = new List<PurchaseOrder>();

    public virtual LoginUser? UpdateByNavigation { get; set; }
}
