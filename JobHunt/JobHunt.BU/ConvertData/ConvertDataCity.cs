using JobHunt.BU.DTO;
using JobHunt.Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobHunt.BU.ConvertData
{
    public class ConvertDataCity
    {
        public CityDTO ConvertToDTO(City EF)
        {
            var DTO = new CityDTO()
            {
                CityId = EF.CityId,
                CName = EF.CName
            };
            return DTO;
        }

        public City ConvertToEF(CityDTO DTO)
        {
            var EF = new City()
            {
                CityId = DTO.CityId,
                CName = DTO.CName
            };
            return EF;
        }
    }
}
