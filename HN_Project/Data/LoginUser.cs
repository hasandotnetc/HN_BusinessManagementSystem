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

    public virtual ICollection<Customer> CustomerEntryByNavigations { get; set; } = new List<Customer>();

    public virtual ICollection<CustomerGroup> CustomerGroups { get; set; } = new List<CustomerGroup>();

    public virtual ICollection<Customer> CustomerUpdateByNavigations { get; set; } = new List<Customer>();

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    public virtual ICollection<EventCodeGeneration> EventCodeGenerations { get; set; } = new List<EventCodeGeneration>();

    public virtual ICollection<EventNoGeneration> EventNoGenerations { get; set; } = new List<EventNoGeneration>();

    public virtual Location Location { get; set; } = null!;

    public virtual ICollection<PasswordResetOtp> PasswordResetOtps { get; set; } = new List<PasswordResetOtp>();

    public virtual ICollection<PaymentMethod> PaymentMethods { get; set; } = new List<PaymentMethod>();

    public virtual ICollection<ProductGroup> ProductGroups { get; set; } = new List<ProductGroup>();

    public virtual ICollection<ProductUnitTypeConversion> ProductUnitTypeConversionEntryByNavigations { get; set; } = new List<ProductUnitTypeConversion>();

    public virtual ICollection<ProductUnitTypeConversion> ProductUnitTypeConversionUpdateByNavigations { get; set; } = new List<ProductUnitTypeConversion>();

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    public virtual ICollection<PurchaseOrder> PurchaseOrderApprovedByNavigations { get; set; } = new List<PurchaseOrder>();

    public virtual ICollection<PurchaseOrder> PurchaseOrderCancelledByNavigations { get; set; } = new List<PurchaseOrder>();

    public virtual ICollection<PurchaseOrder> PurchaseOrderEntryByNavigations { get; set; } = new List<PurchaseOrder>();

    public virtual ICollection<PurchaseOrder> PurchaseOrderUpdateByNavigations { get; set; } = new List<PurchaseOrder>();

    public virtual ICollection<SalesOrder> SalesOrders { get; set; } = new List<SalesOrder>();

    public virtual ICollection<Supplier> SupplierEntryByNavigations { get; set; } = new List<Supplier>();

    public virtual ICollection<Supplier> SupplierUpdateByNavigations { get; set; } = new List<Supplier>();

    public virtual ICollection<Tax> Taxes { get; set; } = new List<Tax>();

    public virtual ICollection<UserSession> UserSessions { get; set; } = new List<UserSession>();

    public virtual ICollection<UserVerification> UserVerifications { get; set; } = new List<UserVerification>();
}
