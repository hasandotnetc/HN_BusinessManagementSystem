using System;
using System.Collections.Generic;

namespace HN_Backend.Data;

public partial class EventNoGeneration
{
    public long EventNoGenerationId { get; set; }

    public string EventType { get; set; } = null!;

    public int EntryYear { get; set; }

    public string? Prefix { get; set; }

    public string? CompanyCode { get; set; }

    public string? LocationCode { get; set; }

    public int CurrentNumber { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime? UpdateOn { get; set; }

    public long? UpdateBy { get; set; }

    public long CompanyId { get; set; }

    public long LocationId { get; set; }

    public virtual Company Company { get; set; } = null!;

    public virtual Location Location { get; set; } = null!;

    public virtual LoginUser? UpdateByNavigation { get; set; }
}
