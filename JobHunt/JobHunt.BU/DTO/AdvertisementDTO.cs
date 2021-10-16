using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobHunt.BU.DTO
{
    public class AdvertisementDTO
    {
        public int AdvertisementId { get; set; }
        public System.DateTime AdStartDate { get; set; }
        public System.DateTime AdEndDate { get; set; }
        public int AdPosition { get; set; }
        public int AdStatus { get; set; }
        public string AdName { get; set; }
        public string AdPhone { get; set; }
        public string AdEmail { get; set; }
        public string AdAddress { get; set; }
        public string AdLink { get; set; }
        public string AdImage { get; set; }

        //
        public string AdStartDateString { get; set; }
        public string AdEndDateString { get; set; }
        public string nameStatus { get; set; }
    }
}
