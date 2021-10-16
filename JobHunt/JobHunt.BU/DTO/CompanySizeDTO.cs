using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobHunt.BU.DTO
{
    public class CompanySizeDTO
    {
        public int CompanySizeId { get; set; }
        public int? CSMin { get; set; }
        public int? CSMax { get; set; }
        public string CSShow { get; set; }
    }
}
