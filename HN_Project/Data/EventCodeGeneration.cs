using System;
using System.Collections.Generic;

namespace HN_Backend.Data;

public partial class EventCodeGeneration
{
    public long EventCodeGenerationId { get; set; }

    public string EventType { get; set; } = null!;

    public string? Prefix { get; set; }

    public string? CompanyCode { get; set; }

    public int CurrentNumber { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime? UpdateOn { get; set; }

    public long? UpdateBy { get; set; }

    public long CompanyId { get; set; }

    public virtual Company Company { get; set; } = null!;

    public virtual LoginUser? UpdateByNavigation { get; set; }
}
