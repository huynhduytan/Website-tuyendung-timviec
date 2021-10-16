using JobHunt.BU.DTO;
using JobHunt.Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobHunt.BU.ConvertData
{
    public class ConvertDataJobCounter
    {
        //public JobCounterDTO ConvertToDTO(JobCounter EF)
        //{
        //    var DTO = new JobCounterDTO()
        //    {
        //        JobCounterId = EF.JobCounterId,
        //        JCCounter = EF.JCCounter,
        //        JCLastTime = EF.JCLastTime,
        //        JC__RecruitId = EF.JC__RecruitId
        //    };
        //    if (EF.RecruitJob != null)
        //        DTO.RecruitJobDTO = new ConvertDataRecruitJob().ConvertToDTO(EF.RecruitJob);
        //    return DTO;
        //}

        //public JobCounter ConvertToEF(JobCounterDTO DTO)
        //{
        //    var EF = new JobCounter()
        //    {
        //        JobCounterId = DTO.JobCounterId,
        //        JCCounter = DTO.JCCounter,
        //        JCLastTime = DTO.JCLastTime,
        //        JC__RecruitId = DTO.JC__RecruitId
        //    };
        //    if (DTO.RecruitJobDTO != null)
        //        EF.RecruitJob = new ConvertDataRecruitJob().ConvertToEF(DTO.RecruitJobDTO);
        //    return EF;
        //}
    }
}
