using System;
using System.Collections.Generic;

namespace HN_Backend.Data;

public partial class Customer
{
    public long CustomerId { get; set; }

    public string Code { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public string Address { get; set; } = null!;

    public string? Nid { get; set; }

    public decimal OpeningBalance { get; set; }

    public decimal TotalSales { get; set; }

    public string? Picture { get; set; }

    public DateTime CreateOn { get; set; }

    public long EntryBy { get; set; }

    public decimal CollectionAmount { get; set; }

    public long CompanyId { get; set; }

    public string ActiveStatus { get; set; } = null!;

    public long? SupplierId { get; set; }

    public bool IsOwnCompanyCustomer { get; set; }

    public DateTime? UpdateOn { get; set; }

    public long CustomerGroupId { get; set; }

    public long? UpdateBy { get; set; }

    public virtual ICollection<Collection> Collections { get; set; } = new List<Collection>();

    public virtual Company Company { get; set; } = null!;

    public virtual CustomerGroup CustomerGroup { get; set; } = null!;

    public virtual LoginUser EntryByNavigation { get; set; } = null!;

    public virtual ICollection<SalesOrder> SalesOrders { get; set; } = new List<SalesOrder>();

    public virtual Supplier? Supplier { get; set; }

    public virtual LoginUser? UpdateByNavigation { get; set; }
}
