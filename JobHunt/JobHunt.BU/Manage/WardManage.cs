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
    public class WardManage
    {
        readonly ConvertDataWard convertData = new ConvertDataWard();
        //private JobHuntEntities db = null;
        private JobHuntRepository<Ward> repo = new JobHuntRepository<Ward>();

        JobHuntEntities db = null;
        public WardManage()
        {
            db = new JobHuntEntities();
        }

        public List<WardDTO> GetAllWards()
        {
            var getEF = repo.SelectAll();
            var listDTO = new List<WardDTO>();
            foreach (var ct in getEF)
            {
                listDTO.Add(convertData.ConvertToDTO(ct));
            }
            return listDTO;
        }

        //Lấy ra list xã by id huyện
        public List<WardDTO> GetListWardsByIdDistrict(int idDistrict)
        {
            var getListWards = db.Wards.Where(x => x.W_DistrictId == idDistrict).ToList();
            var listWardsDTO = new List<WardDTO>();
            foreach (var h in getListWards)
            {
                listWardsDTO.Add(convertData.ConvertToDTO(h));
            }
            return listWardsDTO;
        }

        //Lấy ra thông tin xã
        public WardDTO GetWardById(int? id)
        {
            var getWard = db.Wards.Find(id);
            if (getWard == null)
                return null;
            var WardDTO = convertData.ConvertToDTO(getWard);
            return WardDTO;
        }
    }
}
