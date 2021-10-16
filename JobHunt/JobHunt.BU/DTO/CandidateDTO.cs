using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobHunt.BU.DTO
{
    public class CandidateDTO
    {
        public int CandidateId { get; set; }
        public string Cdd_AspNetUserId { get; set; }
        public int? CP_ProfessionId { get; set; }
        public int? CP_LevelId { get; set; }
        public int? CP_ExperienceId { get; set; }
        public int? CP_SalaryId { get; set; }
        public int? CP_WorkTypeId { get; set; }
        public string CddUserName { get; set; }
        public string CddPassword { get; set; }
        public string CddFullName { get; set; }
        public string CPAvatar { get; set; }
        public int? CddSex { get; set; }
        public string CddAbout { get; set; }
        public string CddAddress { get; set; }
        public string CPPosition { get; set; }
        public string CPExperience { get; set; }
        public string CPSpeciality { get; set; }
        public DateTime? CPPostDate { get; set; }
        public int? CPStatus { get; set; }
        public int? Cdd_CityId { get; set; }
        public string CddPhone { get; set; }
        public string CddEmail { get; set; }
        public int? CddAge { get; set; }
        public DateTime? CddRegisterDate { get; set; }
        public string CddFacebook { get; set; }
        public string CddGoogle { get; set; }
        public int? Cdd_DistrictId { get; set; }
        public int? Cdd_WardId { get; set; }
        public string CddPathCV { get; set; }
        public string CddDescribeCV { get; set; }
        public DateTime? CddBirthday { get; set; }

        public virtual AspNetUserDTO AspNetUserDTO { get; set; }
        public virtual CityDTO CityDTO { get; set; }
        public virtual DistrictDTO DistrictDTO { get; set; }
        public virtual ExperienceDTO ExperienceDTO { get; set; }
        public virtual LevelInfoDTO LevelInfoDTO { get; set; }
        public virtual ProfessionDTO ProfessionDTO { get; set; }
        public virtual SalaryDTO SalaryDTO { get; set; }
        public virtual WardDTO WardDTO { get; set; }
        public virtual WorkTypeDTO WorkTypeDTO { get; set; }


        ///
        public string nameFileCV { get; set; }
        public string statusString { get; set; }
        public string registerDateString { get; set; }
        public virtual RecruitJobDTO RecruitJobDTO { get; set; }
    }
}
