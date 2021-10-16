using JobHunt.BU.ConvertData;
using JobHunt.BU.DTO;
using JobHunt.Model.DAO;
using JobHunt.Model.EF;
using JobHunt.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobHunt.BU.Manage
{
    public class AdvertisementManage
    {
        readonly ConvertDataAdvertisement convertData = new ConvertDataAdvertisement();
        private JobHuntEntities db = null;
        private JobHuntRepository<Advertisement> repo = new JobHuntRepository<Advertisement>();
        AdvertisementDAO dao = new AdvertisementDAO();

        public int Insert(AdvertisementDTO DTO)
        {
            try
            {
                repo.Insert(convertData.ConvertToEF(DTO));
                repo.SaveChanges();
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int Update(AdvertisementDTO DTO)
        {
            try
            {
                return dao.Update(convertData.ConvertToEF(DTO));
            }
            catch (Exception)
            {
                return 0;
            }
        }


        public int Delete(int idAdvertisementInfo)
        {
            try
            {
                return dao.Delete(idAdvertisementInfo);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public List<AdvertisementDTO> GetAll()
        {
            var listDTOs = new List<AdvertisementDTO>();
            var listEFs = new List<Advertisement>();
            using (var db = new JobHuntEntities())
            {
                listEFs = db.Advertisements.Where(x=>x.AdStatus != (int)BU.Common.Enum.StatusAdvertisement.Deleted).ToList();
                foreach (var ad in listEFs)
                {
                    listDTOs.Add(convertData.ConvertToDTO(ad));
                }
                //listEFs = db.Advertisements.Where()
            }
            return listDTOs;
        }

        //lấy danh sách quảng cáo trước 3 ngày hết hạn
        public List<AdvertisementDTO> GetListAdvertisementDashboard()
        {
            var datenow = DateTime.Now;
            var dateexpired = datenow.AddDays(3);
            var listDTOs = new List<AdvertisementDTO>();
            var listEFs = new List<Advertisement>();
            using (var db = new JobHuntEntities())
            {
                listEFs = db.Advertisements.Where(x => x.AdStatus != (int)BU.Common.Enum.StatusAdvertisement.Deleted && x.AdEndDate <= dateexpired && x.AdEndDate >= datenow).ToList();
                foreach (var ad in listEFs)
                {
                    listDTOs.Add(convertData.ConvertToDTO(ad));
                }
                //listEFs = db.Advertisements.Where()
            }
            return listDTOs;
        }
        // Lấy danh sách quảng cáo từ ngày đến ngày
      /*  public List<AdvertisementDTO> GetListAdvertisementByDate( DateTime AdStartDate, DateTime AdEnđate)
       {
            var datenow = DateTime.Now;
            var ListAd = new List<AdvertisementDTO>();
            var listAdEFs = new List<Advertisement>();
            using (var db = new JobHuntEntities())
            {
                listAdEFs = db.Advertisements.Where(x => x.AdStatus)
            }

        }*/
        public bool CheckExistAd(AdvertisementDTO addto)
        {
            var result = false;
            using (var db = new JobHuntEntities())
            {
                result = db.Advertisements.Any(a => a.AdEndDate >= addto.AdStartDate && a.AdPosition == addto.AdPosition && a.AdvertisementId != addto.AdvertisementId);
                //listEFs = db.Advertisements.Where()
            }
            return result;
        }

        public AdvertisementDTO GetAdvertisementByID(int id)
        {
            var dto = new AdvertisementDTO();
            using (var db = new JobHuntEntities())
            {
                dto = convertData.ConvertToDTO(db.Advertisements.Find(id));
                //listEFs = db.Advertisements.Where()
            }
            return dto;
        }

        //
        public List<AdvertisementDTO> GetListAdvertisementsBySearch(string keyWord, int? status)
        {
            db = new JobHuntEntities();

            List<AdvertisementDTO> listAdvertisementDTO = new List<AdvertisementDTO>();
            var getLists = db.Advertisements.Where(x => x.AdStatus != (int)Common.Enum.StatusAdvertisement.Deleted).OrderByDescending(x => x.AdStartDate).ToList();
            
            foreach (var ad in getLists)
            {
                listAdvertisementDTO.Add(convertData.ConvertToDTO(ad));
            }
            if (!string.IsNullOrEmpty(keyWord))
                listAdvertisementDTO = listAdvertisementDTO.Where(x => x.AdName.Contains(keyWord) || x.nameStatus.Contains(keyWord) || x.AdEndDateString.Contains(keyWord) || x.AdStartDateString.Contains(keyWord)).ToList();
            if (status != null)
                listAdvertisementDTO = listAdvertisementDTO.Where(x => x.AdStatus == status).ToList();
            return listAdvertisementDTO;
        }
    }
}
