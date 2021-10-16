using JobHunt.BU.DTO;
using JobHunt.Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobHunt.BU.ConvertData
{
    public class ConvertDataExperience
    {
        public ExperienceDTO ConvertToDTO(Experience EF)
        {
            var DTO = new ExperienceDTO()
            {
                ExperienceId = EF.ExperienceId,
                EMax = EF.EMax,
                EMin = EF.EMin,
                EShow = EF.EShow
            };
            return DTO;
        }

        public Experience ConvertToEF(ExperienceDTO DTO)
        {
            var EF = new Experience()
            {
                ExperienceId = DTO.ExperienceId,
                EMax = DTO.EMax,
                EMin = DTO.EMin,
                EShow = DTO.EShow
            };
            return EF;
        }
    }
}
