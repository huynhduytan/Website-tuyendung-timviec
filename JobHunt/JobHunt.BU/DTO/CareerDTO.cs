using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobHunt.BU.DTO
{
    public class CareerDTO
    {
        public int CareerId { get; set; }
        public string CareerName { get; set; }


        //
        public string Icon { get; set; }
        public int CountJob { get; set; }
    }
}
