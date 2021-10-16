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
    public class DistrictManage
    {
        readonly ConvertDataDistrict convertData = new ConvertDataDistrict();
        //private JobHuntEntities db = null;
        private JobHuntRepository<District> repo = new JobHuntRepository<District>();
        JobHuntEntities db = null;
        public DistrictManage()
        {
            db = new JobHuntEntities();
        }

        public IEnumerable<DistrictDTO> GetAllDistricts()
        {
            var getEF = repo.SelectAll();
            var listDTO = new List<DistrictDTO>();
            foreach (var ef in getEF)
            {
                listDTO.Add(convertData.ConvertToDTO(ef));
            }
            return listDTO;
        }


        //Lấy ra list Huyện by id tinh
        public List<DistrictDTO> GetListDistrictsByIdCity(int idCity)
        {
            var getListDistricts = db.Districts.Where(x => x.DT_CityId == idCity).ToList();
            var listDistrictsDTO = new List<DistrictDTO>();
            foreach (var h in getListDistricts)
            {
                listDistrictsDTO.Add(convertData.ConvertToDTO(h));
            }
            return listDistrictsDTO;
        }


        //Lấy ra thông tin huyện
        public DistrictDTO GetDistrictById(int? id)
        {
            var getDistrict = db.Districts.Find(id);
            if (getDistrict == null)
                return null;
            var DistrictDTO = convertData.ConvertToDTO(getDistrict);
            return DistrictDTO;
        }
    }
}
