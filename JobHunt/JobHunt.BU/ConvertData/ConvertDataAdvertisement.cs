using JobHunt.BU.DTO;
using JobHunt.Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobHunt.BU.ConvertData
{
    public class ConvertDataAdvertisement
    {
        public AdvertisementDTO ConvertToDTO(Advertisement EF)
        {
            var DTO = new AdvertisementDTO()
            {
                AdvertisementId = EF.AdvertisementId,
                AdAddress = EF.AdAddress,
                AdEmail = EF.AdEmail,
                AdEndDate = EF.AdEndDate,
                AdImage = EF.AdImage,
                AdLink = EF.AdLink,
                AdName = EF.AdName,
                AdPhone = EF.AdPhone,
                AdPosition = EF.AdPosition,
                AdStartDate = EF.AdStartDate,
                AdStatus = EF.AdStatus,
                AdStartDateString = EF.AdStartDate.ToString("yyyy-MM-dd"),
                AdEndDateString = EF.AdEndDate.ToString("yyyy-MM-dd")
            };

            switch (EF.AdStatus)
            {
                case (int)BU.Common.Enum.StatusAdvertisement.Show:
                    DTO.nameStatus = "Hiển thị";
                    break;
                default:
                    DTO.nameStatus = "Ẩn";
                    break;
            }
            return DTO;
        }

        public Advertisement ConvertToEF(AdvertisementDTO DTO)
        {
            var EF = new Advertisement()
            {
                AdvertisementId = DTO.AdvertisementId,
                AdAddress = DTO.AdAddress,
                AdEmail = DTO.AdEmail,
                AdEndDate = DTO.AdEndDate,
                AdImage = DTO.AdImage,
                AdLink = DTO.AdLink,
                AdName = DTO.AdName,
                AdPhone = DTO.AdPhone,
                AdPosition = DTO.AdPosition,
                AdStartDate = DTO.AdStartDate,
                AdStatus = DTO.AdStatus
            };
            return EF;
        }
    }
}
