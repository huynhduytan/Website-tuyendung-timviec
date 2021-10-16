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
    public class RecruitManage : JobHuntRepository<Recruit>
    {

        

        readonly ConvertDataRecruit convertData = new ConvertDataRecruit();
        //private JobHuntEntities db = null;
        private JobHuntRepository<Recruit> repo = new JobHuntRepository<Recruit>();

        RecruitDAO dao = new RecruitDAO();
        JobHuntEntities db = null;
        public int Insert(RecruitDTO DTO)
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


        public int Delete(int idRecruit)
        {
            try
            {

                return dao.Delete(idRecruit);
            }
            catch (Exception)
            {
                return 0;
            }
        }


        public RecruitDTO GetRecruitInfoByIdAspNetUser(string idAspNetUser)
        {
            if (string.IsNullOrEmpty(idAspNetUser))
                return null;
            db = new JobHuntEntities();
            var RecruitEF = db.Recruits.FirstOrDefault(x => x.RI_AspNetUserId.Equals(idAspNetUser));
            if (RecruitEF == null)
                return null;
            var getRecruitInfo = convertData.ConvertToDTO(RecruitEF);
            return getRecruitInfo;
        }



        public RecruitDTO GetRecruitInfoById(int idRecruitInfo)
        {
            var getRecruitInfo = convertData.ConvertToDTO(repo.SelectById(idRecruitInfo));
            return getRecruitInfo;
        }

        public List<RecruitDTO> GetAllRecruits()
        {
            db = new JobHuntEntities();
            List<RecruitDTO> listRecruitDTO = new List<RecruitDTO>();
            var getListRecruits = db.Recruits.Where(x => x.RIStatus != (int)Common.Enum.StatusRecruit.Deleted).ToList();
            foreach (var rc in getListRecruits)
            {
                listRecruitDTO.Add(convertData.ConvertToDTO(rc));
            }
            db = null;
            return listRecruitDTO;
        }


        public IEnumerable<RecruitDTO> GetListRecruitHaveSearchAndPaging(string keyWord, int? idCity, int? idProfession, string chooseDate, List<int?> chooseWorkType, List<int?> chooseSalary, List<int?> chooseExperience, List<int?> chooseGender, List<int?> chooseLevel, int page, int pageSize)
        {
            db = new JobHuntEntities();

            List<RecruitDTO> listRecruitDTO = new List<RecruitDTO>();
            List<Recruit> SearchList = new List<Recruit>();


            if (SearchList.Count == 0)
            {
                SearchList = db.Recruits.ToList();
            }

            if (!string.IsNullOrEmpty(keyWord))
            {
                SearchList = SearchList.Where(x => x.RIFullName.Contains(keyWord)).ToList();
            }

            if (!string.IsNullOrEmpty(chooseDate))
            {
                var currentTime = DateTime.Now;
                var getTimeHour = int.Parse(chooseDate.Remove(chooseDate.Length - 1));
                if (getTimeHour != 0)
                {
                    TimeSpan aInterval = new TimeSpan(0, getTimeHour, 0, 0);
                    var getTime = currentTime - aInterval;
                    SearchList = SearchList.Where(x => x.RIRegisterDate >= getTime).ToList();
                }
            }


            //if (idProfesstion != null)
            //    SearchList = SearchList.Where(x => x.CP_ProfessionId == idProfesstion).ToList();


            SearchList = SearchList.Where(x => x.RIStatus != (int)Common.Enum.StatusRecruit.Deleted).ToList();
            foreach (var mb in SearchList)
            {
                listRecruitDTO.Add(convertData.ConvertToDTO(mb));
            }

            db = null;
            return listRecruitDTO.ToPagedList(page, pageSize);
        }
        //update status recruit
        public bool UpdateStatusRecruit(int? status, int idRecruit)
        {
            return new RecruitDAO().UpdateStatus(status, idRecruit);
        }


        //update profile recruit
        public int UpdateProfileRecruit(RecruitDTO rcdto, int type)
        {
            return new RecruitDAO().UpdateProfile(convertData.ConvertToEF(rcdto), type);
        }


        //update profile recruit
        public int UpdateRecruit(RecruitDTO rcdto)
        {
            return new RecruitDAO().Update(convertData.ConvertToEF(rcdto));
        }


        //
        public List<RecruitDTO> GetListRecruits()
        {
            db = new JobHuntEntities();
            List<RecruitDTO> listRecruitsDTO = new List<RecruitDTO>();
            var getLists = db.Recruits.Where(x=>x.RIStatus != (int)Common.Enum.StatusRecruit.Deleted).OrderByDescending(x => x.RIRegisterDate).ToList();
      //      var getListByDate = db.Recruits.Where(x => x.RIRegisterDate >= startdate && x.RIRegisterDate <= enddate).ToList();
            foreach (var cdd in getLists)
            {
                listRecruitsDTO.Add(convertData.ConvertToDTO(cdd));
            }
            return listRecruitsDTO;
        }


        //
        public List<RecruitDTO> GetListRecruitsBySearch(string keyWord, int? status)
        {
            db = new JobHuntEntities();

            List<RecruitDTO> listRecruitDTO = new List<RecruitDTO>();
            var getLists = db.Recruits.Where(x => x.RIStatus != (int)Common.Enum.StatusRecruit.Deleted).OrderByDescending(x => x.RIRegisterDate).ToList();

            foreach (var cdd in getLists)
            {
                listRecruitDTO.Add(convertData.ConvertToDTO(cdd));
            }

            if (!string.IsNullOrEmpty(keyWord))
                listRecruitDTO = listRecruitDTO.Where(x => x.RIUserName.Contains(keyWord) || x.RIFullName.Contains(keyWord) || x.ProfessionDTO.PFName.Contains(keyWord) || x.CityDTO.CName.Contains(keyWord)).ToList();
            if (status != null)
                listRecruitDTO = listRecruitDTO.Where(x => x.RIStatus == status).ToList();
            return listRecruitDTO;
        }
        public List<RecruitDTO> GetListRecruits(DateTime? startdate, DateTime? enddate)
        {
            db = new JobHuntEntities();

            List<RecruitDTO> listRecruitDTO = new List<RecruitDTO>();
            var getLists = db.Recruits.Where(x => x.RIRegisterDate >= startdate && x.RIRegisterDate <= enddate).ToList();
            foreach (var cdd in getLists)
            {
                listRecruitDTO.Add(convertData.ConvertToDTO(cdd));
            }
            
            return listRecruitDTO;
        }
        
    }
}
