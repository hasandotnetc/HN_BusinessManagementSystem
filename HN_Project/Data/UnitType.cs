using System;
using System.Collections.Generic;

namespace HN_Backend.Data;

public partial class UnitType
{
    public long UnitTypeId { get; set; }

    public string Name { get; set; } = null!;

    public DateTime CreateOn { get; set; }

    public virtual ICollection<ProductUnitTypeConversion> ProductUnitTypeConversions { get; set; } = new List<ProductUnitTypeConversion>();

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    public virtual ICollection<PurchaseOrderDetail> PurchaseOrderDetails { get; set; } = new List<PurchaseOrderDetail>();
}
