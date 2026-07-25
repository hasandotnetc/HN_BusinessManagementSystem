using System;
using System.Collections.Generic;

namespace HN_Backend.Data;

public partial class CurrentStock
{
    public long CurrentStockId { get; set; }

    public long ProductId { get; set; }

    public double Quantity { get; set; }

    public decimal Cost { get; set; }

    public long LocationId { get; set; }

    public string StockInType { get; set; } = null!;

    public string? StockInTypeRefNo { get; set; }

    public long SupplierId { get; set; }

    public DateTime CreateOn { get; set; }

    public long EntryBy { get; set; }

    public virtual ICollection<CurrentStockDetail> CurrentStockDetails { get; set; } = new List<CurrentStockDetail>();

    public virtual LoginUser EntryByNavigation { get; set; } = null!;

    public virtual Location Location { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;

    public virtual Supplier Supplier { get; set; } = null!;
}
