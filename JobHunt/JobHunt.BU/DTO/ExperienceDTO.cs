using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobHunt.BU.DTO
{
    public class ExperienceDTO
    {
        public int ExperienceId { get; set; }
        public double? EMin { get; set; }
        public double? EMax { get; set; }
        public string EShow { get; set; }
    }
}
