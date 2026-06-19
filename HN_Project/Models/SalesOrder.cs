using System;
using System.Collections.Generic;

namespace HN_Project.Models;

public partial class SalesOrder
{
    public long SalesOrderId { get; set; }

    public string SalesOrderNo { get; set; } = null!;

    public string InvoiceNo { get; set; } = null!;

    public long CustomerId { get; set; }

    public long EmployeeId { get; set; }

    public long PaymentMethodId { get; set; }

    public decimal TotalAmount { get; set; }

    public decimal DiscountAmount { get; set; }

    public decimal TaxAmount { get; set; }

    public decimal VatAmount { get; set; }

    public decimal GrandTotal { get; set; }

    public decimal DueAmount { get; set; }

    public decimal PaidAmount { get; set; }

    public long LocationId { get; set; }

    public long EntryBy { get; set; }

    public DateTime? CreateOn { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual Employee Employee { get; set; } = null!;

    public virtual LoginUser EntryByNavigation { get; set; } = null!;

    public virtual Location Location { get; set; } = null!;

    public virtual PaymentMethod PaymentMethod { get; set; } = null!;

    public virtual ICollection<SalesOrderDetail> SalesOrderDetails { get; set; } = new List<SalesOrderDetail>();

    public virtual ICollection<SalesOrderSerial> SalesOrderSerials { get; set; } = new List<SalesOrderSerial>();
}
