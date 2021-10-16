using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobHunt.BU.DTO
{
    public class RecruitJobDTO
    {
        public int RecruitJobId { get; set; }
        public string RJTitle { get; set; }
        public int? RJ_RecruitId { get; set; }
        public string RJPosition { get; set; }
        public string RJAddress { get; set; }
        public int? RJAmount { get; set; }
        public int? RJ_ProfessionId { get; set; }
        public int? RJ_LevelId { get; set; }
        public string RJBenefit { get; set; }
        public string RJ_Describe { get; set; }
        public string RJ_Require { get; set; }
        public int? RJ_WorkTypeId { get; set; }
        public int? RJ_SalaryId { get; set; }
        public string RJ_WorkPlace { get; set; }
        public DateTime? RJPostDate { get; set; }
        public DateTime? RJExpirationDate { get; set; }
        public string RJContact { get; set; }
        public string RJEmailContact { get; set; }
        public string RJPhoneContact { get; set; }
        public string RJNameContact { get; set; }
        public int? RJStatus { get; set; }
        public int? RJType { get; set; }
        public int? RJ_ExperienceId { get; set; }
        public int? RJGender { get; set; }
        public int? RJCityId { get; set; }
        public int? RJDistrictId { get; set; }
        public int? RJ_WardId { get; set; }
        public int? RJCount { get; set; }


        public virtual LevelInfoDTO LevelInfoDTO { get; set; }
        public virtual ProfessionDTO ProfessionDTO { get; set; }
        public virtual RecruitDTO RecruitDTO { get; set; }
        public virtual SalaryDTO SalaryDTO { get; set; }
        public virtual WorkTypeDTO WorkTypeDTO { get; set; }
        public virtual ExperienceDTO ExperienceDTO { get; set; }
        public virtual CityDTO CityDTO { get; set; }
        public virtual DistrictDTO DistrictDTO { get; set; }

        //add class show
        public string ClassWorkType { get; set; }
        public string GenderName { get; set; }
        public int CountDays { get; set; }
        public int AppliedCount { get; set; }

        public string TrangThai { get; set; }
        public string NameType { get; set; }
        public string RJExpirationDateString { get; set; }
    }
}
