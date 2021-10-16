using JobHunt.BU.DTO;
using JobHunt.Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobHunt.BU.ConvertData
{
    public class ConvertDataRecruit
    {
        readonly ConvertDataAspNetUser _aspnetuserConvert = new ConvertDataAspNetUser();
        public RecruitDTO ConvertToDTO(Recruit EF)
        {
            var DTO = new RecruitDTO()
            {
                RecruitId = EF.RecruitId,
                RIAbout = EF.RIAbout,
                RIAddress = EF.RIAddress,
                RIEmail = EF.RIEmail,
                RIFullName = EF.RIFullName,
                RICompanyName = EF.RICompanyName,
                RILogo = EF.RILogo,
                RIPassword = EF.RIPassword,
                RIPhone = EF.RIPhone,
                RIRegisterDate = EF.RIRegisterDate,
                RIStatus = EF.RIStatus,
                RIUserName = EF.RIUserName,
                RI_CityId = EF.RI_CityId,
                RI_AspNetUserId = EF.RI_AspNetUserId,
                RIWebsite = EF.RIWebsite,
                RICoverImage = EF.RICoverImage,
                FoundedYear = EF.FoundedYear,
                RI_CompanySizeId = EF.RI_CompanySizeId,
                RI_DistrictId = EF.RI_DistrictId,
                RI_ProfessionId = EF.RI_ProfessionId,
                RI_WardId = EF.RI_WardId,
                RIAvatar = EF.RIAvatar
            };

            switch (DTO.RIStatus)
            {
                case (int)Common.Enum.StatusRecruit.Active:
                    DTO.StatusString = "Hoạt động";
                    break;
                case (int)Common.Enum.StatusRecruit.Approvaling:
                    DTO.StatusString = "Chờ xét duyệt";
                    break;
                default:
                    DTO.StatusString = "Tạm ngưng";
                    break;
            }
            if(EF.FoundedYear != null)
            {
                DTO.FounedYearString = EF.FoundedYear.Value.ToString("yyyy-MM-dd");
            }
            if (EF.City != null)
                DTO.CityDTO = new ConvertDataCity().ConvertToDTO(EF.City);

            if (EF.AspNetUser != null)
            {
                DTO.AspNetUserDTO = _aspnetuserConvert.ConvertToDTO(EF.AspNetUser);
            }

            if (EF.District != null)
            {
                DTO.DistrictDTO = new ConvertDataDistrict().ConvertToDTO(EF.District);
            }

            if (EF.Ward != null)
            {
                DTO.WardDTO = new ConvertDataWard().ConvertToDTO(EF.Ward);
            }

            if (EF.Profession != null)
            {
                DTO.ProfessionDTO = new ConvertDataProfession().ConvertToDTO(EF.Profession);
            }

            if (EF.CompanySize != null)
            {
                DTO.CompanySizeDTO = new ConvertDataCompanySize().ConvertToDTO(EF.CompanySize);
            }


            return DTO;
        }

        public Recruit ConvertToEF(RecruitDTO DTO)
        {
            var EF = new Recruit()
            {
                RecruitId = DTO.RecruitId,
                RIAbout = DTO.RIAbout,
                RIAddress = DTO.RIAddress,
                RIEmail = DTO.RIEmail,
                RIFullName = DTO.RIFullName,
                RICompanyName = DTO.RICompanyName,
                RILogo = DTO.RILogo,
                RIPassword = DTO.RIPassword,
                RIPhone = DTO.RIPhone,
                RIRegisterDate = DTO.RIRegisterDate,
                RIStatus = DTO.RIStatus,
                RIUserName = DTO.RIUserName,
                RI_CityId = DTO.RI_CityId,
                RI_AspNetUserId = DTO.RI_AspNetUserId,
                RICoverImage = DTO.RICoverImage,
                FoundedYear = DTO.FoundedYear,
                RI_CompanySizeId = DTO.RI_CompanySizeId,
                RI_DistrictId = DTO.RI_DistrictId,
                RI_ProfessionId = DTO.RI_ProfessionId,
                RI_WardId = DTO.RI_WardId,
                RIAvatar = DTO.RIAvatar,
                RIWebsite = DTO.RIWebsite
            };

            if (DTO.CityDTO != null)
                EF.City = new ConvertDataCity().ConvertToEF(DTO.CityDTO);

            if (DTO.AspNetUserDTO != null)
            {
                EF.AspNetUser = _aspnetuserConvert.ConvertToEF(DTO.AspNetUserDTO);
            }

            return EF;
        }
    }
}
