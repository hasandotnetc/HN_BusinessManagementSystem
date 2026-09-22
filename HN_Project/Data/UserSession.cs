using System;
using System.Collections.Generic;

namespace HN_Backend.Data;

public partial class UserSession
{
    public long UserSessionId { get; set; }

    public long LoginUserId { get; set; }

    public string Jwtidentifier { get; set; } = null!;

    public string? RefreshToken { get; set; }

    public DateTime CreateOn { get; set; }

    public DateTime? ExpiryTime { get; set; }

    public DateTime? RevokedTime { get; set; }

    public bool IsRevoked { get; set; }

    public string? DeviceName { get; set; }

    public string? IpAddress { get; set; }

    public virtual LoginUser LoginUser { get; set; } = null!;
}
