using System;
using System.Collections.Generic;

namespace HN_Backend.Data;

public partial class Company
{
    public long CompanyId { get; set; }

    public string Name { get; set; } = null!;

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public string? WebsiteLink { get; set; }

    public string? OwnerName { get; set; }

    public DateTime OpeningDate { get; set; }

    public DateTime? LastClosingDate { get; set; }

    public string? CompanyLogo { get; set; }

    public DateTime CreatedOn { get; set; }

    public virtual ICollection<Location> Locations { get; set; } = new List<Location>();

    public virtual ICollection<LoginUser> LoginUsers { get; set; } = new List<LoginUser>();
}
