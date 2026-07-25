using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HN_Shared.DTOs
{
    public class CategoryVM
    {
        public long CategoryId { get; set; }  
        public string Name { get; set; } = null!;
    }
}
