using System;
using System.Collections.Generic;

namespace HN_Backend.Data;

public partial class PasswordResetOtp
{
    public long PasswordResetOtpId { get; set; }

    public long UserId { get; set; }

    public string OtpHash { get; set; } = null!;

    public DateTime ExpiresDate { get; set; }

    public bool IsUsed { get; set; }

    public DateTime CreatedOn { get; set; }

    public virtual LoginUser User { get; set; } = null!;
}
