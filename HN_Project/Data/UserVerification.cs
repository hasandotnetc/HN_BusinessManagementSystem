using System;
using System.Collections.Generic;

namespace HN_Backend.Data;

public partial class UserVerification
{
    public long UserVerificationId { get; set; }

    public long LoginUserId { get; set; }

    public string VerificationType { get; set; } = null!;

    public string VerificationCode { get; set; } = null!;

    public DateTime CreateOn { get; set; }

    public DateTime ExpiredDate { get; set; }

    public DateTime? VerifiedDate { get; set; }

    public bool IsVerified { get; set; }

    public int AttemptCount { get; set; }

    public bool IsUsed { get; set; }

    public bool IsActive { get; set; }

    public virtual LoginUser LoginUser { get; set; } = null!;
}
