using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HN_Shared.DTOs
{
    public class SummaryDto
    {
        public decimal SubTotal { get; set; }
        public decimal Discount { get; set; }
        public decimal TaxPercent { get; set; }
        public decimal GrandTotal { get; set; }
    }
}
