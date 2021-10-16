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
    public class CandidateManage : JobHuntRepository<Candidate>
    {
        readonly ConvertDataCandidate convertData = new ConvertDataCandidate();
        private JobHuntEntities db = null;
        private JobHuntRepository<Candidate> repo = new JobHuntRepository<Candidate>();

        readonly CandidateDAO dao = new CandidateDAO();

        /// <summary>
        /// Thêm mới
        /// </summary>
        /// <param name="DTO"></param>
        /// <returns></returns>
        public bool Insert(CandidateDTO DTO)
        {
            try
            {
                return dao.Insert(convertData.ConvertToEF(DTO));
            }
            catch (Exception)
            {
                return false;
            }
        }


        public bool Delete(int idCandidateInfo)
        {
            try
            {
                repo.Delete(idCandidateInfo);
                repo.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public int DeleteCandidate(int idCandidateInfo)
        {
            try
            {

                return dao.DeleteCandidate(idCandidateInfo);
            }
            catch (Exception)
            {
                return 0;
            }
        }


        public CandidateDTO GetCandidateInfoById(int idCandidateInfo)
        {
            var getCandidateInfo = convertData.ConvertToDTO(repo.SelectById(idCandidateInfo));
            return getCandidateInfo;
        }
        public int ActiveCdd(int idCandidateInfo)
        {
            try
            {
                return dao.ActiveCandidate(idCandidateInfo);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public CandidateDTO GetCandidateInfoByIdAspNetUser(string idAspNetUser)
        {
            if (string.IsNullOrEmpty(idAspNetUser))
                return null;
            db = new JobHuntEntities();
            var candidateEF = db.Candidates.FirstOrDefault(x => x.Cdd_AspNetUserId.Equals(idAspNetUser));
            if (candidateEF == null)
                return null;
            var getCandidateInfo = convertData.ConvertToDTO(candidateEF);
            return getCandidateInfo;
        }

       

        public IEnumerable<CandidateDTO> GetAllCandidateInfo()
        {

            var getEF = repo.SelectAll();
            var listDTO = new List<CandidateDTO>();
            getEF = getEF.Where(x => x.CPStatus != (int)Common.Enum.StatusCandidate.Deleted).ToList();
            foreach (var cdd in getEF)
            {
                listDTO.Add(convertData.ConvertToDTO(cdd));
            }
            return listDTO;
        }


        public bool UpdateProfileCandidate(CandidateDTO candidateDTO)
        {
            return new CandidateDAO().UpdateProfileCandidate(convertData.ConvertToEF(candidateDTO));
        }

        public bool UpdateSocialAndContact(CandidateDTO candidateDTO)
        {
            return new CandidateDAO().UpdateSocialAndContact(convertData.ConvertToEF(candidateDTO));
        }


        public bool UpdateCV(CandidateDTO candidatedto)
        {
            return new CandidateDAO().UploadCV(convertData.ConvertToEF(candidatedto));
        }



        public IEnumerable<CandidateDTO> GetListCandidateHaveSearchAndPaging(string keyWord, int? idCity, int? idProfesstion, int? idProfesstionRecruit, int page, int pageSize)
        {
            db = new JobHuntEntities();

            List<CandidateDTO> listCandidateDTO = new List<CandidateDTO>();
            List<Candidate> SearchList = new List<Candidate>();

            var checkIdRecruit = false;

            if (SearchList.Count == 0)
            {
                SearchList = db.Candidates.ToList();
            }


            if (!string.IsNullOrEmpty(keyWord))
            {
                SearchList = SearchList.Where(x => x.CddUserName.Contains(keyWord) ||
                x.Profession.PFName.Contains(keyWord)).ToList();
                checkIdRecruit = true;
            }


            if (idProfesstion != null)
            {
                SearchList = SearchList.Where(x => x.CP_ProfessionId == idProfesstion).ToList();
                checkIdRecruit = true;
            }

            if (idCity != null)
            {
                SearchList = SearchList.Where(x => x.City.CityId == idCity).ToList();
                checkIdRecruit = true;
            }
            if (idProfesstionRecruit != null && checkIdRecruit == false)
            {
                var getList = SearchList.Where(x => x.CP_ProfessionId == idProfesstionRecruit);
                var indexCount = getList.Count();
                var count = 0;
                foreach (var item in getList.ToList())
                {
                    var get = SearchList.FindIndex(x => x.CandidateId == item.CandidateId);
                    var temp = SearchList[get];
                    SearchList[get] = SearchList[count];
                    SearchList[count] = item;
                    count++;
                }

            }

            SearchList = SearchList.Where(x => x.CPStatus != (int)Common.Enum.StatusCandidate.Deleted).ToList();
            foreach (var mb in SearchList)
            {
                listCandidateDTO.Add(convertData.ConvertToDTO(mb));
            }

            db = null;
            return listCandidateDTO.ToPagedList(page, pageSize);
        }



        public int CheckSaveJob(string userID, int idJob)
        {
            db = new JobHuntEntities();
            var getCandidate = GetCandidateInfoByIdAspNetUser(userID);
            if (getCandidate == null)
                return 0;
            var getIdCandidate = getCandidate.CandidateId;
            var resultCheck = db.SaveJobs.FirstOrDefault(x => x.SJ_CandidateId == getIdCandidate && x.SJ_RecruitJobId == idJob);
            db = null;
            if (resultCheck == null)
                return 0;
            return resultCheck.SaveJobId;
        }

        //get list save jobs GetListSaveJobs
        public IEnumerable<RecruitJobDTO> GetListSaveJobs(string userID, int page, int pageSize)
        {
            db = new JobHuntEntities();

            List<RecruitJobDTO> listRecruitJobsDTO = new List<RecruitJobDTO>();
            List<RecruitJob> ListRecruitJobs = new List<RecruitJob>();
            List<SaveJob> ListSaveJobs = new List<SaveJob>();

            var GetInfoCandidateByIdUser = GetCandidateInfoByIdAspNetUser(userID);

            ListSaveJobs = db.SaveJobs.Where(x => x.SJ_CandidateId == GetInfoCandidateByIdUser.CandidateId).ToList();

            if (ListSaveJobs != null)
            {
                foreach (var sv in ListSaveJobs)
                {
                    ListRecruitJobs.Add(sv.RecruitJob);
                }
            }


            foreach (var mb in ListRecruitJobs)
            {
                listRecruitJobsDTO.Add(new ConvertDataRecruitJob().ConvertToDTO(mb));
            }
            db = null;
            return listRecruitJobsDTO.ToPagedList(page, pageSize);
        }

        //update status candidate
        public bool UpdateStatusCandidate(int? status, int idCandidate)
        {
            return new CandidateDAO().UpdateStatus(status, idCandidate);
        }

        //get list job
        public List<CandidateDTO> GetListCandidates(int? status )
        {
            db = new JobHuntEntities();
           
            List<CandidateDTO> listCandidateDTO = new List<CandidateDTO>();
            var getLists = db.Candidates.Where(x => x.CPStatus != (int)Common.Enum.StatusCandidate.Deleted).OrderByDescending(x => x.CddRegisterDate).ToList();
           // var getListsByDate = db.Candidates.Where(x => x.CddRegisterDate >= startdate && x.CddRegisterDate <= enddate).ToList();
            if (status != null)
                getLists = getLists.Where(x => x.CPStatus == status).ToList();
            foreach (var cdd in getLists)
            {
                listCandidateDTO.Add(convertData.ConvertToDTO(cdd));
            }
            return listCandidateDTO;
        }
        // get by date
        public List<CandidateDTO> GetListCandidatesByDate(DateTime? startdate, DateTime? enddate)
        {

            db = new JobHuntEntities();
            List<CandidateDTO> listCandidateDTO = new List<CandidateDTO>();
            var getListsDate = db.Candidates.Where(x => x.CddRegisterDate >= startdate && x.CddRegisterDate <= enddate).ToList();
            foreach (var cdd in getListsDate)
            {
                listCandidateDTO.Add(convertData.ConvertToDTO(cdd));
            }
            return listCandidateDTO;
        }
        
        //update candidate
        public int UpdateCandidate(CandidateDTO cdddto)
        {
            return new CandidateDAO().UpdateCandidate(convertData.ConvertToEF(cdddto));
        }

        public List<CandidateDTO> GetListCandidatesBySearch(string keyWord, int? status)
        {
            db = new JobHuntEntities();

            List<CandidateDTO> listCandidateDTO = new List<CandidateDTO>();
            var getLists = db.Candidates.Where(x => x.CPStatus != (int)Common.Enum.StatusCandidate.Deleted).OrderByDescending(x => x.CddRegisterDate).ToList();
            foreach (var cdd in getLists)
            {
                listCandidateDTO.Add(convertData.ConvertToDTO(cdd));
            }
            if (!string.IsNullOrEmpty(keyWord))
                listCandidateDTO = listCandidateDTO.Where(x => x.CddUserName.Contains(keyWord) || x.CddFullName.Contains(keyWord) || x.ProfessionDTO.PFName.Contains(keyWord) || x.CityDTO.CName.Contains(keyWord) || x.registerDateString.Contains(keyWord) || x.statusString.Contains(keyWord)).ToList();
            if (status != null)
                listCandidateDTO = listCandidateDTO.Where(x => x.CPStatus == status).ToList();
            return listCandidateDTO;
        }
    }
}
