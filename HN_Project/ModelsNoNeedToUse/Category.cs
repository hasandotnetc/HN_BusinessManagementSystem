using System;
using System.Collections.Generic;

namespace HN_Backend.Models;

public partial class Category
{
    public long CategoryId { get; set; }

    public string Code { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Picture { get; set; }

    public DateTime CreateOn { get; set; }

    public long EntryBy { get; set; }

    public virtual LoginUser EntryByNavigation { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
