using JobHunt.BU.DTO;
using JobHunt.Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobHunt.BU.ConvertData
{
    public class ConvertDataNew
    {
        public NewDTO ConvertToDTO(New EF)
        {
            var DTO = new NewDTO()
            {
                NewsId = EF.NewsId,
                NDetail = EF.NDetail,
                NPostDate = EF.NPostDate,
                NPublicDate = EF.NPublicDate,
                NQuote = EF.NQuote,
                Nstatus = EF.Nstatus,
                NTitle = EF.NTitle,
                N_CategoryId = EF.N_CategoryId,
                N_WebmasterInfoId = EF.N_WebmasterInfoId,
                NAvatar = EF.NAvatar,
                NType = EF.NType
            };
            if (EF.WebmasterInfo != null)
                DTO.WebmasterInfoDTO = new ConvertDataWebmasterInfo().ConvertToDTO(EF.WebmasterInfo);
            if(EF.Category != null)
                DTO.CategoryDTO = new ConvertDataCategory().ConvertToDTO(EF.Category);

            //convert to name type
            switch (EF.NType)
            {
                case (int)BU.Common.Enum.TypeNew.Hot:
                    DTO.NameType = "Nổi bật";
                    break;
                default:
                    DTO.NameType = "Thường";
                    break;
            }

            //convert to name status
            switch (EF.Nstatus)
            {
                case (int)BU.Common.Enum.StatusNew.Active:
                    DTO.NameStatus = "Hoạt động";
                    break;
                case (int)BU.Common.Enum.StatusNew.Block:
                    DTO.NameStatus = "Dừng hoạt động";
                    break;
                default:
                    DTO.NameStatus = "Đã xóa";
                    break;
            }
            return DTO;
        }

        public New ConvertToEF(NewDTO DTO)
        {
            var EF = new New()
            {
                NewsId = DTO.NewsId,
                NDetail = DTO.NDetail,
                NPostDate = DTO.NPostDate,
                NPublicDate = DTO.NPublicDate,
                NQuote = DTO.NQuote,
                Nstatus = DTO.Nstatus,
                NTitle = DTO.NTitle,
                N_CategoryId = DTO.N_CategoryId,
                N_WebmasterInfoId = DTO.N_WebmasterInfoId,
                NAvatar = DTO.NAvatar,
                NType = DTO.NType
            };

            var a = EF.NDetail.Length;
            return EF;
        }
    }
}
