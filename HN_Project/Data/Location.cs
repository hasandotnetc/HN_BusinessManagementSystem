using System;
using System.Collections.Generic;

namespace HN_Backend.Data;

public partial class Location
{
    public long LocationId { get; set; }

    public string Code { get; set; } = null!;

    public string Name { get; set; } = null!;

    public DateTime CreateOn { get; set; }

    public virtual ICollection<Collection> Collections { get; set; } = new List<Collection>();

    public virtual ICollection<CurrentStock> CurrentStocks { get; set; } = new List<CurrentStock>();

    public virtual ICollection<EventNoOrCodeGeneration> EventNoOrCodeGenerations { get; set; } = new List<EventNoOrCodeGeneration>();

    public virtual ICollection<LoginUser> LoginUsers { get; set; } = new List<LoginUser>();

    public virtual ICollection<PaymentMethod> PaymentMethods { get; set; } = new List<PaymentMethod>();

    public virtual ICollection<SalesOrder> SalesOrders { get; set; } = new List<SalesOrder>();
}
