using JobHunt.BU.DTO;
using JobHunt.Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobHunt.BU.ConvertData
{
    public class ConvertDataCandidatePostResume
    {
         ConvertDataRecruitJob recruitjobConvert = new ConvertDataRecruitJob();
         ConvertDataCandidate candidateConvert = new ConvertDataCandidate();
         //ConvertDataRecruitJob _recruitConvert = new ConvertDataRecruitJob();
        public CandidatePostResumeDTO ConvertToDTO(CandidatePostResume EF)
        {
            var DTO = new CandidatePostResumeDTO
            {
                CandidatePostResumeId = EF.CandidatePostResumeId,
                CPRPostDate = EF.CPRPostDate,
                CPRStatus = EF.CPRStatus,
                CPR_CandidateId = EF.CPR_CandidateId,
                CPR_RecruitJobId = EF.CPR_RecruitJobId,
                CddPathFileCV = EF.CddPathFileCV,
                CddPhone = EF.CddPhone
            };
            //if (EF.City != null)
            //{
            //    DTO.CityDTO = _cityConvert.ConvertToDTO(EF.City);
            //}
            if(EF.RecruitJob != null)
            {
                DTO.RecruitJobDTO = recruitjobConvert.ConvertToDTO(EF.RecruitJob);
            }

            if (EF.Candidate != null)
            {
                DTO.CandidateDTO = candidateConvert.ConvertToDTO(EF.Candidate);
            }

            return DTO;
        }


        public CandidatePostResume ConvertToEF(CandidatePostResumeDTO DTO)
        {
            var EF = new CandidatePostResume
            {
                CPRPostDate = DTO.CPRPostDate,
                CPRStatus = DTO.CPRStatus,
                CPR_CandidateId = DTO.CPR_CandidateId,
                CPR_RecruitJobId = DTO.CPR_RecruitJobId,
                CddPathFileCV = DTO.CddPathFileCV,
                CddPhone = DTO.CddPhone
            };
            //if (DTO.CityDTO != null)
            //{
            //    EF.City = _cityConvert.ConvertToEF(DTO.CityDTO);
            //}
            return EF;
        }
    }
}
