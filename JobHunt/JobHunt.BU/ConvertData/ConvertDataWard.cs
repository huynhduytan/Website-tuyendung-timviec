using JobHunt.BU.DTO;
using JobHunt.Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobHunt.BU.ConvertData
{
    class ConvertDataWard
    {
        public WardDTO ConvertToDTO(Ward EF)
        {
            var DTO = new WardDTO()
            {
                WardId = EF.WardId,
                WardName = EF.WardName,
                W_DistrictId = EF.W_DistrictId
            };

            if (EF.District != null)
            {
                DTO.DistrictDTO = new ConvertDataDistrict().ConvertToDTO(EF.District);
            }
            return DTO;
        }

        public Ward ConvertToEF(WardDTO DTO)
        {
            var EF = new Ward()
            {
                WardId = DTO.WardId,
                WardName = DTO.WardName,
                W_DistrictId = DTO.W_DistrictId
            };
            return EF;
        }
    }
}
