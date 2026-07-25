using System;
using System.Collections.Generic;

namespace HN_Backend.Data;

public partial class Bank
{
    public long BankId { get; set; }

    public string BankName { get; set; } = null!;

    public string? Address { get; set; }

    public string? Phone { get; set; }

    public virtual ICollection<CollectionDetail> CollectionDetails { get; set; } = new List<CollectionDetail>();
}
