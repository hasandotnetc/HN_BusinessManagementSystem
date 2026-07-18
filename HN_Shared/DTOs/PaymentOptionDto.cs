using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HN_Shared.DTOs
{
    public class PaymentOptionDto
    {
        public long PaymentModeId { get; set; }
        public string Method { get; set; }
        public decimal ReceiveAmount { get; set; }
        public decimal DueAmount { get; set; }
        public decimal ChangeAmount { get; set; }
        public string MobileNo { get; set; }
        public string CardNo { get; set; }
        public string ChequeNo { get; set; }
        public string BankName { get; set; }
    }
}
