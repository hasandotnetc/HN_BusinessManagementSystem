using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace HN_Backend.Data;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<AccountsGroup> AccountsGroups { get; set; }

    public virtual DbSet<AccountsSubGroup> AccountsSubGroups { get; set; }

    public virtual DbSet<Bank> Banks { get; set; }

    public virtual DbSet<Brand> Brands { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Collection> Collections { get; set; }

    public virtual DbSet<CollectionDetail> CollectionDetails { get; set; }

    public virtual DbSet<Company> Companies { get; set; }

    public virtual DbSet<ControlAccount> ControlAccounts { get; set; }

    public virtual DbSet<CurrentStock> CurrentStocks { get; set; }

    public virtual DbSet<CurrentStockDetail> CurrentStockDetails { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<CustomerGroup> CustomerGroups { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<EventCodeGeneration> EventCodeGenerations { get; set; }

    public virtual DbSet<EventNoGeneration> EventNoGenerations { get; set; }

    public virtual DbSet<Location> Locations { get; set; }

    public virtual DbSet<LoginUser> LoginUsers { get; set; }

    public virtual DbSet<PasswordResetOtp> PasswordResetOtps { get; set; }

    public virtual DbSet<PaymentMethod> PaymentMethods { get; set; }

    public virtual DbSet<PostedTransaction> PostedTransactions { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductGroup> ProductGroups { get; set; }

    public virtual DbSet<ProductUnitTypeConversion> ProductUnitTypeConversions { get; set; }

    public virtual DbSet<PurchaseIn> PurchaseIns { get; set; }

    public virtual DbSet<PurchaseInDetail> PurchaseInDetails { get; set; }

    public virtual DbSet<PurchaseInDetailSerial> PurchaseInDetailSerials { get; set; }

    public virtual DbSet<PurchaseInDetailTax> PurchaseInDetailTaxes { get; set; }

    public virtual DbSet<PurchaseOrder> PurchaseOrders { get; set; }

    public virtual DbSet<PurchaseOrderDetail> PurchaseOrderDetails { get; set; }

    public virtual DbSet<PurchaseOrderDetailTax> PurchaseOrderDetailTaxes { get; set; }

    public virtual DbSet<SalesOrder> SalesOrders { get; set; }

    public virtual DbSet<SalesOrderDetail> SalesOrderDetails { get; set; }

    public virtual DbSet<SalesOrderSerial> SalesOrderSerials { get; set; }

    public virtual DbSet<SubsidiaryAccount> SubsidiaryAccounts { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    public virtual DbSet<Tax> Taxes { get; set; }

    public virtual DbSet<UnitType> UnitTypes { get; set; }

    public virtual DbSet<UserSession> UserSessions { get; set; }

    public virtual DbSet<UserVerification> UserVerifications { get; set; }

    public virtual DbSet<Voucher> Vouchers { get; set; }

    public virtual DbSet<VoucherDetail> VoucherDetails { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=DefaultConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.AccountsId);

            entity.Property(e => e.ActiveStatus)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.BalanceType)
                .HasMaxLength(6)
                .IsUnicode(false);
            entity.Property(e => e.Code)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.CreateOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(250);
            entity.Property(e => e.Notes).HasMaxLength(500);
            entity.Property(e => e.OpeningBalance).HasColumnType("decimal(18, 3)");
            entity.Property(e => e.OpeningDate).HasColumnType("datetime");
            entity.Property(e => e.StatementType)
                .HasMaxLength(15)
                .IsUnicode(false);

            entity.HasOne(d => d.AccountsGroup).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.AccountsGroupId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Accounts_AccountsGroup");

            entity.HasOne(d => d.AccountsSubGroup).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.AccountsSubGroupId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Accounts_AccountsSubGroup");

            entity.HasOne(d => d.CompanyNavigation).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.Company)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Accounts_Company");

            entity.HasOne(d => d.ControlAccounts).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.ControlAccountsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Accounts_ControlAccounts");

            entity.HasOne(d => d.EntryByNavigation).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.EntryBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Accounts_LoginUser");

            entity.HasOne(d => d.Location).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.LocationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Accounts_Location");

            entity.HasOne(d => d.SubsidiaryAccounts).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.SubsidiaryAccountsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Accounts_SubsidiaryAccounts");
        });

        modelBuilder.Entity<AccountsGroup>(entity =>
        {
            entity.ToTable("AccountsGroup");

            entity.Property(e => e.BalanceType)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Code)
                .HasMaxLength(2)
                .IsUnicode(false);
            entity.Property(e => e.CreateOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(25);

            entity.HasOne(d => d.CompanyNavigation).WithMany(p => p.AccountsGroups)
                .HasForeignKey(d => d.Company)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AccountsGroup_Company");

            entity.HasOne(d => d.EntryByNavigation).WithMany(p => p.AccountsGroups)
                .HasForeignKey(d => d.EntryBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AccountsGroup_LoginUser");

            entity.HasOne(d => d.Location).WithMany(p => p.AccountsGroups)
                .HasForeignKey(d => d.LocationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AccountsGroup_Location");
        });

        modelBuilder.Entity<AccountsSubGroup>(entity =>
        {
            entity.ToTable("AccountsSubGroup");

            entity.Property(e => e.Code)
                .HasMaxLength(2)
                .IsUnicode(false);
            entity.Property(e => e.CreateOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(150);

            entity.HasOne(d => d.AccountsGroup).WithMany(p => p.AccountsSubGroups)
                .HasForeignKey(d => d.AccountsGroupId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AccountsSubGroup_AccountsGroup");

            entity.HasOne(d => d.CompanyNavigation).WithMany(p => p.AccountsSubGroups)
                .HasForeignKey(d => d.Company)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AccountsSubGroup_Company");

            entity.HasOne(d => d.EntryByNavigation).WithMany(p => p.AccountsSubGroups)
                .HasForeignKey(d => d.EntryBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AccountsSubGroup_LoginUser");

            entity.HasOne(d => d.Location).WithMany(p => p.AccountsSubGroups)
                .HasForeignKey(d => d.LocationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AccountsSubGroup_Location");
        });

        modelBuilder.Entity<Bank>(entity =>
        {
            entity.ToTable("Bank");

            entity.HasIndex(e => e.BankName, "Bank_BankName");

            entity.Property(e => e.Address)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.BankName)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Brand>(entity =>
        {
            entity.ToTable("Brand");

            entity.Property(e => e.CreateOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(200);

            entity.HasOne(d => d.Company).WithMany(p => p.Brands)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Brand_Company");

            entity.HasOne(d => d.EntryByNavigation).WithMany(p => p.Brands)
                .HasForeignKey(d => d.EntryBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Brand_LoginUser");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.ToTable("Category");

            entity.Property(e => e.CreateOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(200);

            entity.HasOne(d => d.Company).WithMany(p => p.Categories)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Category_Company");

            entity.HasOne(d => d.EntryByNavigation).WithMany(p => p.Categories)
                .HasForeignKey(d => d.EntryBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Category_LoginUser");
        });

        modelBuilder.Entity<Collection>(entity =>
        {
            entity.ToTable("Collection");

            entity.HasIndex(e => e.CollectionNo, "Collection_CollectionNo");

            entity.Property(e => e.CollectionAgainst)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.CollectionNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CreateOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Remarks)
                .HasMaxLength(500)
                .IsUnicode(false);

            entity.HasOne(d => d.CollectedByNavigation).WithMany(p => p.Collections)
                .HasForeignKey(d => d.CollectedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Collection_Employee");

            entity.HasOne(d => d.Customer).WithMany(p => p.Collections)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Collection_Customer");

            entity.HasOne(d => d.Location).WithMany(p => p.Collections)
                .HasForeignKey(d => d.LocationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Collection_Location");

            entity.HasOne(d => d.SalesOrder).WithMany(p => p.Collections)
                .HasForeignKey(d => d.SalesOrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Collection_SalesOrder");
        });

        modelBuilder.Entity<CollectionDetail>(entity =>
        {
            entity.ToTable("CollectionDetail");

            entity.Property(e => e.Amount).HasColumnType("decimal(18, 3)");
            entity.Property(e => e.CollectionReference)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CreateOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Bank).WithMany(p => p.CollectionDetails)
                .HasForeignKey(d => d.BankId)
                .HasConstraintName("FK_CollectionDetail_Bank");

            entity.HasOne(d => d.Collection).WithMany(p => p.CollectionDetails)
                .HasForeignKey(d => d.CollectionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CollectionDetail_Collection");
        });

        modelBuilder.Entity<Company>(entity =>
        {
            entity.ToTable("Company");

            entity.Property(e => e.CompanyLogo)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.LastClosingDate).HasColumnType("datetime");
            entity.Property(e => e.Name)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.OpeningDate).HasColumnType("datetime");
            entity.Property(e => e.OwnerName)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.WebsiteLink)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ControlAccount>(entity =>
        {
            entity.HasKey(e => e.ControlAccountsId);

            entity.Property(e => e.CreateOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(150);

            entity.HasOne(d => d.AccountsGroup).WithMany(p => p.ControlAccounts)
                .HasForeignKey(d => d.AccountsGroupId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ControlAccounts_AccountsGroup");

            entity.HasOne(d => d.AccountsSubGroup).WithMany(p => p.ControlAccounts)
                .HasForeignKey(d => d.AccountsSubGroupId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ControlAccounts_AccountsSubGroup");

            entity.HasOne(d => d.CompanyNavigation).WithMany(p => p.ControlAccounts)
                .HasForeignKey(d => d.Company)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ControlAccounts_Company");

            entity.HasOne(d => d.EntryByNavigation).WithMany(p => p.ControlAccounts)
                .HasForeignKey(d => d.EntryBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ControlAccounts_LoginUser");

            entity.HasOne(d => d.Location).WithMany(p => p.ControlAccounts)
                .HasForeignKey(d => d.LocationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ControlAccounts_Location");
        });

        modelBuilder.Entity<CurrentStock>(entity =>
        {
            entity.ToTable("CurrentStock");

            entity.Property(e => e.CurrentStockId).ValueGeneratedNever();
            entity.Property(e => e.Cost).HasColumnType("decimal(18, 3)");
            entity.Property(e => e.CreateOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.StockInType)
                .HasMaxLength(5)
                .IsUnicode(false);
            entity.Property(e => e.StockInTypeRefNo)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Company).WithMany(p => p.CurrentStocks)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CurrentStock_Company");

            entity.HasOne(d => d.EntryByNavigation).WithMany(p => p.CurrentStocks)
                .HasForeignKey(d => d.EntryBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CurrentStock_LoginUser");

            entity.HasOne(d => d.Location).WithMany(p => p.CurrentStocks)
                .HasForeignKey(d => d.LocationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CurrentStock_Location");

            entity.HasOne(d => d.Product).WithMany(p => p.CurrentStocks)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CurrentStock_Product");

            entity.HasOne(d => d.Supplier).WithMany(p => p.CurrentStocks)
                .HasForeignKey(d => d.SupplierId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CurrentStock_Supplier");
        });

        modelBuilder.Entity<CurrentStockDetail>(entity =>
        {
            entity.ToTable("CurrentStockDetail");

            entity.HasIndex(e => e.SerialNo, "CurrentStockDetail_SerialNo");

            entity.Property(e => e.CurrentStockDetailId).ValueGeneratedNever();
            entity.Property(e => e.SerialNo)
                .HasMaxLength(250)
                .IsUnicode(false);

            entity.HasOne(d => d.CurrentStock).WithMany(p => p.CurrentStockDetails)
                .HasForeignKey(d => d.CurrentStockId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CurrentStock_CurrentStock");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.ToTable("Customer");

            entity.HasIndex(e => e.Code, "Customer_Code");

            entity.HasIndex(e => e.Name, "Customer_Name");

            entity.HasIndex(e => e.Phone, "Customer_Phone");

            entity.HasIndex(e => e.Phone, "UQ_Customer_Phone").IsUnique();

            entity.Property(e => e.ActiveStatus)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValue("Y");
            entity.Property(e => e.Address).HasMaxLength(500);
            entity.Property(e => e.Code)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CollectionAmount).HasColumnType("decimal(18, 3)");
            entity.Property(e => e.CreateOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Name).HasMaxLength(500);
            entity.Property(e => e.Nid)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("NID");
            entity.Property(e => e.OpeningBalance).HasColumnType("decimal(18, 3)");
            entity.Property(e => e.Phone)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Picture).IsUnicode(false);
            entity.Property(e => e.TotalSales).HasColumnType("decimal(18, 3)");
            entity.Property(e => e.UpdateOn).HasColumnType("datetime");

            entity.HasOne(d => d.Company).WithMany(p => p.Customers)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Customer_Company");

            entity.HasOne(d => d.CustomerGroup).WithMany(p => p.Customers)
                .HasForeignKey(d => d.CustomerGroupId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Customer_CustomerGroup");

            entity.HasOne(d => d.EntryByNavigation).WithMany(p => p.CustomerEntryByNavigations)
                .HasForeignKey(d => d.EntryBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Customer_LoginUser");

            entity.HasOne(d => d.Supplier).WithMany(p => p.Customers)
                .HasForeignKey(d => d.SupplierId)
                .HasConstraintName("FK_Customer_Supplier");

            entity.HasOne(d => d.UpdateByNavigation).WithMany(p => p.CustomerUpdateByNavigations)
                .HasForeignKey(d => d.UpdateBy)
                .HasConstraintName("FK_Customer_LoginUser_Update");
        });

        modelBuilder.Entity<CustomerGroup>(entity =>
        {
            entity.ToTable("CustomerGroup");

            entity.Property(e => e.Code)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CreateOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(200);

            entity.HasOne(d => d.Company).WithMany(p => p.CustomerGroups)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CustomerGroup_Company");

            entity.HasOne(d => d.EntryByNavigation).WithMany(p => p.CustomerGroups)
                .HasForeignKey(d => d.EntryBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CustomerGroup_LoginUser");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.ToTable("Employee");

            entity.HasIndex(e => e.Name, "Employee_Name");

            entity.HasIndex(e => e.Phone, "Employee_Phone");

            entity.HasIndex(e => e.Phone, "UQ_Employee_Phone").IsUnique();

            entity.Property(e => e.ActiveStatus)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValue("Y");
            entity.Property(e => e.Code)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreateOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Nid)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("NID");
            entity.Property(e => e.Phone)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Picture).IsUnicode(false);

            entity.HasOne(d => d.Company).WithMany(p => p.Employees)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Employee_Company");

            entity.HasOne(d => d.EntryByNavigation).WithMany(p => p.Employees)
                .HasForeignKey(d => d.EntryBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Employee_LoginUser");
        });

        modelBuilder.Entity<EventCodeGeneration>(entity =>
        {
            entity.ToTable("EventCodeGeneration");

            entity.HasIndex(e => new { e.CompanyId, e.EventType }, "UQ_EventCodeGeneration").IsUnique();

            entity.Property(e => e.CompanyCode)
                .HasMaxLength(5)
                .IsUnicode(false);
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.EventType)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Prefix)
                .HasMaxLength(5)
                .IsUnicode(false);
            entity.Property(e => e.UpdateOn).HasColumnType("datetime");

            entity.HasOne(d => d.Company).WithMany(p => p.EventCodeGenerations)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EventCodeGeneration_Company");

            entity.HasOne(d => d.UpdateByNavigation).WithMany(p => p.EventCodeGenerations)
                .HasForeignKey(d => d.UpdateBy)
                .HasConstraintName("FK_EventCodeGeneration_LoginUser");
        });

        modelBuilder.Entity<EventNoGeneration>(entity =>
        {
            entity.ToTable("EventNoGeneration");

            entity.HasIndex(e => new { e.CompanyId, e.LocationId, e.EventType, e.EntryYear }, "UQ_EventNoGeneration").IsUnique();

            entity.Property(e => e.CompanyCode)
                .HasMaxLength(5)
                .IsUnicode(false);
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.EventType)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LocationCode)
                .HasMaxLength(5)
                .IsUnicode(false);
            entity.Property(e => e.Prefix)
                .HasMaxLength(5)
                .IsUnicode(false);
            entity.Property(e => e.UpdateOn).HasColumnType("datetime");

            entity.HasOne(d => d.Company).WithMany(p => p.EventNoGenerations)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EventNoGeneration_Company");

            entity.HasOne(d => d.Location).WithMany(p => p.EventNoGenerations)
                .HasForeignKey(d => d.LocationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EventNoGeneration_Location");

            entity.HasOne(d => d.UpdateByNavigation).WithMany(p => p.EventNoGenerations)
                .HasForeignKey(d => d.UpdateBy)
                .HasConstraintName("FK_EventNoGeneration_LoginUser");
        });

        modelBuilder.Entity<Location>(entity =>
        {
            entity.ToTable("Location");

            entity.Property(e => e.Code)
                .HasMaxLength(5)
                .IsUnicode(false);
            entity.Property(e => e.CreateOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Name)
                .HasMaxLength(150)
                .IsUnicode(false);

            entity.HasOne(d => d.Company).WithMany(p => p.Locations)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Location_Company");
        });

        modelBuilder.Entity<LoginUser>(entity =>
        {
            entity.ToTable("LoginUser");

            entity.HasIndex(e => e.Code, "UQ_LoginUser_Code").IsUnique();

            entity.HasIndex(e => e.Email, "UQ_LoginUser_Email").IsUnique();

            entity.HasIndex(e => e.Phone, "UQ_LoginUser_Phone").IsUnique();

            entity.Property(e => e.Address)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Code)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.CreateOn).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Picture)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.UserLevel)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Company).WithMany(p => p.LoginUsers)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LoginUser_Company");

            entity.HasOne(d => d.Location).WithMany(p => p.LoginUsers)
                .HasForeignKey(d => d.LocationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LoginUser_Location");
        });

        modelBuilder.Entity<PasswordResetOtp>(entity =>
        {
            entity.ToTable("PasswordResetOtp");

            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ExpiresDate).HasColumnType("datetime");
            entity.Property(e => e.OtpHash).HasMaxLength(255);

            entity.HasOne(d => d.User).WithMany(p => p.PasswordResetOtps)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PasswordResetOtp_LoginUser");
        });

        modelBuilder.Entity<PaymentMethod>(entity =>
        {
            entity.ToTable("PaymentMethod");

            entity.Property(e => e.ActiveStatus)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValue("Y");
            entity.Property(e => e.CreateOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Name)
                .HasMaxLength(250)
                .IsUnicode(false);

            entity.HasOne(d => d.Company).WithMany(p => p.InverseCompany)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PaymentMethod_Company");

            entity.HasOne(d => d.EntryByNavigation).WithMany(p => p.PaymentMethods)
                .HasForeignKey(d => d.EntryBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PaymentMethod_LoginUser");
        });

        modelBuilder.Entity<PostedTransaction>(entity =>
        {
            entity.ToTable("PostedTransaction");

            entity.Property(e => e.Amount).HasColumnType("decimal(18, 3)");
            entity.Property(e => e.BalanceType)
                .HasMaxLength(6)
                .IsUnicode(false);
            entity.Property(e => e.CreateOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Date).HasColumnType("datetime");

            entity.HasOne(d => d.Accounts).WithMany(p => p.PostedTransactions)
                .HasForeignKey(d => d.AccountsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PostedTransaction_Accounts");

            entity.HasOne(d => d.CompanyNavigation).WithMany(p => p.PostedTransactions)
                .HasForeignKey(d => d.Company)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PostedTransaction_Company");

            entity.HasOne(d => d.EntryByNavigation).WithMany(p => p.PostedTransactions)
                .HasForeignKey(d => d.EntryBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PostedTransaction_LoginUser");

            entity.HasOne(d => d.Location).WithMany(p => p.PostedTransactions)
                .HasForeignKey(d => d.LocationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PostedTransaction_Location");

            entity.HasOne(d => d.VoucherDetail).WithMany(p => p.PostedTransactions)
                .HasForeignKey(d => d.VoucherDetailId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PostedTransaction_VoucherDetail");

            entity.HasOne(d => d.Voucher).WithMany(p => p.PostedTransactions)
                .HasForeignKey(d => d.VoucherId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PostedTransaction_Voucher");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable("Product");

            entity.HasIndex(e => e.Code, "Product_Code");

            entity.HasIndex(e => e.Name, "Product_Model");

            entity.HasIndex(e => e.Name, "Product_Name");

            entity.Property(e => e.ActiveStatus)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.Code)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreateOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.IsVatPercentageOrAmount)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.Model).HasMaxLength(200);
            entity.Property(e => e.Name).HasMaxLength(500);
            entity.Property(e => e.Picture).IsUnicode(false);
            entity.Property(e => e.ProductNote).HasMaxLength(500);
            entity.Property(e => e.ProductType)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.SerialAvailable)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.Vat).HasColumnType("decimal(18, 3)");
            entity.Property(e => e.Warranty).HasColumnType("decimal(18, 3)");

            entity.HasOne(d => d.Brand).WithMany(p => p.Products)
                .HasForeignKey(d => d.BrandId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Product_Brand");

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Product_Category");

            entity.HasOne(d => d.Company).WithMany(p => p.Products)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Product_Company");

            entity.HasOne(d => d.EntryByNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.EntryBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Product_LoginUser");

            entity.HasOne(d => d.Group).WithMany(p => p.Products)
                .HasForeignKey(d => d.GroupId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Product_ProductGroup");

            entity.HasOne(d => d.UnitType).WithMany(p => p.Products)
                .HasForeignKey(d => d.UnitTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Product_UnitType");
        });

        modelBuilder.Entity<ProductGroup>(entity =>
        {
            entity.ToTable("ProductGroup");

            entity.Property(e => e.CreateOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(200);

            entity.HasOne(d => d.Company).WithMany(p => p.ProductGroups)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductGroup_Company");

            entity.HasOne(d => d.EntryByNavigation).WithMany(p => p.ProductGroups)
                .HasForeignKey(d => d.EntryBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductGroup_LoginUser");
        });

        modelBuilder.Entity<ProductUnitTypeConversion>(entity =>
        {
            entity.ToTable("ProductUnitTypeConversion");

            entity.HasIndex(e => new { e.ProductId, e.UnitTypeId }, "UQ_ProductUnitTypeConversion").IsUnique();

            entity.Property(e => e.ConversionToUnitType).HasColumnType("decimal(18, 3)");
            entity.Property(e => e.EntryDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");

            entity.HasOne(d => d.Company).WithMany(p => p.ProductUnitTypeConversions)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductUnitTypeConversion_Company");

            entity.HasOne(d => d.EntryByNavigation).WithMany(p => p.ProductUnitTypeConversionEntryByNavigations)
                .HasForeignKey(d => d.EntryBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductUnitTypeConversion_LoginUser");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductUnitTypeConversions)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductUnitTypeConversion_Product");

            entity.HasOne(d => d.UnitType).WithMany(p => p.ProductUnitTypeConversions)
                .HasForeignKey(d => d.UnitTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductUnitTypeConversion_UnitType");

            entity.HasOne(d => d.UpdateByNavigation).WithMany(p => p.ProductUnitTypeConversionUpdateByNavigations)
                .HasForeignKey(d => d.UpdateBy)
                .HasConstraintName("FK_ProductUnitTypeConversion_LoginUserUpdate");
        });

        modelBuilder.Entity<PurchaseIn>(entity =>
        {
            entity.ToTable("PurchaseIn");

            entity.Property(e => e.PurchaseInId).ValueGeneratedNever();
            entity.Property(e => e.AdditionalCost).HasColumnType("decimal(18, 3)");
            entity.Property(e => e.Amount).HasColumnType("decimal(18, 3)");
            entity.Property(e => e.Approved)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.ApprovedTime).HasColumnType("datetime");
            entity.Property(e => e.Cancelled)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.CancelledTime).HasColumnType("datetime");
            entity.Property(e => e.CreateOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.Discount).HasColumnType("decimal(18, 3)");
            entity.Property(e => e.DiscountType)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.PreviousDeu).HasColumnType("decimal(18, 3)");
            entity.Property(e => e.PurchaseInNo)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.ReferenceNo)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Remarks).HasMaxLength(500);
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(18, 3)");
            entity.Property(e => e.Updated)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedTime).HasColumnType("datetime");

            entity.HasOne(d => d.ApprovedByNavigation).WithMany(p => p.PurchaseInApprovedByNavigations)
                .HasForeignKey(d => d.ApprovedBy)
                .HasConstraintName("FK_PurchaseIn_LoginUser_Approved");

            entity.HasOne(d => d.CancelledByNavigation).WithMany(p => p.PurchaseInCancelledByNavigations)
                .HasForeignKey(d => d.CancelledBy)
                .HasConstraintName("FK_PurchaseIn_LoginUser_Cancelled");

            entity.HasOne(d => d.CompanyNavigation).WithMany(p => p.PurchaseIns)
                .HasForeignKey(d => d.Company)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PurchaseIn_Company");

            entity.HasOne(d => d.EntryByNavigation).WithMany(p => p.PurchaseInEntryByNavigations)
                .HasForeignKey(d => d.EntryBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PurchaseIn_LoginUser");

            entity.HasOne(d => d.Location).WithMany(p => p.PurchaseIns)
                .HasForeignKey(d => d.LocationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PurchaseIn_Location");

            entity.HasOne(d => d.PaymentMethod).WithMany(p => p.PurchaseIns)
                .HasForeignKey(d => d.PaymentMethodId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Supplier_PaymentMethod");

            entity.HasOne(d => d.Supplier).WithMany(p => p.PurchaseIns)
                .HasForeignKey(d => d.SupplierId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Supplier_Location");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.PurchaseInUpdatedByNavigations)
                .HasForeignKey(d => d.UpdatedBy)
                .HasConstraintName("FK_PurchaseIn_LoginUser_Updated");

            entity.HasOne(d => d.Voucher).WithMany(p => p.PurchaseIns)
                .HasForeignKey(d => d.VoucherId)
                .HasConstraintName("FK_PurchaseIn_Voucher");
        });

        modelBuilder.Entity<PurchaseInDetail>(entity =>
        {
            entity.ToTable("PurchaseInDetail");

            entity.Property(e => e.PurchaseInDetailId).ValueGeneratedNever();
            entity.Property(e => e.AdditionalCost).HasColumnType("decimal(18, 3)");
            entity.Property(e => e.BatchNo)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Cost).HasColumnType("decimal(18, 3)");
            entity.Property(e => e.DiscountAmount).HasColumnType("decimal(18, 3)");
            entity.Property(e => e.DiscountType)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.ExpiryDate).HasColumnType("datetime");
            entity.Property(e => e.LotNo)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.ManufactureDate).HasColumnType("datetime");
            entity.Property(e => e.PurchaseInAdditionalCost).HasColumnType("decimal(18, 3)");
            entity.Property(e => e.PurchaseInDiscount).HasColumnType("decimal(18, 3)");
            entity.Property(e => e.PurchaseInDiscountType)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.Quantity).HasColumnType("decimal(18, 3)");

            entity.HasOne(d => d.Location).WithMany(p => p.PurchaseInDetails)
                .HasForeignKey(d => d.LocationId)
                .HasConstraintName("FK_PurchaseInDetail_Location");

            entity.HasOne(d => d.Product).WithMany(p => p.PurchaseInDetails)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PurchaseInDetail_Product");

            entity.HasOne(d => d.PurchaseIn).WithMany(p => p.PurchaseInDetails)
                .HasForeignKey(d => d.PurchaseInId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PurchaseInDetail_PurchaseIn");

            entity.HasOne(d => d.UnitTypeNavigation).WithMany(p => p.PurchaseInDetails)
                .HasForeignKey(d => d.UnitType)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PurchaseInDetail_UnitType");
        });

        modelBuilder.Entity<PurchaseInDetailSerial>(entity =>
        {
            entity.ToTable("PurchaseInDetailSerial");

            entity.Property(e => e.PurchaseInDetailSerialId).ValueGeneratedNever();
            entity.Property(e => e.SerialNo)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.PurchaseInDetail).WithMany(p => p.PurchaseInDetailSerials)
                .HasForeignKey(d => d.PurchaseInDetailId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PurchaseInDetailSerial_PurchaseInDetail");

            entity.HasOne(d => d.PurchaseIn).WithMany(p => p.PurchaseInDetailSerials)
                .HasForeignKey(d => d.PurchaseInId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PurchaseInDetailSerial_PurchaseIn");
        });

        modelBuilder.Entity<PurchaseInDetailTax>(entity =>
        {
            entity.ToTable("PurchaseInDetailTax");

            entity.Property(e => e.PurchaseInDetailTaxId).ValueGeneratedNever();
            entity.Property(e => e.CreateOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.TaxAmount).HasColumnType("decimal(18, 3)");
            entity.Property(e => e.TaxOn)
                .HasMaxLength(1)
                .IsUnicode(false);

            entity.HasOne(d => d.PurchaseInDetail).WithMany(p => p.PurchaseInDetailTaxes)
                .HasForeignKey(d => d.PurchaseInDetailId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PurchaseInDetailTax_PurchaseInDetail");

            entity.HasOne(d => d.PurchaseIn).WithMany(p => p.PurchaseInDetailTaxes)
                .HasForeignKey(d => d.PurchaseInId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PurchaseInDetailTax_PurchaseIn");

            entity.HasOne(d => d.Tax).WithMany(p => p.PurchaseInDetailTaxes)
                .HasForeignKey(d => d.TaxId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PurchaseInDetailTax_Tax");
        });

        modelBuilder.Entity<PurchaseOrder>(entity =>
        {
            entity.ToTable("PurchaseOrder");

            entity.HasIndex(e => e.PurchaseOrderNo, "PurchaseOrderNo");

            entity.Property(e => e.ApprovalStatus)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValue("N");
            entity.Property(e => e.ApprovedDate).HasColumnType("datetime");
            entity.Property(e => e.CancelRemarks)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.CancelledDate).HasColumnType("datetime");
            entity.Property(e => e.CreateOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DeliveryDate).HasColumnType("datetime");
            entity.Property(e => e.ExpectedPaymentReleaseDate).HasColumnType("datetime");
            entity.Property(e => e.OrderRefNo)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.OrderType)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.PartialShipment)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Podate)
                .HasColumnType("datetime")
                .HasColumnName("PODate");
            entity.Property(e => e.PurchaseOrderNo)
                .HasMaxLength(35)
                .IsUnicode(false);
            entity.Property(e => e.PurchaseOrderType)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Remarks).HasMaxLength(500);
            entity.Property(e => e.ShipToAddress).HasMaxLength(500);
            entity.Property(e => e.ShipmentMode)
                .HasMaxLength(3)
                .IsUnicode(false);
            entity.Property(e => e.SupplierAddress)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.SupplierContactNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SupplierContactPerson)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(18, 3)");
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");

            entity.HasOne(d => d.ApprovedByNavigation).WithMany(p => p.PurchaseOrderApprovedByNavigations)
                .HasForeignKey(d => d.ApprovedBy)
                .HasConstraintName("FK_PurchaseOrder_LoginUser_Approve");

            entity.HasOne(d => d.CancelledByNavigation).WithMany(p => p.PurchaseOrderCancelledByNavigations)
                .HasForeignKey(d => d.CancelledBy)
                .HasConstraintName("FK_PurchaseOrder_LoginUser_Cancell");

            entity.HasOne(d => d.Company).WithMany(p => p.PurchaseOrders)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PurchaseOrder_Company");

            entity.HasOne(d => d.DeliveryToNavigation).WithMany(p => p.PurchaseOrderDeliveryToNavigations)
                .HasForeignKey(d => d.DeliveryTo)
                .HasConstraintName("FK_PurchaseOrder_DeliveryTo");

            entity.HasOne(d => d.Employee).WithMany(p => p.PurchaseOrders)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK_PurchaseOrder_Employee");

            entity.HasOne(d => d.EntryByNavigation).WithMany(p => p.PurchaseOrderEntryByNavigations)
                .HasForeignKey(d => d.EntryBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PurchaseOrder_LoginUser_Entry");

            entity.HasOne(d => d.Location).WithMany(p => p.PurchaseOrderLocations)
                .HasForeignKey(d => d.LocationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PurchaseOrder_Location");

            entity.HasOne(d => d.PaymentMethod).WithMany(p => p.PurchaseOrders)
                .HasForeignKey(d => d.PaymentMethodId)
                .HasConstraintName("FK_PurchaseOrder_PaymentMethod");

            entity.HasOne(d => d.Supplier).WithMany(p => p.PurchaseOrders)
                .HasForeignKey(d => d.SupplierId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PurchaseOrder_Supplier");

            entity.HasOne(d => d.UpdateByNavigation).WithMany(p => p.PurchaseOrderUpdateByNavigations)
                .HasForeignKey(d => d.UpdateBy)
                .HasConstraintName("FK_PurchaseOrder_LoginUser_Update");
        });

        modelBuilder.Entity<PurchaseOrderDetail>(entity =>
        {
            entity.HasKey(e => e.PurchaseOrderDetailId).HasName("FK_PurchaseOrderDetail");

            entity.ToTable("PurchaseOrderDetail");

            entity.Property(e => e.Cost).HasColumnType("decimal(18, 3)");
            entity.Property(e => e.CreateOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Quantity).HasColumnType("decimal(18, 3)");

            entity.HasOne(d => d.Product).WithMany(p => p.PurchaseOrderDetails)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PurchaseOrderDetail_Product");

            entity.HasOne(d => d.PurchaseOrder).WithMany(p => p.PurchaseOrderDetails)
                .HasForeignKey(d => d.PurchaseOrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PurchaseOrderDetail_PurchaseOrder");

            entity.HasOne(d => d.UnitType).WithMany(p => p.PurchaseOrderDetails)
                .HasForeignKey(d => d.UnitTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PurchaseOrderDetail_UnitType");
        });

        modelBuilder.Entity<PurchaseOrderDetailTax>(entity =>
        {
            entity.HasKey(e => e.PurchaseOrderDetailTaxId).HasName("FK_PurchaseOrderDetail_Tax");

            entity.ToTable("PurchaseOrderDetail_Tax");

            entity.Property(e => e.CreateOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.TaxAmount).HasColumnType("decimal(18, 3)");
            entity.Property(e => e.TaxOn)
                .HasMaxLength(1)
                .IsUnicode(false);

            entity.HasOne(d => d.PurchaseOrderDetail).WithMany(p => p.PurchaseOrderDetailTaxes)
                .HasForeignKey(d => d.PurchaseOrderDetailId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PurchaseOrderDetailTax_PurchaseOrderDetail");

            entity.HasOne(d => d.Tax).WithMany(p => p.PurchaseOrderDetailTaxes)
                .HasForeignKey(d => d.TaxId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PurchaseOrderDetailTax_Tax");
        });

        modelBuilder.Entity<SalesOrder>(entity =>
        {
            entity.ToTable("SalesOrder");

            entity.HasIndex(e => e.InvoiceNo, "SalesOrder_InvoiceNo");

            entity.HasIndex(e => e.SalesOrderNo, "SalesOrder_SalesOrderNo");

            entity.Property(e => e.CreateOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DiscountAmount).HasColumnType("decimal(18, 3)");
            entity.Property(e => e.DueAmount).HasColumnType("decimal(18, 3)");
            entity.Property(e => e.GrandTotal).HasColumnType("decimal(18, 3)");
            entity.Property(e => e.InvoiceNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PaidAmount).HasColumnType("decimal(18, 3)");
            entity.Property(e => e.SalesOrderNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TaxAmount).HasColumnType("decimal(18, 3)");
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(18, 3)");
            entity.Property(e => e.VatAmount).HasColumnType("decimal(18, 3)");

            entity.HasOne(d => d.Customer).WithMany(p => p.SalesOrders)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SalesOrder_Customer");

            entity.HasOne(d => d.Employee).WithMany(p => p.SalesOrders)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SalesOrder_Employee");

            entity.HasOne(d => d.EntryByNavigation).WithMany(p => p.SalesOrders)
                .HasForeignKey(d => d.EntryBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SalesOrder_LoginUser");

            entity.HasOne(d => d.Location).WithMany(p => p.SalesOrders)
                .HasForeignKey(d => d.LocationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SalesOrder_Location");

            entity.HasOne(d => d.PaymentMethod).WithMany(p => p.SalesOrders)
                .HasForeignKey(d => d.PaymentMethodId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SalesOrder_PaymentMethod");
        });

        modelBuilder.Entity<SalesOrderDetail>(entity =>
        {
            entity.ToTable("SalesOrderDetail");

            entity.Property(e => e.Cost).HasColumnType("decimal(18, 3)");
            entity.Property(e => e.CreateOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Discount).HasColumnType("decimal(18, 3)");
            entity.Property(e => e.Price).HasColumnType("decimal(18, 3)");
            entity.Property(e => e.Quantity).HasColumnType("decimal(18, 3)");

            entity.HasOne(d => d.Product).WithMany(p => p.SalesOrderDetails)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SalesOrderDetail_Product");

            entity.HasOne(d => d.SalesOrder).WithMany(p => p.SalesOrderDetails)
                .HasForeignKey(d => d.SalesOrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SalesOrderDetail_SalesOrder");
        });

        modelBuilder.Entity<SalesOrderSerial>(entity =>
        {
            entity.ToTable("SalesOrderSerial");

            entity.HasIndex(e => e.SerialNo, "SalesOrderSerial_SerialNo");

            entity.Property(e => e.CreateOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.SerialNo)
                .HasMaxLength(250)
                .IsUnicode(false);

            entity.HasOne(d => d.SalesOrderDetail).WithMany(p => p.SalesOrderSerials)
                .HasForeignKey(d => d.SalesOrderDetailId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SalesOrderSerial_SalesOrderDetail");

            entity.HasOne(d => d.SalesOrder).WithMany(p => p.SalesOrderSerials)
                .HasForeignKey(d => d.SalesOrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SalesOrderSerial_SalesOrder");
        });

        modelBuilder.Entity<SubsidiaryAccount>(entity =>
        {
            entity.HasKey(e => e.SubsidiaryAccountsId);

            entity.Property(e => e.CreateOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(150);

            entity.HasOne(d => d.AccountsGroup).WithMany(p => p.SubsidiaryAccounts)
                .HasForeignKey(d => d.AccountsGroupId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SubsidiaryAccounts_AccountsGroup");

            entity.HasOne(d => d.AccountsSubGroup).WithMany(p => p.SubsidiaryAccounts)
                .HasForeignKey(d => d.AccountsSubGroupId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SubsidiaryAccounts_AccountsSubGroup");

            entity.HasOne(d => d.CompanyNavigation).WithMany(p => p.SubsidiaryAccounts)
                .HasForeignKey(d => d.Company)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SubsidiaryAccounts_Company");

            entity.HasOne(d => d.ControlAccounts).WithMany(p => p.SubsidiaryAccounts)
                .HasForeignKey(d => d.ControlAccountsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SubsidiaryAccounts_ControlAccounts");

            entity.HasOne(d => d.EntryByNavigation).WithMany(p => p.SubsidiaryAccounts)
                .HasForeignKey(d => d.EntryBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SubsidiaryAccounts_LoginUser");

            entity.HasOne(d => d.Location).WithMany(p => p.SubsidiaryAccounts)
                .HasForeignKey(d => d.LocationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SubsidiaryAccounts_Location");
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.ToTable("Supplier");

            entity.HasIndex(e => e.Name, "Supplier_Name");

            entity.HasIndex(e => e.Phone, "UQ_Supplier_Phone").IsUnique();

            entity.Property(e => e.ActiveStatus)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.Address).HasMaxLength(500);
            entity.Property(e => e.Code)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreateOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Name).HasMaxLength(500);
            entity.Property(e => e.OpeningBalance).HasColumnType("decimal(18, 3)");
            entity.Property(e => e.Phone)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Picture).IsUnicode(false);
            entity.Property(e => e.UpdateOn).HasColumnType("datetime");

            entity.HasOne(d => d.Company).WithMany(p => p.Suppliers)
                .HasForeignKey(d => d.CompanyId)
                .HasConstraintName("FK_Supplier_Company");

            entity.HasOne(d => d.EntryByNavigation).WithMany(p => p.SupplierEntryByNavigations)
                .HasForeignKey(d => d.EntryBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Supplier_LoginUser");

            entity.HasOne(d => d.UpdateByNavigation).WithMany(p => p.SupplierUpdateByNavigations)
                .HasForeignKey(d => d.UpdateBy)
                .HasConstraintName("FK_Supplier_LoginUser_Update");
        });

        modelBuilder.Entity<Tax>(entity =>
        {
            entity.HasKey(e => e.TaxId).HasName("FK_Tax");

            entity.ToTable("Tax");

            entity.Property(e => e.CreateOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.TaxName).HasMaxLength(30);

            entity.HasOne(d => d.EntryByNavigation).WithMany(p => p.Taxes)
                .HasForeignKey(d => d.EntryBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tax_LoginUser_Entry");
        });

        modelBuilder.Entity<UnitType>(entity =>
        {
            entity.ToTable("UnitType");

            entity.Property(e => e.CreateOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<UserSession>(entity =>
        {
            entity.ToTable("UserSession");

            entity.HasIndex(e => e.Jwtidentifier, "UQ_UserSession_JWTIdentifier").IsUnique();

            entity.Property(e => e.CreateOn)
                .HasDefaultValueSql("(getutcdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DeviceName).HasMaxLength(200);
            entity.Property(e => e.ExpiryTime).HasColumnType("datetime");
            entity.Property(e => e.IpAddress).HasMaxLength(50);
            entity.Property(e => e.Jwtidentifier)
                .HasMaxLength(100)
                .HasColumnName("JWTIdentifier");
            entity.Property(e => e.RefreshToken).HasMaxLength(500);
            entity.Property(e => e.RevokedTime).HasColumnType("datetime");

            entity.HasOne(d => d.LoginUser).WithMany(p => p.UserSessions)
                .HasForeignKey(d => d.LoginUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserSession_LoginUser");
        });

        modelBuilder.Entity<UserVerification>(entity =>
        {
            entity.ToTable("UserVerification");

            entity.Property(e => e.CreateOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ExpiredDate).HasColumnType("datetime");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.VerificationCode)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.VerificationType)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.VerifiedDate).HasColumnType("datetime");

            entity.HasOne(d => d.LoginUser).WithMany(p => p.UserVerifications)
                .HasForeignKey(d => d.LoginUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserVerification_LoginUser");
        });

        modelBuilder.Entity<Voucher>(entity =>
        {
            entity.ToTable("Voucher");

            entity.Property(e => e.VoucherId).ValueGeneratedNever();
            entity.Property(e => e.Approved)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.ApprovedDate).HasColumnType("datetime");
            entity.Property(e => e.CancelledDate).HasColumnType("datetime");
            entity.Property(e => e.CreateOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.PostedDate).HasColumnType("datetime");
            entity.Property(e => e.ReferenceNo)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.VoucherNo)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.VoucherType)
                .HasMaxLength(3)
                .IsUnicode(false);

            entity.HasOne(d => d.ApprovedByNavigation).WithMany(p => p.VoucherApprovedByNavigations).HasForeignKey(d => d.ApprovedBy);

            entity.HasOne(d => d.CancelledByNavigation).WithMany(p => p.VoucherCancelledByNavigations).HasForeignKey(d => d.CancelledBy);

            entity.HasOne(d => d.CompanyNavigation).WithMany(p => p.Vouchers)
                .HasForeignKey(d => d.Company)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Voucher_Company");

            entity.HasOne(d => d.EntryByNavigation).WithMany(p => p.VoucherEntryByNavigations)
                .HasForeignKey(d => d.EntryBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Voucher_LoginUser");

            entity.HasOne(d => d.Location).WithMany(p => p.Vouchers)
                .HasForeignKey(d => d.LocationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Voucher_Location");

            entity.HasOne(d => d.PostedByNavigation).WithMany(p => p.VoucherPostedByNavigations).HasForeignKey(d => d.PostedBy);

            entity.HasOne(d => d.PrepareByNavigation).WithMany(p => p.VoucherPrepareByNavigations)
                .HasForeignKey(d => d.PrepareBy)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<VoucherDetail>(entity =>
        {
            entity.ToTable("VoucherDetail");

            entity.Property(e => e.VoucherDetailId).ValueGeneratedNever();
            entity.Property(e => e.Credit).HasColumnType("decimal(18, 3)");
            entity.Property(e => e.Debit).HasColumnType("decimal(18, 3)");
            entity.Property(e => e.Particulars).HasMaxLength(1000);

            entity.HasOne(d => d.Accounts).WithMany(p => p.VoucherDetails)
                .HasForeignKey(d => d.AccountsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_VoucherDetail_Accounts");

            entity.HasOne(d => d.Voucher).WithMany(p => p.VoucherDetails)
                .HasForeignKey(d => d.VoucherId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_VoucherDetail_Voucher");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
