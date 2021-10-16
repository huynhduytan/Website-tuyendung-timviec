using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobHunt.BU.ConvertData;
using JobHunt.BU.DTO;
using JobHunt.Model.EF;
using JobHunt.Repository;

namespace JobHunt.BU.Manage
{
    public class ProfessionManage : JobHuntRepository<Profession>
    {
        readonly ConvertDataProfession convertData = new ConvertDataProfession();
      
        private JobHuntRepository<Profession> repo = new JobHuntRepository<Profession>();

        public IEnumerable<ProfessionDTO> GetAllProfessions()
        {
            var getEF = repo.SelectAll();
            var listDTO = new List<ProfessionDTO>();
            foreach (var ef in getEF)
            {
                listDTO.Add(convertData.ConvertToDTO(ef));
            }
            return listDTO;
        }

        public ProfessionDTO GetDetailProfession(int idProfession)
        {
            var DTO = convertData.ConvertToDTO(repo.SelectById(idProfession));
            return DTO;
        }
    }
}
