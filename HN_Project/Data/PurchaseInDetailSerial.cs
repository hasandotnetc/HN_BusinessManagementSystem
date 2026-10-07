using System;
using System.Collections.Generic;

namespace HN_Backend.Data;

public partial class PurchaseInDetailSerial
{
    public Guid PurchaseInDetailSerialId { get; set; }

    public Guid PurchaseInDetailId { get; set; }

    public Guid PurchaseInId { get; set; }

    public string? SerialNo { get; set; }

    public virtual PurchaseIn PurchaseIn { get; set; } = null!;

    public virtual PurchaseInDetail PurchaseInDetail { get; set; } = null!;
}
