using JobHunt.BU.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobHunt.BU.DTO
{
    public class WebmasterInfoDTO
    {
        public int WebmasterInfoId { get; set; }
        public string WI_AspNetUserId { get; set; }
        public string WIUserName { get; set; }
        public string WIPassword { get; set; }
        public string WIFullName { get; set; }
        public int? WIType { get; set; }
        public int? WIStatus { get; set; }
        public int? WIGender { get; set; }
        public DateTime? WIBirthDay { get; set; }
        public DateTime? WIDateStart { get; set; }
        public string WIAddress { get; set; }
        public int? WIPosition { get; set; }

        public virtual AspNetUserDTO AspNetUserDTO { get; set; }

        //
        public string nameRole { get; set; }
        public string WIEmail { get; set; }
        public string WIPhoneNumber { get; set; }
        public string WIBirthdayString { get; set; }
        public string WIStatusString { get; set; }
    }
}
