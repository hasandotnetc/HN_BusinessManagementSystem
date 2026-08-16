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

    public virtual DbSet<Bank> Banks { get; set; }

    public virtual DbSet<Brand> Brands { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Collection> Collections { get; set; }

    public virtual DbSet<CollectionDetail> CollectionDetails { get; set; }

    public virtual DbSet<CurrentStock> CurrentStocks { get; set; }

    public virtual DbSet<CurrentStockDetail> CurrentStockDetails { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Location> Locations { get; set; }

    public virtual DbSet<LoginUser> LoginUsers { get; set; }

    public virtual DbSet<PaymentMethod> PaymentMethods { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductGroup> ProductGroups { get; set; }

    public virtual DbSet<SalesOrder> SalesOrders { get; set; }

    public virtual DbSet<SalesOrderDetail> SalesOrderDetails { get; set; }

    public virtual DbSet<SalesOrderSerial> SalesOrderSerials { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    public virtual DbSet<UnitType> UnitTypes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-E4QISLB;Database=Hasan_DB;Trusted_Connection=True;TrustServerCertificate=True;Encrypt=False;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
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

            entity.Property(e => e.Code)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CreateOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Picture).IsUnicode(false);

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
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Picture).IsUnicode(false);

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

        modelBuilder.Entity<CurrentStock>(entity =>
        {
            entity.ToTable("CurrentStock");

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

            entity.HasIndex(e => e.Email, "UQ_Customer_Email").IsUnique();

            entity.HasIndex(e => e.Nid, "UQ_Customer_NID").IsUnique();

            entity.HasIndex(e => e.Phone, "UQ_Customer_Phone").IsUnique();

            entity.Property(e => e.Address)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Code)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreateOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DueAmount).HasColumnType("decimal(18, 3)");
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
            entity.Property(e => e.OpeningBalance).HasColumnType("decimal(18, 3)");
            entity.Property(e => e.Phone)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Picture).IsUnicode(false);
            entity.Property(e => e.TotalSales).HasColumnType("decimal(18, 3)");

            entity.HasOne(d => d.EntryByNavigation).WithMany(p => p.Customers)
                .HasForeignKey(d => d.EntryBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Customer_LoginUser");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.ToTable("Employee");

            entity.HasIndex(e => e.Name, "Employee_Name");

            entity.HasIndex(e => e.Phone, "Employee_Phone");

            entity.HasIndex(e => e.Phone, "UQ_Employee_Phone").IsUnique();

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

            entity.HasOne(d => d.EntryByNavigation).WithMany(p => p.Employees)
                .HasForeignKey(d => d.EntryBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Employee_LoginUser");
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
        });

        modelBuilder.Entity<PaymentMethod>(entity =>
        {
            entity.ToTable("PaymentMethod");

            entity.Property(e => e.CreateOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Name)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.Picture).IsUnicode(false);

            entity.HasOne(d => d.EntryByNavigation).WithMany(p => p.PaymentMethods)
                .HasForeignKey(d => d.EntryBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PaymentMethod_LoginUser");

            entity.HasOne(d => d.Location).WithMany(p => p.PaymentMethods)
                .HasForeignKey(d => d.LocationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PaymentMethod_Location");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable("Product");

            entity.HasIndex(e => e.Code, "Product_Code");

            entity.HasIndex(e => e.Model, "Product_Model");

            entity.HasIndex(e => e.Name, "Product_Name");

            entity.Property(e => e.Code)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreateOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Discount).HasColumnType("decimal(18, 3)");
            entity.Property(e => e.Model)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Picture).IsUnicode(false);
            entity.Property(e => e.Price).HasColumnType("decimal(18, 3)");
            entity.Property(e => e.ProductType)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.SerialAvailable)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.Tax).HasColumnType("decimal(18, 3)");
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
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.HasOne(d => d.EntryByNavigation).WithMany(p => p.ProductGroups)
                .HasForeignKey(d => d.EntryBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductGroup_LoginUser");
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

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.ToTable("Supplier");

            entity.HasIndex(e => e.Name, "Supplier_Name");

            entity.HasIndex(e => e.Phone, "UQ_Supplier_Phone").IsUnique();

            entity.Property(e => e.Address)
                .HasMaxLength(500)
                .IsUnicode(false);
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
            entity.Property(e => e.Phone)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Picture).IsUnicode(false);

            entity.HasOne(d => d.EntryByNavigation).WithMany(p => p.Suppliers)
                .HasForeignKey(d => d.EntryBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Supplier_LoginUser");
        });

        modelBuilder.Entity<UnitType>(entity =>
        {
            entity.ToTable("UnitType");

            entity.Property(e => e.CreateOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
