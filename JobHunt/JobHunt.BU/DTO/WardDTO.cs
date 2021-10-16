using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobHunt.BU.DTO
{
    public class WardDTO
    {
        public int WardId { get; set; }
        public string WardName { get; set; }
        public int? W_DistrictId { get; set; }

        public virtual DistrictDTO DistrictDTO { get; set; }
    }
}
