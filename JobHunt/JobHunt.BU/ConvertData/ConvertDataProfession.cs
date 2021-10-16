using JobHunt.BU.DTO;
using JobHunt.Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobHunt.BU.ConvertData
{
    public class ConvertDataProfession
    {
        public ProfessionDTO ConvertToDTO(Profession EF)
        {
            var DTO = new ProfessionDTO()
            {
                ProfessionId = EF.ProfessionId,
                PFCareerId = EF.PFCareerId,
                PFName = EF.PFName
            };
            if (EF.Career != null)
                DTO.CareerDTO = new ConvertDataCareer().ConvertToDTO(EF.Career);
            
            return DTO;
        }

        public Profession ConvertToEF(ProfessionDTO DTO)
        {
            var EF = new Profession()
            {
                ProfessionId = DTO.ProfessionId,
                PFCareerId = DTO.PFCareerId,
                PFName = DTO.PFName,
            };
            if (DTO.CareerDTO != null)
                EF.Career = new ConvertDataCareer().ConvertToEF(DTO.CareerDTO);
            return EF;
        }
    }
}
