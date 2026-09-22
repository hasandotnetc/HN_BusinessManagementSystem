using System;
using System.Collections.Generic;

namespace HN_Backend.Models;

public partial class Customer
{
    public long CustomerId { get; set; }

    public string Code { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public string? Address { get; set; }

    public string? Nid { get; set; }

    public decimal OpeningBalance { get; set; }

    public decimal TotalSales { get; set; }

    public decimal DueAmount { get; set; }

    public string? Picture { get; set; }

    public DateTime CreateOn { get; set; }

    public long EntryBy { get; set; }

    public virtual ICollection<Collection> Collections { get; set; } = new List<Collection>();

    public virtual LoginUser EntryByNavigation { get; set; } = null!;

    public virtual ICollection<SalesOrder> SalesOrders { get; set; } = new List<SalesOrder>();
}
