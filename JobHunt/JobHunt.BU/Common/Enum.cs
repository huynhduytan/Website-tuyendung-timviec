using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobHunt.BU.Common
{
    public static class Enum
    {
        public enum EnumStatusJob {
            Approvaling = 1,
            Active = 2,
            Inactive = 3,
            Deleted = 4
        }
        public enum EnumTypeJob
        {
            Hot = 1,
            Normal = 2,
            Stop = 3,
            Delete = 4
        }

        public enum StatusAccount
        {
            Active = 1,
            Block = 2,
            Approvaling = 3,
            Deleted = 4
        }

        public enum StatusRecruit
        {
            Active = 1,
            Block = 2,
            Approvaling = 3,
            Deleted = 4
        }

        public enum StatusCandidate
        {
            Active = 1,
            Block = 2,
            Approvaling = 3,
            Deleted = 4
            
        }

        public enum EnumCareer
        {
            BanHangTiepThi = 1,
            HangTieuDung = 2,
            KyThuat = 3,
            DichVu = 4,
            MayTinhIT = 5,
            SucKhoe = 6,
            SanXuat = 7,
            HanhChinhNhanSu = 8,
            KeToanTaiChinh = 9,
            TruyenThongMedia = 10,
            XayDung = 11,
            GiaoDucDaoTao = 12,
            KhoaHoc = 13,
            KhachSanDuLich = 14,
            NganhKhac = 15
        }


        public enum TypeUpdate
        {
            UpdateInformation = 1,
            UpdateContact = 2
        }

        public enum StatusNew
        {
            Active = 1,
            Block = 2,
            Approvaling = 3,
            Deleted = 4
        }

        public enum TypeNew
        {
            Hot = 1,
            Normal = 2
        }

        public enum TypeUser
        {
            Candidate = 1,
            Recruit = 2
        }

        public enum TypeFix
        {
            Add = 1,
            Update = 2
        }

        public enum PositionBookAd
        {
            Slider1 = 1,
            Slider2 = 2,
            Slider3 = 3
        }

        public enum StatusAdvertisement
        {
            Hide = 1,
            Show = 2,
            Deleted = 4
        }

        public enum Gender
        {
            Boy = 1,
            Girl = 2,
            Other = 3,
            NoRequired = 4
        }

        public enum Role
        {
            Admin = 1,
            Employee = 2
        }


        /*
         * ManageUser : Quản lý người tìm việc, Quản lý người tuyển dụng
         * ManageNewAndJob: Quản lý công việc, Quản lý tin tức
         * ManageWebsite: Quản lý thống kê, Quản lý website, Quản lý quảng cáo
         * */
        public enum WIPosition
        {
            ManageUser = 1,
            ManageNewAndJob = 2,
            ManageWebsite = 3,
            Admin = 4
        }
    }
}
