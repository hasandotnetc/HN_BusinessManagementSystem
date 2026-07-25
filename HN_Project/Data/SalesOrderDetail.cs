using System;
using System.Collections.Generic;

namespace HN_Backend.Data;

public partial class SalesOrderDetail
{
    public long SalesOrderDetailId { get; set; }

    public long SalesOrderId { get; set; }

    public long ProductId { get; set; }

    public decimal Price { get; set; }

    public decimal Discount { get; set; }

    public decimal Quantity { get; set; }

    public decimal Cost { get; set; }

    public DateTime CreateOn { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual SalesOrder SalesOrder { get; set; } = null!;

    public virtual ICollection<SalesOrderSerial> SalesOrderSerials { get; set; } = new List<SalesOrderSerial>();
}
