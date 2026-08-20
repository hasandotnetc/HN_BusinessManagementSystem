using System;
using System.Collections.Generic;

namespace HN_Backend.Data;

public partial class LoginUser
{
    public long LoginUserId { get; set; }

    public string Code { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string? Email { get; set; }

    public string? Address { get; set; }

    public string PasswordHash { get; set; } = null!;

    public string? Picture { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreateOn { get; set; }

    public DateTime? UpdateOn { get; set; }

    public long LocationId { get; set; }

    public string UserLevel { get; set; } = null!;

    public long CompanyId { get; set; }

    public virtual ICollection<Brand> Brands { get; set; } = new List<Brand>();

    public virtual ICollection<Category> Categories { get; set; } = new List<Category>();

    public virtual Company Company { get; set; } = null!;

    public virtual ICollection<CurrentStock> CurrentStocks { get; set; } = new List<CurrentStock>();

    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    public virtual Location Location { get; set; } = null!;

    public virtual ICollection<PasswordResetOtp> PasswordResetOtps { get; set; } = new List<PasswordResetOtp>();

    public virtual ICollection<PaymentMethod> PaymentMethods { get; set; } = new List<PaymentMethod>();

    public virtual ICollection<ProductGroup> ProductGroups { get; set; } = new List<ProductGroup>();

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    public virtual ICollection<SalesOrder> SalesOrders { get; set; } = new List<SalesOrder>();

    public virtual ICollection<Supplier> Suppliers { get; set; } = new List<Supplier>();

    public virtual ICollection<UserSession> UserSessions { get; set; } = new List<UserSession>();
}
