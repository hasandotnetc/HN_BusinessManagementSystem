using System;
using System.Collections.Generic;

namespace HN_Backend.Data;

public partial class ProductGroup
{
    public long ProductGroupId { get; set; }

    public string Name { get; set; } = null!;

    public DateTime CreateOn { get; set; }

    public long EntryBy { get; set; }

    public long CompanyId { get; set; }

    public virtual Company Company { get; set; } = null!;

    public virtual LoginUser EntryByNavigation { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
