using JobHunt.BU.DTO;
using JobHunt.Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobHunt.BU.ConvertData
{
    public class ConvertDataCompanySize
    {
        public CompanySizeDTO ConvertToDTO(CompanySize EF)
        {
            var DTO = new CompanySizeDTO()
            {
                CompanySizeId = EF.CompanySizeId,
                CSMax = EF.CSMax,
                CSMin = EF.CSMin,
                CSShow = EF.CSShow
            };
            return DTO;
        }

        public CompanySize ConvertToEF(CompanySizeDTO DTO)
        {
            var EF = new CompanySize()
            {
                CompanySizeId = DTO.CompanySizeId,
                CSMax = DTO.CSMax,
                CSMin = DTO.CSMin,
                CSShow = DTO.CSShow
            };
            return EF;
        }
    }
}
