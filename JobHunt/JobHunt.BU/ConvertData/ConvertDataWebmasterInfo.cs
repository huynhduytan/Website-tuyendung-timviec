using JobHunt.BU.DTO;
using JobHunt.Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobHunt.BU.ConvertData
{
    public class ConvertDataWebmasterInfo
    {

        readonly ConvertDataAspNetUser _aspnetuserConvert = new ConvertDataAspNetUser();
        public WebmasterInfoDTO ConvertToDTO(WebmasterInfo EF)
        {
            var DTO = new WebmasterInfoDTO()
            {
                WebmasterInfoId = EF.WebmasterInfoId,
                WIFullName = EF.WIFullName,
                WIStatus = EF.WIStatus,
                WIType = EF.WIType,
                WIUserName = EF.WIUserName,
                WI_AspNetUserId = EF.WI_AspNetUserId,
                WIBirthDay = EF.WIBirthDay,
                WIGender = EF.WIGender,
                WIAddress = EF.WIAddress,
                WIDateStart = EF.WIDateStart,
                WIPosition = EF.WIPosition
            };

            if (EF.WIBirthDay != null)
            {
                DTO.WIBirthdayString = EF.WIBirthDay.Value.ToString("yyyy-MM-dd");

            }
            if (EF.AspNetUser != null)
            {
                DTO.AspNetUserDTO = _aspnetuserConvert.ConvertToDTO(EF.AspNetUser);
                DTO.WIEmail = DTO.AspNetUserDTO.Email;
                DTO.WIPhoneNumber = DTO.AspNetUserDTO.PhoneNumber;
            }

            switch (EF.WIPosition)
            {
                case (int)BU.Common.Enum.WIPosition.Admin:
                    DTO.nameRole = "Quản trị";
                    break;
                default:
                    DTO.nameRole = "Nhân viên";
                    break;
            }


            switch (EF.WIStatus)
            {
                case (int)BU.Common.Enum.StatusAccount.Active:
                    DTO.WIStatusString = "Hoạt động";
                    break;
                case (int)BU.Common.Enum.StatusAccount.Approvaling:
                    DTO.WIStatusString = "Chờ xét duyệt";
                    break;
                case (int)BU.Common.Enum.StatusAccount.Block:
                    DTO.WIStatusString = "Ngừng hoạt động";
                    break;
                default:
                    DTO.WIStatusString = "Đã xóa";
                    break;
            }
            return DTO;
        }

        public WebmasterInfo ConvertToEF(WebmasterInfoDTO DTO)
        {
            var EF = new WebmasterInfo()
            {
                WebmasterInfoId = DTO.WebmasterInfoId,
                WIFullName = DTO.WIFullName,
                WIStatus = DTO.WIStatus,
                WIType = DTO.WIType,
                WIUserName = DTO.WIUserName,
                WI_AspNetUserId = DTO.WI_AspNetUserId,
                WIBirthDay = DTO.WIBirthDay,
                WIGender = DTO.WIGender,
                WIAddress = DTO.WIAddress,
                WIDateStart = DTO.WIDateStart,
                WIPosition = DTO.WIPosition
            };

            if (DTO.AspNetUserDTO != null)
            {
                EF.AspNetUser = _aspnetuserConvert.ConvertToEF(DTO.AspNetUserDTO);
            }


            return EF;
        }
    }
}
