using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HN_Shared.DTOs
{
    public class ProductGroupVM
    {
        public long ProductGroupId { get; set; }

        public string Name { get; set; } = null!;

        //public DateTime CreateOn { get; set; }

        //public long EntryBy { get; set; }
    }
}
