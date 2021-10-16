using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobHunt.BU.DTO
{
    public class JobCounterDTO
    {
        public int JobCounterId { get; set; }
        public int? JC__RecruitId { get; set; }
        public int? JCCounter { get; set; }
        public DateTime? JCLastTime { get; set; }

        public virtual RecruitJobDTO RecruitJobDTO { get; set; }
    }
}
