using System;
using System.Collections.Generic;

namespace HN_Backend.Data;

public partial class ProductUnitTypeConversion
{
    public long ProductUnitTypeConversionId { get; set; }

    public long ProductId { get; set; }

    public long UnitTypeId { get; set; }

    public decimal ConversionToUnitType { get; set; }

    public bool IsPurchaseAllowed { get; set; }

    public bool IsSaleAllowed { get; set; }

    public bool IsDefaultPurchase { get; set; }

    public bool IsDefaultSale { get; set; }

    public bool IsActive { get; set; }

    public long EntryBy { get; set; }

    public DateTime EntryDate { get; set; }

    public long? UpdateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public long CompanyId { get; set; }

    public virtual Company Company { get; set; } = null!;

    public virtual LoginUser EntryByNavigation { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;

    public virtual UnitType UnitType { get; set; } = null!;

    public virtual LoginUser? UpdateByNavigation { get; set; }
}
