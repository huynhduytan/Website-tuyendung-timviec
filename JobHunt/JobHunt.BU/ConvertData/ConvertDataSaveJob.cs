using JobHunt.BU.DTO;
using JobHunt.Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobHunt.BU.ConvertData
{
    public class ConvertDataSaveJob
    {
        public SaveJobDTO ConvertToDTO(SaveJob EF)
        {
            var DTO = new SaveJobDTO()
            {
                SaveJobId = EF.SaveJobId,
                SJStatus = EF.SJStatus,
                SJ_CandidateId = EF.SJ_CandidateId,
                SJ_RecruitJobId = EF.SJ_RecruitJobId
            };

            if (EF.Candidate != null)
                DTO.CandidateDTO = new ConvertDataCandidate().ConvertToDTO(EF.Candidate);

            if (EF.RecruitJob != null)
                DTO.RecruitJobDTO = new ConvertDataRecruitJob().ConvertToDTO(EF.RecruitJob);


            return DTO;
        }

        public SaveJob ConvertToEF(SaveJobDTO DTO)
        {
            var EF = new SaveJob()
            {
                SaveJobId = DTO.SaveJobId,
                SJStatus = DTO.SJStatus,
                SJ_CandidateId = DTO.SJ_CandidateId,
                SJ_RecruitJobId = DTO.SJ_RecruitJobId
            };

            if (DTO.CandidateDTO != null)
                EF.Candidate = new ConvertDataCandidate().ConvertToEF(DTO.CandidateDTO);

            if (DTO.RecruitJobDTO != null)
                EF.RecruitJob = new ConvertDataRecruitJob().ConvertToEF(DTO.RecruitJobDTO);


            return EF;
        }
    }
}
