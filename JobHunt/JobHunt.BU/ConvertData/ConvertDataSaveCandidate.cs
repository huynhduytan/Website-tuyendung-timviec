using JobHunt.BU.DTO;
using JobHunt.Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobHunt.BU.ConvertData
{
    public class ConvertDataSaveCandidate
    {
        public SaveCandidateDTO ConvertToDTO(SaveCandidate EF)
        {
            var DTO = new SaveCandidateDTO()
            {
                SaveCandidateId = EF.SaveCandidateId,
                SC_CandidateId = EF.SC_CandidateId,
                SC_RecruitId = EF.SC_RecruitId
            };

            if (EF.Candidate != null)
                DTO.CandidateDTO = new ConvertDataCandidate().ConvertToDTO(EF.Candidate);

            if (EF.Recruit != null)
                DTO.RecruitDTO = new ConvertDataRecruit().ConvertToDTO(EF.Recruit);


            return DTO;
        }

        public SaveCandidate ConvertToEF(SaveCandidateDTO DTO)
        {
            var EF = new SaveCandidate()
            {
                SaveCandidateId = DTO.SaveCandidateId,
                SC_CandidateId = DTO.SC_CandidateId,
                SC_RecruitId = DTO.SC_RecruitId
            };

            if (DTO.CandidateDTO != null)
                EF.Candidate = new ConvertDataCandidate().ConvertToEF(DTO.CandidateDTO);

            if (DTO.RecruitDTO != null)
                EF.Recruit = new ConvertDataRecruit().ConvertToEF(DTO.RecruitDTO);

            return EF;
        }
    }
}
