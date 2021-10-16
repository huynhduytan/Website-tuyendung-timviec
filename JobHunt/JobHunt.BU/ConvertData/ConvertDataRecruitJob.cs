using JobHunt.BU.DTO;
using JobHunt.Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobHunt.BU.ConvertData
{
    public class ConvertDataRecruitJob
    {
        public RecruitJobDTO ConvertToDTO(RecruitJob EF)
        {
            var DTO = new RecruitJobDTO()
            {
                RecruitJobId = EF.RecruitJobId,
                RJAmount = EF.RJAmount,
                RJEmailContact = EF.RJEmailContact,
                RJExpirationDate = EF.RJExpirationDate,
                RJPosition = EF.RJPosition,
                RJPostDate = EF.RJPostDate,
                RJStatus = EF.RJStatus,
                RJ_Describe = EF.RJ_Describe,
                RJ_RecruitId = EF.RJ_RecruitId,
                RJ_ProfessionId = EF.RJ_ProfessionId,
                RJ_Require = EF.RJ_Require,
                RJ_SalaryId = EF.RJ_SalaryId,
                RJ_WorkPlace = EF.RJ_WorkPlace,
                RJ_WorkTypeId = EF.RJ_WorkTypeId,
                RJ_LevelId = EF.RJ_LevelId,
                RJType = EF.RJType,
                RJAddress = EF.RJAddress,
                RJTitle = EF.RJTitle,
                RJ_ExperienceId = EF.RJ_ExperienceId,
                RJCityId = EF.RJCityId,
                RJDistrictId = EF.RJDistrictId,
                RJBenefit = EF.RJBenefit,
                RJCount = EF.RJCount,
                RJ_WardId = EF.RJ_WardId,
                RJNameContact = EF.RJNameContact,
                RJPhoneContact = EF.RJPhoneContact,
                RJGender = EF.RJGender,
                RJExpirationDateString = EF.RJExpirationDate.Value.ToString("yyyy-MM-dd")
            };
            switch (DTO.RJ_WorkTypeId)
            {
                case 1:
                    DTO.ClassWorkType = "ft";
                    break;
                case 2:
                    DTO.ClassWorkType = "pt";
                    break;
                case 3:
                    DTO.ClassWorkType = "pt";
                    break;
                case 4:
                    DTO.ClassWorkType = "fl";
                    break;
                case 5:
                    DTO.ClassWorkType = "tp";
                    break;
                default:
                    DTO.ClassWorkType = "fl";
                    break;
            }
            switch (EF.RJGender)
            {
                case 1:
                    DTO.GenderName = "Nam";
                    break;
                case 2:
                    DTO.GenderName = "Nữ";
                    break;
                default:
                    DTO.GenderName = "Khác";
                    break;
            }

            switch (EF.RJStatus)
            {
                case (int)Common.Enum.EnumStatusJob.Active:
                    DTO.TrangThai = "Hoạt động";
                    break;
                case (int)Common.Enum.EnumStatusJob.Approvaling:
                    DTO.TrangThai = "Chờ xét duyệt";
                    break;
                default:
                    DTO.TrangThai = "Ngừng hoạt động";
                    break;
            }

            switch (EF.RJType)
            {
                case (int)Common.Enum.EnumTypeJob.Hot:
                    DTO.NameType = "Nổi bật";
                    break;
                case (int)Common.Enum.EnumTypeJob.Normal:
                    DTO.NameType = "Thường";
                    break;
                case (int)Common.Enum.EnumTypeJob.Stop:
                    DTO.NameType = "Ngừng hoạt động";
                    break;
                default:
                    DTO.NameType = "Đã xóa";
                    break;
            }

            var dateTimeCount = DateTime.Now - EF.RJPostDate;
            DTO.CountDays = dateTimeCount.Value.Days;
            if (EF.LevelInfo != null)
                DTO.LevelInfoDTO = new ConvertDataLevelInfo().ConvertToDTO(EF.LevelInfo);

            if (EF.Profession != null)
                DTO.ProfessionDTO = new ConvertDataProfession().ConvertToDTO(EF.Profession);

            if (EF.Recruit != null)
                DTO.RecruitDTO = new ConvertDataRecruit().ConvertToDTO(EF.Recruit);

            if (EF.Salary != null)
                DTO.SalaryDTO = new ConvertDataSalary().ConvertToDTO(EF.Salary);

            if (EF.WorkType != null)
                DTO.WorkTypeDTO = new ConvertDataWorkType().ConvertToDTO(EF.WorkType);

            if (EF.Experience != null)
                DTO.ExperienceDTO = new ConvertDataExperience().ConvertToDTO(EF.Experience);

            if (EF.City != null)
                DTO.CityDTO = new ConvertDataCity().ConvertToDTO(EF.City);

            if (EF.District != null)
                DTO.DistrictDTO = new ConvertDataDistrict().ConvertToDTO(EF.District);

            var getApplied = EF.CandidatePostResumes.Where(x => x.CPR_RecruitJobId == DTO.RecruitJobId);
            if (getApplied != null)
                DTO.AppliedCount = getApplied.Count();

            if (EF.City != null)
                DTO.CityDTO = new ConvertDataCity().ConvertToDTO(EF.City);
            return DTO;
        }

        public RecruitJob ConvertToEF(RecruitJobDTO DTO)
        {
            var EF = new RecruitJob()
            {
                RecruitJobId = DTO.RecruitJobId,
                RJAmount = DTO.RJAmount,
                RJEmailContact = DTO.RJEmailContact,
                RJExpirationDate = DTO.RJExpirationDate,
                RJPosition = DTO.RJPosition,
                RJPostDate = DTO.RJPostDate,
                RJStatus = DTO.RJStatus,
                RJ_Describe = DTO.RJ_Describe,
                RJ_RecruitId = DTO.RJ_RecruitId,
                RJ_ProfessionId = DTO.RJ_ProfessionId,
                RJ_Require = DTO.RJ_Require,
                RJ_SalaryId = DTO.RJ_SalaryId,
                RJ_WorkPlace = DTO.RJ_WorkPlace,
                RJ_WorkTypeId = DTO.RJ_WorkTypeId,
                RJ_LevelId = DTO.RJ_LevelId,
                RJType = DTO.RJType,
                RJTitle = DTO.RJTitle,
                RJAddress = DTO.RJAddress,
                RJCityId = DTO.RJCityId,
                RJDistrictId = DTO.RJDistrictId,
                RJGender = DTO.RJGender,
                RJBenefit = DTO.RJBenefit,
                RJ_ExperienceId = DTO.RJ_ExperienceId,
                RJCount = DTO.RJCount,
                RJ_WardId = DTO.RJ_WardId,
                RJNameContact = DTO.RJNameContact,
                RJPhoneContact = DTO.RJPhoneContact,
            };

            return EF;
        }
    }
}
