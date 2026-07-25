using System;
using System.Collections.Generic;

namespace HN_Backend.Data;

public partial class CurrentStockDetail
{
    public long CurrentStockDetailId { get; set; }

    public long CurrentStockId { get; set; }

    public string SerialNo { get; set; } = null!;

    public virtual CurrentStock CurrentStock { get; set; } = null!;
}
