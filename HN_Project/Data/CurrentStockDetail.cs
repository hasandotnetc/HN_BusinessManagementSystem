using System;
using System.Collections.Generic;

namespace HN_Backend.Data;

public partial class CurrentStockDetail
{
    public Guid CurrentStockDetailId { get; set; }

    public Guid CurrentStockId { get; set; }

    public string SerialNo { get; set; } = null!;

    public virtual CurrentStock CurrentStock { get; set; } = null!;
}
