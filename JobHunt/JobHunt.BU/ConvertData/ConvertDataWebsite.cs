using JobHunt.BU.DTO;
using JobHunt.Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobHunt.BU.ConvertData
{
    public class ConvertDataWebsite
    {
        public WebsiteDTO ConvertToDTO(Website EF)
        {
            var DTO = new WebsiteDTO()
            {
                WebsiteId = EF.WebsiteId,
                WBanner = EF.WBanner,
                WEmail = EF.WEmail,
                WFacebook = EF.WFacebook,
                WPhone = EF.WPhone,
                WYoutube = EF.WYoutube,
                WBanner2 = EF.WBanner2,
                WBanner3 = EF.WBanner3
            };
            return DTO;
        }

        public Website ConvertToEF(WebsiteDTO DTO)
        {
            var EF = new Website()
            {
                WebsiteId = DTO.WebsiteId,
                WBanner = DTO.WBanner,
                WEmail = DTO.WEmail,
                WFacebook = DTO.WFacebook,
                WPhone = DTO.WPhone,
                WYoutube = DTO.WYoutube,
                WBanner2 = DTO.WBanner2,
                WBanner3 = DTO.WBanner3
            };
            return EF;
        }
    }
}
