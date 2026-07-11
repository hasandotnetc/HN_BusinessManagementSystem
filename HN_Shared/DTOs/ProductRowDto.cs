using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HN_Shared.DTOs
{
    public class ProductRowDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public string SerialNo { get; set; }
        public string Model { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal RowDiscount { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
