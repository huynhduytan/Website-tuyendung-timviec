using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobHunt.BU.DTO
{
    public class DistrictDTO
    {
        public int DistrictId { get; set; }
        public string DistrictName { get; set; }
        public int? DT_CityId { get; set; }

        public virtual CityDTO CityDTO { get; set; }
    }
}
