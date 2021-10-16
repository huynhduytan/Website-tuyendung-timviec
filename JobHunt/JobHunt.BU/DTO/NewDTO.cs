using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobHunt.BU.DTO
{
    public class NewDTO
    {
        public int NewsId { get; set; }
        public string NTitle { get; set; }
        public string NQuote { get; set; }
        public string NDetail { get; set; }
        public DateTime? NPostDate { get; set; }
        public DateTime? NPublicDate { get; set; }
        public int? NType { get; set; }
        public int? N_WebmasterInfoId { get; set; }
        public int? N_CategoryId { get; set; }
        public int? Nstatus { get; set; }
        public string NAvatar { get; set; }

        public virtual CategoryDTO CategoryDTO { get; set; }
        public virtual WebmasterInfoDTO WebmasterInfoDTO { get; set; }


        //
        public string NameStatus { get; set; }
        public string NameType { get; set; }
    }
}
