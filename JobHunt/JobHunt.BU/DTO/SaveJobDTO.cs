using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobHunt.BU.DTO
{
    public class SaveJobDTO
    {
        public int SaveJobId { get; set; }
        public int? SJ_CandidateId { get; set; }
        public int? SJ_RecruitJobId { get; set; }
        public bool? SJStatus { get; set; }

        public virtual CandidateDTO CandidateDTO { get; set; }
        public virtual RecruitJobDTO RecruitJobDTO { get; set; }

        //
        public string status { get; set; }
        public string postBy { get; set; }
    }
}
