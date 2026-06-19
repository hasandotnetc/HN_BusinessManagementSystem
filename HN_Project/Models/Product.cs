using System;
using System.Collections.Generic;

namespace HN_Project.Models;

public partial class Product
{
    public long ProductId { get; set; }

    public long CategoryId { get; set; }

    public long BrandId { get; set; }

    public string Code { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Model { get; set; }

    public string SerialAvailable { get; set; } = null!;

    public decimal Price { get; set; }

    public decimal Discount { get; set; }

    public decimal Vat { get; set; }

    public decimal Tax { get; set; }

    public decimal Warranty { get; set; }

    public string? Picture { get; set; }

    public DateTime? CreateOn { get; set; }

    public long EntryBy { get; set; }

    public virtual Brand Brand { get; set; } = null!;

    public virtual Category Category { get; set; } = null!;

    public virtual ICollection<CurrentStock> CurrentStocks { get; set; } = new List<CurrentStock>();

    public virtual LoginUser EntryByNavigation { get; set; } = null!;

    public virtual ICollection<SalesOrderDetail> SalesOrderDetails { get; set; } = new List<SalesOrderDetail>();
}
