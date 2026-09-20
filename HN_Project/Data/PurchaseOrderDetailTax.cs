using System;
using System.Collections.Generic;

namespace HN_Backend.Data;

public partial class PurchaseOrderDetailTax
{
    public long PurchaseOrderDetailTaxId { get; set; }

    public long PurchaseOrderDetailId { get; set; }

    public long TaxId { get; set; }

    public decimal? TaxAmount { get; set; }

    public string? TaxOn { get; set; }

    public bool? IsIncluded { get; set; }

    public DateTime CreateOn { get; set; }

    public virtual PurchaseOrderDetail PurchaseOrderDetail { get; set; } = null!;

    public virtual Tax Tax { get; set; } = null!;
}
