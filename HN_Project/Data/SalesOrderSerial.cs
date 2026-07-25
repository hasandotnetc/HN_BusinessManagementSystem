using System;
using System.Collections.Generic;

namespace HN_Backend.Data;

public partial class SalesOrderSerial
{
    public long SalesOrderSerialId { get; set; }

    public long SalesOrderDetailId { get; set; }

    public long SalesOrderId { get; set; }

    public string? SerialNo { get; set; }

    public DateTime CreateOn { get; set; }

    public virtual SalesOrder SalesOrder { get; set; } = null!;

    public virtual SalesOrderDetail SalesOrderDetail { get; set; } = null!;
}
