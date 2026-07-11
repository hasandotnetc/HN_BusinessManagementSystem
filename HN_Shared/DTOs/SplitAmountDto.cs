using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HN_Shared.DTOs
{
    public class SplitAmountDto
    {
        public string Method { get; set; }
        public decimal Amount { get; set; }
        public string Ref1 { get; set; } // Mobile / Card Last 4 digits / Cheque No
        public string Ref2 { get; set; } // Bank Name (For Card or Cheque)
    }
}
