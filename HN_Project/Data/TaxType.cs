using System;
using System.Collections.Generic;

namespace HN_Backend.Data;

public partial class TaxType
{
    public long TaxTypeId { get; set; }

    public string TaxTypeName { get; set; } = null!;

    public DateTime CreateOn { get; set; }
}
