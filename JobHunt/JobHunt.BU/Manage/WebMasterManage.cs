using JobHunt.BU.ConvertData;
using JobHunt.BU.DTO;
using JobHunt.Model.DAO;
using JobHunt.Model.EF;
using JobHunt.Repository;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobHunt.BU.Manage
{
    public class WebMasterManage
    {
        readonly ConvertDataWebmasterInfo convertData = new ConvertDataWebmasterInfo();
        //private JobHuntEntities db = null;
        private JobHuntRepository<WebmasterInfo> repo = new JobHuntRepository<WebmasterInfo>();
        WebmasterInfoDAO dao = new WebmasterInfoDAO();
        //Tên kết nối khi mọi người tạo.
        JobHuntEntities db = null;

        public int Insert(WebmasterInfoDTO DTO)
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

        public int Update(WebmasterInfoDTO DTO)
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

        public int Delete(int idwebmt)
        {
            try
            {

                return dao.Delete(idwebmt);
            }
            catch (Exception)
            {
                return 0;
            }
        }


        public IEnumerable<WebmasterInfoDTO> GetListWebmasterInfoHaveSearchAndPaging(string keyWord, int page, int pageSize)
        {
            db = new JobHuntEntities();

            List<WebmasterInfoDTO> listDTOs = new List<WebmasterInfoDTO>();
            List<WebmasterInfo> listEFs = db.WebmasterInfoes.ToList();

            if (!string.IsNullOrEmpty(keyWord))
            {
                listEFs = db.WebmasterInfoes.Where(x => x.WIFullName.Contains(keyWord) || x.WIUserName.Contains(keyWord) && x.WIStatus != (int)Common.Enum.StatusCandidate.Deleted).ToList();
            }
           
            foreach (var mb in listEFs)
            {
                if (mb.WIPosition == (int)BU.Common.Enum.WIPosition.Admin)
                    continue;
                else
                    listDTOs.Add(convertData.ConvertToDTO(mb));

            }

            db = null;
            return listDTOs.ToPagedList(page, pageSize);
        }

        public IEnumerable<WebmasterInfoDTO> GetListWebmasterInfos()
        {
            db = new JobHuntEntities();

            List<WebmasterInfoDTO> listDTOs = new List<WebmasterInfoDTO>();
            List<WebmasterInfo> listEFs = db.WebmasterInfoes.Where(x=>x.WIStatus != (int)BU.Common.Enum.StatusAccount.Deleted).ToList();
            foreach (var mb in listEFs)
            {
                listDTOs.Add(convertData.ConvertToDTO(mb));

            }

            db = null;
            return listDTOs;
        }
       // get by date
       public List<WebmasterInfoDTO> GetListWebmasterInforByDate(DateTime? startdate, DateTime? enddate)
        {
            db = new JobHuntEntities();

            List<WebmasterInfoDTO> listDTOs = new List<WebmasterInfoDTO>();
            var getList = db.WebmasterInfoes.Where(x => x.WIDateStart >= startdate && x.WIDateStart <= enddate);
            foreach(var mb in getList){
                listDTOs.Add(convertData.ConvertToDTO(mb));
            }
            return listDTOs;
        }

        public List<AspNetRoleDTO> GetListRoles()
        {
            db = new JobHuntEntities();
            var listDTOs = new List<AspNetRoleDTO>();
            var listEFs = db.AspNetRoles.ToList();
            foreach (var ef in listEFs)
            {
                listDTOs.Add(new ConvertDataAspNetRole().ConvertToDTO(ef));
            }
            db = null;
            return listDTOs;
        }

        public AspNetRoleDTO GetRoleById(string idrole)
        {
            db = new JobHuntEntities();
            var ef = db.AspNetRoles.FirstOrDefault(x=>x.Id.Equals(idrole));
            db = null;
            return new ConvertDataAspNetRole().ConvertToDTO(ef);
        }

        public WebmasterInfoDTO GetInfoWebmasterById(int id)
        {
            db = new JobHuntEntities();
            var dto = convertData.ConvertToDTO(db.WebmasterInfoes.Find(id));
            db = null;
            return dto;
        }
        //public List<string> 
        public WebmasterInfoDTO GetInfoWebmasterByIdUser(string idUser)
        {
            db = new JobHuntEntities();
            var ef = db.WebmasterInfoes.FirstOrDefault(x => x.WI_AspNetUserId.Equals(idUser));
            db = null;
            return convertData.ConvertToDTO(ef);
        }

        //
        public List<WebmasterInfoDTO> GetListWebmasterInfosBySearch(string keyWord, int? status)
        {
            db = new JobHuntEntities();

            List<WebmasterInfoDTO> listWebmasterInfoDTO = new List<WebmasterInfoDTO>();
            var getLists = db.WebmasterInfoes.Where(x => x.WIStatus != (int)Common.Enum.StatusAccount.Deleted).OrderByDescending(x => x.WIDateStart).ToList();

            foreach (var webmt in getLists)
            {
                listWebmasterInfoDTO.Add(convertData.ConvertToDTO(webmt));
            }
            if (!string.IsNullOrEmpty(keyWord))
                listWebmasterInfoDTO = listWebmasterInfoDTO.Where(x => x.WIUserName.Contains(keyWord) || x.WIFullName.Contains(keyWord) || x.WIAddress.Contains(keyWord) || x.WIStatusString.Contains(keyWord) || x.nameRole.Contains(keyWord) || x.WIDateStart.Value.ToString("dd-MM-yyyy").Contains(keyWord)).ToList();
            if (status != null)
                listWebmasterInfoDTO = listWebmasterInfoDTO.Where(x => x.WIStatus == status).ToList();
            return listWebmasterInfoDTO;
        }
    }
}
