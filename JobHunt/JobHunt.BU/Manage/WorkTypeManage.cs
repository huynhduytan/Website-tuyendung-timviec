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
    public class WorkTypeManage
    {
        readonly ConvertDataWorkType convertData = new ConvertDataWorkType();
        //private JobHuntEntities db = null;
        private JobHuntRepository<WorkType> repo = new JobHuntRepository<WorkType>();

        public IEnumerable<WorkTypeDTO> GetAllWorkTypes()
        {
            var getEF = repo.SelectAll();
            var listDTO = new List<WorkTypeDTO>();
            foreach (var ef in getEF)
            {
                listDTO.Add(convertData.ConvertToDTO(ef));
            }
            return listDTO;
        }
    }
}
