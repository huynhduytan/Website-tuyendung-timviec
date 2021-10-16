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
    public class LevelManage
    {
        readonly ConvertDataLevelInfo convertData = new ConvertDataLevelInfo();
        //private JobHuntEntities db = null;
        private JobHuntRepository<LevelInfo> repo = new JobHuntRepository<LevelInfo>();

        public IEnumerable<LevelInfoDTO> GetAllLevels()
        {

            var getEF = repo.SelectAll();
            var listDTO = new List<LevelInfoDTO>();
            foreach (var ct in getEF)
            {
                listDTO.Add(convertData.ConvertToDTO(ct));
            }
            return listDTO;
        }
    }
}
