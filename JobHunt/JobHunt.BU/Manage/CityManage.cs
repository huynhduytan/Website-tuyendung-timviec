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
    public class CityManage : JobHuntRepository<City>
    {
        readonly ConvertDataCity convertData = new ConvertDataCity();
        //private JobHuntEntities db = null;
        private JobHuntRepository<City> repo = new JobHuntRepository<City>();

        //Tên kết nối khi mọi người tạo.
        //JobHuntEntities db = null;

        public List<CityDTO> GetAllCities()
        {
            var getEF = repo.SelectAll();
            var listDTO = new List<CityDTO>();
            foreach (var ct in getEF)
            {
                listDTO.Add(convertData.ConvertToDTO(ct));
            }
            return listDTO;
        }


    }
}
