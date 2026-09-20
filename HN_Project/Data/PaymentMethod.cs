using System;
using System.Collections.Generic;

namespace HN_Backend.Data;

public partial class PaymentMethod
{
    public long PaymentMethodId { get; set; }

    public string Name { get; set; } = null!;

    public long EntryBy { get; set; }

    public DateTime CreateOn { get; set; }

    public long CompanyId { get; set; }

    public string ActiveStatus { get; set; } = null!;

    public virtual PaymentMethod Company { get; set; } = null!;

    public virtual LoginUser EntryByNavigation { get; set; } = null!;

    public virtual ICollection<PaymentMethod> InverseCompany { get; set; } = new List<PaymentMethod>();

    public virtual ICollection<PurchaseOrder> PurchaseOrders { get; set; } = new List<PurchaseOrder>();

    public virtual ICollection<SalesOrder> SalesOrders { get; set; } = new List<SalesOrder>();
}
