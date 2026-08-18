using System;
using System.Collections.Generic;

namespace HN_Backend.Data;

public partial class EventNoOrCodeGeneration
{
    public long EventNoOrCodeGenerationId { get; set; }

    public string EventType { get; set; } = null!;

    public int EntryYear { get; set; }

    public string? CurrentNumberOrCode { get; set; }

    public DateTime CreatedOn { get; set; }

    public long LocationId { get; set; }

    public virtual Location Location { get; set; } = null!;
}
