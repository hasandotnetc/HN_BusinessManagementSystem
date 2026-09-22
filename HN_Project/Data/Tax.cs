using System;
using System.Collections.Generic;

namespace HN_Backend.Data;

public partial class Tax
{
    public long TaxId { get; set; }

    public string TaxName { get; set; } = null!;

    public long EntryBy { get; set; }

    public DateTime CreateOn { get; set; }

    public virtual LoginUser EntryByNavigation { get; set; } = null!;

    public virtual ICollection<PurchaseOrderDetailTax> PurchaseOrderDetailTaxes { get; set; } = new List<PurchaseOrderDetailTax>();
}
