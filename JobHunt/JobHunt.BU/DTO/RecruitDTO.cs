using JobHunt.BU.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobHunt.BU.DTO
{
    public class RecruitDTO
    {
        public int RecruitId { get; set; }
        public string RI_AspNetUserId { get; set; }
        public string RIUserName { get; set; }
        public string RIPassword { get; set; }
        public string RIFullName { get; set; }
        public string RIEmail { get; set; }
        public string RICompanyName { get; set; }
        public string RIAbout { get; set; }
        public string RILogo { get; set; }
        public string RIAddress { get; set; }
        public string RIPhone { get; set; }
        public DateTime? RIRegisterDate { get; set; }
        public int? RIStatus { get; set; }
        public string RIWebsite { get; set; }
        public int? RI_CityId { get; set; }
        public int? RI_DistrictId { get; set; }
        public int? RI_WardId { get; set; }
        public DateTime? FoundedYear { get; set; }
        public int? RI_CompanySizeId { get; set; }
        public int? RI_ProfessionId { get; set; }
        public string RICoverImage { get; set; }
        public string RIAvatar { get; set; }

        public virtual CityDTO CityDTO { get; set; }
        public virtual AspNetUserDTO AspNetUserDTO { get; set; }
        public virtual DistrictDTO DistrictDTO { get; set; }
        public virtual ProfessionDTO ProfessionDTO { get; set; }
        public virtual WardDTO WardDTO { get; set; }
        public virtual CompanySizeDTO CompanySizeDTO { get; set; }

        //
        public string FounedYearString { get; set; }
        public string StatusString { get; set; }
    }
}
