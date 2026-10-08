using System;
using System.Collections.Generic;

namespace HN_Backend.Data;

public partial class PurchaseInDetail
{
    public Guid PurchaseInDetailId { get; set; }

    public Guid PurchaseInId { get; set; }

    public long ProductId { get; set; }

    public long UnitTypeId { get; set; }

    public decimal Quantity { get; set; }

    public decimal Cost { get; set; }

    public decimal? AdditionalCost { get; set; }

    public decimal? PurchaseInAdditionalCost { get; set; }

    public decimal? DiscountAmount { get; set; }

    public string? DiscountType { get; set; }

    public decimal? PurchaseInDiscount { get; set; }

    public string? PurchaseInDiscountType { get; set; }

    public long? LocationId { get; set; }

    public string? BatchNo { get; set; }

    public string? LotNo { get; set; }

    public DateTime? ExpiryDate { get; set; }

    public DateTime? ManufactureDate { get; set; }

    public virtual Location? Location { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual PurchaseIn PurchaseIn { get; set; } = null!;

    public virtual ICollection<PurchaseInDetailSerial> PurchaseInDetailSerials { get; set; } = new List<PurchaseInDetailSerial>();

    public virtual ICollection<PurchaseInDetailTax> PurchaseInDetailTaxes { get; set; } = new List<PurchaseInDetailTax>();

    public virtual UnitType UnitType { get; set; } = null!;
}
