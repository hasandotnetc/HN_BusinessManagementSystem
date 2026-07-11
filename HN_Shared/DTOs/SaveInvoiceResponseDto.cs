using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HN_Shared.DTOs
{
    public class SaveInvoiceResponseDto
    {
        public string SalesOrderNo { get; set; } = string.Empty;
        public string CollectionNo { get; set; } = string.Empty;
        public string InvoiceNo { get; set; } = string.Empty;
    }
}
