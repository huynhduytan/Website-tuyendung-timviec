using JobHunt.BU.ConvertData;
using JobHunt.BU.DTO;
using JobHunt.Model.EF;
using JobHunt.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobHunt.BU.Manage
{
    public class ExperienceManage
    {
        readonly ConvertDataExperience convertData = new ConvertDataExperience();
        //private JobHuntEntities db = null;
        private JobHuntRepository<Experience> repo = new JobHuntRepository<Experience>();

        public IEnumerable<ExperienceDTO> GetAllExperiences()
        {
            var getEF = repo.SelectAll();
            var listDTO = new List<ExperienceDTO>();
            foreach (var ef in getEF)
            {
                listDTO.Add(convertData.ConvertToDTO(ef));
            }
            return listDTO;
        }
    }
}
