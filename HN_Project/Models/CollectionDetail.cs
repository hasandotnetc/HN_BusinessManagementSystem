using System;
using System.Collections.Generic;

namespace HN_Backend.Models;

public partial class CollectionDetail
{
    public long CollectionDetailId { get; set; }

    public long CollectionId { get; set; }

    public long? BankId { get; set; }

    public string? CollectionReference { get; set; }

    public decimal? Amount { get; set; }

    public DateTime CreateOn { get; set; }

    public virtual Bank? Bank { get; set; }

    public virtual Collection Collection { get; set; } = null!;
}
