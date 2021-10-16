using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobHunt.BU.DTO
{
    public class SalaryDTO
    {
        public int SalaryId { get; set; }
        public double? SMin { get; set; }
        public double? SMax { get; set; }
        public string SShow { get; set; }
    }
}
