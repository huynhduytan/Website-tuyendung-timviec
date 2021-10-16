using JobHunt.BU.DTO;
using JobHunt.Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobHunt.BU.ConvertData
{
    public class ConvertDataCandidate
    {
        public CandidateDTO ConvertToDTO(Candidate EF)
        {
            var DTO = new CandidateDTO()
            {
                CandidateId = EF.CandidateId,
                CPAvatar = EF.CPAvatar,
                CPExperience = EF.CPExperience,
                CPPosition = EF.CPPosition,
                CPPostDate = EF.CPPostDate,
                CPSpeciality = EF.CPSpeciality,
                CPStatus = EF.CPStatus,
                CP_ExperienceId = EF.CP_ExperienceId,
                CP_LevelId = EF.CP_LevelId,
                CP_ProfessionId = EF.CP_ProfessionId,
                CP_SalaryId = EF.CP_SalaryId,
                CP_WorkTypeId = EF.CP_WorkTypeId,
                CddAbout = EF.CddAbout,
                CddAddress = EF.CddAddress,
                CddAge = EF.CddAge,
                CddEmail = EF.CddEmail,
                CddFullName = EF.CddFullName,
                CddPassword = EF.CddPassword,
                CddPhone = EF.CddPhone,
                CddRegisterDate = EF.CddRegisterDate,
                CddSex = EF.CddSex,
                CddUserName = EF.CddUserName,
                Cdd_AspNetUserId = EF.Cdd_AspNetUserId,
                Cdd_CityId = EF.Cdd_CityId,
                CddFacebook = EF.CddFacebook,
                CddGoogle = EF.CddGoogle,
                Cdd_DistrictId = EF.Cdd_DistrictId,
                Cdd_WardId = EF.Cdd_WardId,
                CddDescribeCV = EF.CddDescribeCV,
                CddPathCV = EF.CddPathCV,
                CddBirthday = EF.CddBirthday,
                registerDateString = EF.CddRegisterDate.Value.ToShortDateString()
            };

            if (EF.Experience != null)
                DTO.ExperienceDTO = new ConvertDataExperience().ConvertToDTO(EF.Experience);

            if (EF.LevelInfo != null)
                DTO.LevelInfoDTO = new ConvertDataLevelInfo().ConvertToDTO(EF.LevelInfo);

            if (EF.Profession != null)
                DTO.ProfessionDTO = new ConvertDataProfession().ConvertToDTO(EF.Profession);

            if (EF.Salary != null)
                DTO.SalaryDTO = new ConvertDataSalary().ConvertToDTO(EF.Salary);

            if (EF.WorkType != null)
                DTO.WorkTypeDTO = new ConvertDataWorkType().ConvertToDTO(EF.WorkType);

            if (EF.AspNetUser != null)
                DTO.AspNetUserDTO = new ConvertDataAspNetUser().ConvertToDTO(EF.AspNetUser);

            if (EF.City != null)
                DTO.CityDTO = new ConvertDataCity().ConvertToDTO(EF.City);

            if (EF.District != null)
                DTO.DistrictDTO = new ConvertDataDistrict().ConvertToDTO(EF.District);

            if (EF.Ward != null)
                DTO.WardDTO = new ConvertDataWard().ConvertToDTO(EF.Ward);

            if (!string.IsNullOrEmpty(EF.CddPathCV))
            {
                DTO.nameFileCV = EF.CddPathCV.Split('/').Last();
                var nameFile = DTO.nameFileCV.Split('.').First();
                var extention = DTO.nameFileCV.Split('.').Last();

                DTO.nameFileCV = nameFile.Remove(nameFile.Length - 14) + "." + extention;
            }

            switch (DTO.CPStatus)
            {
                case (int)Common.Enum.StatusCandidate.Active:
                    DTO.statusString = "Hoạt động";
                    break;
                case (int)Common.Enum.StatusCandidate.Approvaling:
                    DTO.statusString = "Chờ phê duyệt";
                    break;
                default:
                    DTO.statusString = "Tạm ngừng";
                    break;
            }
            return DTO;
        }

        public Candidate ConvertToEF(CandidateDTO DTO)
        {
            var EF = new Candidate()
            {
                CandidateId = DTO.CandidateId,
                CPAvatar = DTO.CPAvatar,
                CPExperience = DTO.CPExperience,
                CPPosition = DTO.CPPosition,
                CPPostDate = DTO.CPPostDate,
                CPSpeciality = DTO.CPSpeciality,
                CPStatus = DTO.CPStatus,
                CP_ExperienceId = DTO.CP_ExperienceId,
                CP_LevelId = DTO.CP_LevelId,
                CP_ProfessionId = DTO.CP_ProfessionId,
                CP_SalaryId = DTO.CP_SalaryId,
                CP_WorkTypeId = DTO.CP_WorkTypeId,
                CddAbout = DTO.CddAbout,
                CddAddress = DTO.CddAddress,
                CddAge = DTO.CddAge,
                CddEmail = DTO.CddEmail,
                CddFullName = DTO.CddFullName,
                CddPassword = DTO.CddPassword,
                CddPhone = DTO.CddPhone,
                CddRegisterDate = DTO.CddRegisterDate,
                CddSex = DTO.CddSex,
                CddUserName = DTO.CddUserName,
                Cdd_AspNetUserId = DTO.Cdd_AspNetUserId,
                Cdd_CityId = DTO.Cdd_CityId,
                Cdd_DistrictId = DTO.Cdd_DistrictId,
                Cdd_WardId = DTO.Cdd_WardId,
                CddFacebook = DTO.CddFacebook,
                CddGoogle = DTO.CddGoogle,
                CddDescribeCV = DTO.CddDescribeCV,
                CddPathCV = DTO.CddPathCV,
                CddBirthday = DTO.CddBirthday
            };


            if (DTO.ExperienceDTO != null)
                EF.Experience = new ConvertDataExperience().ConvertToEF(DTO.ExperienceDTO);

            if (DTO.LevelInfoDTO != null)
                EF.LevelInfo = new ConvertDataLevelInfo().ConvertToEF(DTO.LevelInfoDTO);

            if (DTO.ProfessionDTO != null)
                EF.Profession = new ConvertDataProfession().ConvertToEF(DTO.ProfessionDTO);

            if (DTO.SalaryDTO != null)
                EF.Salary = new ConvertDataSalary().ConvertToEF(DTO.SalaryDTO);

            if (DTO.WorkTypeDTO != null)
                EF.WorkType = new ConvertDataWorkType().ConvertToEF(DTO.WorkTypeDTO);

            if (DTO.AspNetUserDTO != null)
                EF.AspNetUser = new ConvertDataAspNetUser().ConvertToEF(DTO.AspNetUserDTO);

            if (DTO.CityDTO != null)
                EF.City = new ConvertDataCity().ConvertToEF(DTO.CityDTO);



            return EF;
        }
    }
}
