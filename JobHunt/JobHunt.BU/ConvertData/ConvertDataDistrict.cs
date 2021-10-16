using JobHunt.BU.DTO;
using JobHunt.Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobHunt.BU.ConvertData
{
    public class ConvertDataDistrict
    {
        public DistrictDTO ConvertToDTO(District EF)
        {
            var DTO = new DistrictDTO()
            {
                DistrictId = EF.DistrictId,
                DistrictName = EF.DistrictName,
                DT_CityId = EF.DT_CityId
            };

            if(EF.City != null)
            {
                DTO.CityDTO = new ConvertDataCity().ConvertToDTO(EF.City);
            }
            return DTO;
        }

        public District ConvertToEF(DistrictDTO DTO)
        {
            var EF = new District()
            {
                DistrictId = DTO.DistrictId,
                DistrictName = DTO.DistrictName,
                DT_CityId = DTO.DT_CityId
            };
            return EF;
        }
    }
}
