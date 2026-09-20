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

    public virtual ICollection<Brand> Brands { get; set; } = new List<Brand>();

    public virtual ICollection<Category> Categories { get; set; } = new List<Category>();

    public virtual ICollection<CustomerGroup> CustomerGroups { get; set; } = new List<CustomerGroup>();

    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    public virtual ICollection<EventCodeGeneration> EventCodeGenerations { get; set; } = new List<EventCodeGeneration>();

    public virtual ICollection<EventNoGeneration> EventNoGenerations { get; set; } = new List<EventNoGeneration>();

    public virtual ICollection<Location> Locations { get; set; } = new List<Location>();

    public virtual ICollection<LoginUser> LoginUsers { get; set; } = new List<LoginUser>();

    public virtual ICollection<ProductGroup> ProductGroups { get; set; } = new List<ProductGroup>();

    public virtual ICollection<ProductUnitTypeConversion> ProductUnitTypeConversions { get; set; } = new List<ProductUnitTypeConversion>();

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    public virtual ICollection<PurchaseOrder> PurchaseOrders { get; set; } = new List<PurchaseOrder>();

    public virtual ICollection<Supplier> Suppliers { get; set; } = new List<Supplier>();
}
