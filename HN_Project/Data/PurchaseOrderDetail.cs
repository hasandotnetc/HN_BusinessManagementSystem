using System;
using System.Collections.Generic;

namespace HN_Backend.Data;

public partial class PurchaseOrderDetail
{
    public long PurchaseOrderDetailId { get; set; }

    public long PurchaseOrderId { get; set; }

    public long ProductId { get; set; }

    public decimal Quantity { get; set; }

    public long UnitTypeId { get; set; }

    public decimal Cost { get; set; }

    public DateTime CreateOn { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual PurchaseOrder PurchaseOrder { get; set; } = null!;

    public virtual ICollection<PurchaseOrderDetailTax> PurchaseOrderDetailTaxes { get; set; } = new List<PurchaseOrderDetailTax>();

    public virtual UnitType UnitType { get; set; } = null!;
}
