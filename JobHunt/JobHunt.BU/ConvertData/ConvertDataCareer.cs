using JobHunt.BU.DTO;
using JobHunt.Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobHunt.BU.ConvertData
{
    public class ConvertDataCareer
    {
        public CareerDTO ConvertToDTO(Career EF)
        {
            var DTO = new CareerDTO()
            {
                CareerId = EF.CareerId,
                CareerName = EF.CareerName
            };
            switch (DTO.CareerId)
            {
                case (int)Common.Enum.EnumCareer.BanHangTiepThi:
                    DTO.Icon = "la la-tag";
                    break;
                case (int)Common.Enum.EnumCareer.HangTieuDung:
                    DTO.Icon = "la la-cart-plus";
                    break;
                case (int)Common.Enum.EnumCareer.KyThuat:
                    DTO.Icon = "la la-gears";
                    break;
                case (int)Common.Enum.EnumCareer.DichVu:
                    DTO.Icon = "la la-globe";
                    break;
                case (int)Common.Enum.EnumCareer.MayTinhIT:
                    DTO.Icon = "la la-desktop";
                    break;
                case (int)Common.Enum.EnumCareer.SucKhoe:
                    DTO.Icon = "la la-user-md";
                    break;
                case (int)Common.Enum.EnumCareer.SanXuat:
                    DTO.Icon = "la la-industry";
                    break;
                case (int)Common.Enum.EnumCareer.HanhChinhNhanSu:
                    DTO.Icon = "la la-users";
                    break;
                case (int)Common.Enum.EnumCareer.KeToanTaiChinh:
                    DTO.Icon = "la la-money";
                    break;
                case (int)Common.Enum.EnumCareer.TruyenThongMedia:
                    DTO.Icon = "la la-bullhorn";
                    break;
                case (int)Common.Enum.EnumCareer.XayDung:
                    DTO.Icon = "la la-building";
                    break;
                case (int)Common.Enum.EnumCareer.GiaoDucDaoTao:
                    DTO.Icon = "la la-graduation-cap";
                    break;
                case (int)Common.Enum.EnumCareer.KhoaHoc:
                    DTO.Icon = "la la-flask";
                    break;
                case (int)Common.Enum.EnumCareer.KhachSanDuLich:
                    DTO.Icon = "la la-globe";
                    break;
                default:
                    DTO.Icon = "la la-bars";
                    break;
            }
            return DTO;
        }

        public Career ConvertToEF(CareerDTO DTO)
        {
            var EF = new Career()
            {
                CareerId = DTO.CareerId,
                CareerName = DTO.CareerName
            };
            return EF;
        }
    }
}
