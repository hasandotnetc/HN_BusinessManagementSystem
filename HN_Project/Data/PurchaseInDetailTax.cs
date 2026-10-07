using System;
using System.Collections.Generic;

namespace HN_Backend.Data;

public partial class PurchaseInDetailTax
{
    public Guid PurchaseInDetailTaxId { get; set; }

    public Guid PurchaseInDetailId { get; set; }

    public Guid PurchaseInId { get; set; }

    public long TaxId { get; set; }

    public decimal? TaxAmount { get; set; }

    public string? TaxOn { get; set; }

    public DateTime CreateOn { get; set; }

    public virtual PurchaseIn PurchaseIn { get; set; } = null!;

    public virtual PurchaseInDetail PurchaseInDetail { get; set; } = null!;

    public virtual Tax Tax { get; set; } = null!;
}
