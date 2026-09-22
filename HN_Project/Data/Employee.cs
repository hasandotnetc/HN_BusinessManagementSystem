using System;
using System.Collections.Generic;

namespace HN_Backend.Data;

public partial class Employee
{
    public long EmployeeId { get; set; }

    public string Code { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public string? Nid { get; set; }

    public string? Picture { get; set; }

    public DateTime CreateOn { get; set; }

    public long EntryBy { get; set; }

    public string ActiveStatus { get; set; } = null!;

    public long CompanyId { get; set; }

    public virtual ICollection<Collection> Collections { get; set; } = new List<Collection>();

    public virtual Company Company { get; set; } = null!;

    public virtual LoginUser EntryByNavigation { get; set; } = null!;

    public virtual ICollection<PurchaseOrder> PurchaseOrders { get; set; } = new List<PurchaseOrder>();

    public virtual ICollection<SalesOrder> SalesOrders { get; set; } = new List<SalesOrder>();
}
