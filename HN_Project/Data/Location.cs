using System;
using System.Collections.Generic;

namespace HN_Backend.Data;

public partial class Location
{
    public long LocationId { get; set; }

    public string Code { get; set; } = null!;

    public string Name { get; set; } = null!;

    public DateTime CreateOn { get; set; }

    public long CompanyId { get; set; }

    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();

    public virtual ICollection<AccountsGroup> AccountsGroups { get; set; } = new List<AccountsGroup>();

    public virtual ICollection<AccountsSubGroup> AccountsSubGroups { get; set; } = new List<AccountsSubGroup>();

    public virtual ICollection<Collection> Collections { get; set; } = new List<Collection>();

    public virtual Company Company { get; set; } = null!;

    public virtual ICollection<ControlAccount> ControlAccounts { get; set; } = new List<ControlAccount>();

    public virtual ICollection<CurrentStock> CurrentStocks { get; set; } = new List<CurrentStock>();

    public virtual ICollection<EventNoGeneration> EventNoGenerations { get; set; } = new List<EventNoGeneration>();

    public virtual ICollection<LoginUser> LoginUsers { get; set; } = new List<LoginUser>();

    public virtual ICollection<PostedTransaction> PostedTransactions { get; set; } = new List<PostedTransaction>();

    public virtual ICollection<PurchaseInDetail> PurchaseInDetails { get; set; } = new List<PurchaseInDetail>();

    public virtual ICollection<PurchaseIn> PurchaseIns { get; set; } = new List<PurchaseIn>();

    public virtual ICollection<PurchaseOrder> PurchaseOrderDeliveryToNavigations { get; set; } = new List<PurchaseOrder>();

    public virtual ICollection<PurchaseOrder> PurchaseOrderLocations { get; set; } = new List<PurchaseOrder>();

    public virtual ICollection<SalesOrder> SalesOrders { get; set; } = new List<SalesOrder>();

    public virtual ICollection<SubsidiaryAccount> SubsidiaryAccounts { get; set; } = new List<SubsidiaryAccount>();

    public virtual ICollection<Voucher> Vouchers { get; set; } = new List<Voucher>();
}
