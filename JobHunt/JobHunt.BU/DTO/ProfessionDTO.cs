using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobHunt.BU.DTO
{
    public class ProfessionDTO
    {
        public int ProfessionId { get; set; }
        public int? PFCareerId { get; set; }
        public string PFName { get; set; }
        public virtual CareerDTO CareerDTO { get; set; }

        //
        public string Icon { get; set; }
    }
}
