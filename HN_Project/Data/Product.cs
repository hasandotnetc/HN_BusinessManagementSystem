using System;
using System.Collections.Generic;

namespace HN_Backend.Data;

public partial class Product
{
    public long ProductId { get; set; }

    public long CategoryId { get; set; }

    public long BrandId { get; set; }

    public string Code { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Model { get; set; } = null!;

    public string SerialAvailable { get; set; } = null!;

    public string ProductType { get; set; } = null!;

    public long UnitTypeId { get; set; }

    public decimal Vat { get; set; }

    public decimal Warranty { get; set; }

    public string? Picture { get; set; }

    public DateTime CreateOn { get; set; }

    public long EntryBy { get; set; }

    public long GroupId { get; set; }

    public long CompanyId { get; set; }

    public string? IsVatPercentageOrAmount { get; set; }

    public string? ProductNote { get; set; }

    public string? ActiveStatus { get; set; }

    public virtual Brand Brand { get; set; } = null!;

    public virtual Category Category { get; set; } = null!;

    public virtual Company Company { get; set; } = null!;

    public virtual ICollection<CurrentStock> CurrentStocks { get; set; } = new List<CurrentStock>();

    public virtual LoginUser EntryByNavigation { get; set; } = null!;

    public virtual ProductGroup Group { get; set; } = null!;

    public virtual ICollection<ProductUnitTypeConversion> ProductUnitTypeConversions { get; set; } = new List<ProductUnitTypeConversion>();

    public virtual ICollection<PurchaseOrderDetail> PurchaseOrderDetails { get; set; } = new List<PurchaseOrderDetail>();

    public virtual ICollection<SalesOrderDetail> SalesOrderDetails { get; set; } = new List<SalesOrderDetail>();

    public virtual UnitType UnitType { get; set; } = null!;
}
