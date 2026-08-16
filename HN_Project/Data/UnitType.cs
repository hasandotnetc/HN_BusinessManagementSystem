using System;
using System.Collections.Generic;

namespace HN_Backend.Data;

public partial class UnitType
{
    public long UnitTypeId { get; set; }

    public string? Name { get; set; }

    public DateTime CreateOn { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
