using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobHunt.BU.DTO
{
    public class SignUpNewsletterDTO
    {
        public int RegisterID { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public bool CheckNew { get; set; }
        public bool CheckPost { get; set; }
        public int? IdProfession { get; set; }
    }
}
