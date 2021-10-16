using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobHunt.BU.DTO
{
    public class SaveCandidateDTO
    {
        public int SaveCandidateId { get; set; }
        public int? SC_RecruitId { get; set; }
        public int? SC_CandidateId { get; set; }

        public virtual CandidateDTO CandidateDTO { get; set; }
        public virtual RecruitDTO RecruitDTO { get; set; }
    }
}
