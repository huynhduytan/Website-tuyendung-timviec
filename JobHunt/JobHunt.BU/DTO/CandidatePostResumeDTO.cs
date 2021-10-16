using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobHunt.BU.DTO
{
    public class CandidatePostResumeDTO
    {
        public int CandidatePostResumeId { get; set; }
        public int? CPR_CandidateId { get; set; }
        public int? CPR_RecruitJobId { get; set; }
        public DateTime? CPRPostDate { get; set; }
        public bool? CPRStatus { get; set; }
        public string CddPhone { get; set; }
        public string CddPathFileCV { get; set; }

        public virtual CandidateDTO CandidateDTO { get; set; }
        public virtual RecruitJobDTO RecruitJobDTO { get; set; }
    }
}
