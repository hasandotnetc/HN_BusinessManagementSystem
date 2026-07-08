using System;
using System.Collections.Generic;

namespace HN_Backend.Models;

public partial class Collection
{
    public long CollectionId { get; set; }

    public string? CollectionNo { get; set; }

    public long CollectedBy { get; set; }

    public string? CollectionAgainst { get; set; }

    public long CustomerId { get; set; }

    public long SalesOrderId { get; set; }

    public long LocationId { get; set; }

    public string? Remarks { get; set; }

    public DateTime CreateOn { get; set; }

    public virtual Employee CollectedByNavigation { get; set; } = null!;

    public virtual ICollection<CollectionDetail> CollectionDetails { get; set; } = new List<CollectionDetail>();

    public virtual Customer Customer { get; set; } = null!;

    public virtual Location Location { get; set; } = null!;

    public virtual SalesOrder SalesOrder { get; set; } = null!;
}
