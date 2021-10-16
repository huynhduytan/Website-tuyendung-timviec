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
    public class RecruitJobManage
    {
        readonly ConvertDataRecruitJob convertData = new ConvertDataRecruitJob();
        private JobHuntEntities db = null;
        private JobHuntRepository<RecruitJob> repo = new JobHuntRepository<RecruitJob>();

        RecruitJobDAO dao = new RecruitJobDAO();



        public RecruitJobDTO Insert(RecruitJobDTO DTO)
        {
            try
            {
                var result = repo.Insert(convertData.ConvertToEF(DTO));
                repo.SaveChanges();
                return convertData.ConvertToDTO(result);
            }
            catch (Exception)
            {
                return null;
            }
        }
        public bool DeleteJob(int idJob)
        {
            try
            {
                return new RecruitJobDAO().Delete(idJob);
            }
            catch (Exception)
            {
                return false;
            }
        }
        
        public IEnumerable<RecruitJobDTO> GetAllRecruitJobs()
        {

            var getEF = repo.SelectAll().Where(x => x.RJExpirationDate >= DateTime.Now && x.RJStatus != (int)Common.Enum.EnumStatusJob.Inactive);
            var listDTO = new List<RecruitJobDTO>();
            foreach (var cdd in getEF)
            {
                listDTO.Add(convertData.ConvertToDTO(cdd));
            }
            return listDTO;
        }

        public List<RecruitJobDTO> GetAllListRecruitJobs()
        {

            var getEF = repo.SelectAll().Where(x => x.RJExpirationDate >= DateTime.Now && x.RJStatus != (int)Common.Enum.EnumStatusJob.Inactive);
            var listDTO = new List<RecruitJobDTO>();
            foreach (var cdd in getEF)
            {
                listDTO.Add(convertData.ConvertToDTO(cdd));
            }
            return listDTO;
        }
        public List<RecruitJobDTO> GetAllListRecruitJobsByDate(DateTime? startdate, DateTime? enddate)
        {
            db = new JobHuntEntities();
            var listDTO = new List<RecruitJobDTO>();
            var getList = db.RecruitJobs.Where(x => x.RJExpirationDate >= startdate && x.RJExpirationDate <= enddate);
            foreach (var cdd in getList)
            {
                listDTO.Add(convertData.ConvertToDTO(cdd));
            }
            return listDTO;
        }
        public RecruitJobDTO GetRecruitJobByID(int id)
        {
            var getEF = repo.SelectById(id);
            var DTO = convertData.ConvertToDTO(getEF);
            return DTO;
        }

        //get top find job
        public List<RecruitJobDTO> GetListFindJob()
        {
            db = new JobHuntEntities();
            List<RecruitJobDTO> listRecruitJobDTO = new List<RecruitJobDTO>();
            var getLists = db.RecruitJobs.Where(x => x.RJStatus != (int)Common.Enum.EnumStatusJob.Deleted).OrderByDescending(x => x.RJCount).ToList();
            foreach (var job in getLists)
            {
                listRecruitJobDTO.Add(convertData.ConvertToDTO(job));
            }

            return listRecruitJobDTO;
        }

        //get list recruit job by status
        public IEnumerable<RecruitJobDTO> GetListRecruitJobsByType(int? type)
        {
            db = new JobHuntEntities();
            var getEF = db.RecruitJobs.Where(x => x.RJType == type && x.RJExpirationDate >= DateTime.Now && x.RJStatus != (int?)Common.Enum.EnumStatusJob.Inactive).ToList();
            if (getEF.Count == 0)
                getEF = db.RecruitJobs.Where(x => x.RJExpirationDate >= DateTime.Now && x.RJStatus != (int?)Common.Enum.EnumStatusJob.Inactive).ToList();
            var listDTO = new List<RecruitJobDTO>();
            foreach (var cdd in getEF)
            {
                listDTO.Add(convertData.ConvertToDTO(cdd));
            }
            return listDTO;
        }

        //get list recruit job by time
        public IEnumerable<RecruitJobDTO> GetListRecruitJobsByTime()
        {
            db = new JobHuntEntities();
            var getEF = db.RecruitJobs.OrderByDescending(x => x.RJPostDate).Where(x => x.RJType != (int?)BU.Common.Enum.EnumStatusJob.Inactive && x.RJExpirationDate >= DateTime.Now).ToList();
            var listDTO = new List<RecruitJobDTO>();
            foreach (var cdd in getEF)
            {
                listDTO.Add(convertData.ConvertToDTO(cdd));
            }
            return listDTO;
        }

        public IEnumerable<CandidateDTO> GetListCandidatesAppliedPaging(int idRecruit, int pageNumber, int pageSize)
        {
            db = new JobHuntEntities();
            var getEF = db.CandidatePostResumes.Where(x => x.RecruitJob.Recruit.RecruitId == idRecruit).ToList();
            var listDTO = new List<CandidateDTO>();
            foreach (var cdd in getEF)
            {
                var cddDTO = new ConvertDataCandidate().ConvertToDTO(cdd.Candidate);
                //
                var getRecruitJob = db.RecruitJobs.Find(cdd.CPR_RecruitJobId);
                if (getRecruitJob != null)
                    cddDTO.RecruitJobDTO = new ConvertDataRecruitJob().ConvertToDTO(getRecruitJob);
                listDTO.Add(cddDTO);
            }
            return listDTO.ToPagedList(pageNumber, pageSize);
        }

        //get list recruit job by id recruit paging
        public IEnumerable<RecruitJobDTO> GetListRecruitJobsUserIdPaging(int IdRecruit, int pageNumber, int pageSize)
        {
            db = new JobHuntEntities();
            var getEF = db.RecruitJobs.OrderByDescending(x => x.RJPostDate).Where(x => x.RJ_RecruitId == IdRecruit && x.RJType != (int?)BU.Common.Enum.EnumStatusJob.Inactive).ToList();
            var listDTO = new List<RecruitJobDTO>();
            foreach (var cdd in getEF)
            {
                listDTO.Add(convertData.ConvertToDTO(cdd));
            }
            return listDTO.ToPagedList(pageNumber, pageSize);
        }

        //get list recruit job by id recruit without paging
        public List<RecruitJobDTO> GetListRecruitJobsUserIdNoPaging(int? IdRecruit, int? status)
        {
            db = new JobHuntEntities();
            var getEF = db.RecruitJobs.OrderByDescending(x => x.RJPostDate).Where(x => x.RJStatus != (int?)BU.Common.Enum.EnumStatusJob.Deleted).ToList();
            if (IdRecruit != null)
            {
                getEF = getEF.Where(x => x.RJ_RecruitId == IdRecruit).ToList();
            }
            if (status != null)
                getEF = getEF.Where(x => x.RJStatus == status).ToList();
            var listDTO = new List<RecruitJobDTO>();
            foreach (var cdd in getEF)
            {
                listDTO.Add(convertData.ConvertToDTO(cdd));
            }
            return listDTO;
        }


        public IEnumerable<RecruitJobDTO> GetListRecruitJobHaveSearchAndPaging(string keyWord, int? idCity, int? idProfesstion, int page, int pageSize)
        {
            db = new JobHuntEntities();

            List<RecruitJobDTO> listRecruitJobDTO = new List<RecruitJobDTO>();
            List<RecruitJob> SearchList = new List<RecruitJob>();


            if (SearchList.Count == 0)
            {
                SearchList = db.RecruitJobs.ToList();
            }

            if (!string.IsNullOrEmpty(keyWord))
            {
                SearchList = SearchList.Where(x => x.RJTitle.Contains(keyWord) ||
                x.Profession.PFName.Contains(keyWord)).ToList();
            }

            if (idProfesstion != null)
                SearchList = SearchList.Where(x => x.RJ_ProfessionId == idProfesstion).ToList();

            if (idCity != null)
                SearchList = SearchList.Where(x => x.RJCityId == idCity).ToList();

            SearchList = SearchList.Where(x => x.RJExpirationDate >= DateTime.Now && x.RJType != (int?)BU.Common.Enum.EnumStatusJob.Inactive).ToList();

            //find gần đúng profession ~ thuộc nghề
            //if(SearchList == null && idProfesstion != null)
            //{
            //    var findProfesstion = new ProfessionManage().GetDetailProfession((int)idProfesstion);
            //    SearchList = SearchList.Where(x => x.Profession.Career.CareerId == findProfesstion.CareerDTO.CareerId && x.RJExpirationDate >= DateTime.Now && x.RJType != (int?)BU.Common.Enum.EnumStatusJob.Inactive).ToList();
            //}


            foreach (var mb in SearchList)
            {
                listRecruitJobDTO.Add(convertData.ConvertToDTO(mb));
            }

            db = null;
            return listRecruitJobDTO.ToPagedList(page, pageSize);
        }

        public List<RecruitJobDTO> GetListRecentJobs(int WorkTypeId, int SalaryId, int? DistrictId)
        {
            var listDTO = new List<RecruitJobDTO>();
            db = new JobHuntEntities();
            var getListEF = db.RecruitJobs.Where(x => x.RJ_WorkTypeId == WorkTypeId && x.RJ_SalaryId == SalaryId && x.RJDistrictId == DistrictId && x.RJStatus != (int)Common.Enum.EnumStatusJob.Inactive && x.RJExpirationDate >= DateTime.Now).OrderByDescending(x => x.RJPostDate).ToList();
            foreach (var rj in getListEF)
            {
                listDTO.Add(convertData.ConvertToDTO(rj));
            }

            db = null;
            return listDTO;
        }


        //update count
        public bool UpdateCounter(int? idJob)
        {
            return new RecruitJobDAO().UpdateCounter(idJob);
        }

        //update job
        public int UpdateJob(RecruitJobDTO rcjob)
        {
            return new RecruitJobDAO().Update(convertData.ConvertToEF(rcjob));
        }


        //count job
        public int Statistical(int typeChoose)
        {
            db = new JobHuntEntities();
            var count = 0;
            //typeChoose = 1: count job posted
            //typeChoose = 2: count job approval
            //typeChoose = 3: count company
            //typeChoose = 5: count member

            switch (typeChoose)
            {
                case 1:
                    count = db.RecruitJobs.Where(x => x.RJStatus != (int?)Common.Enum.EnumStatusJob.Inactive).Count();
                    break;
                case 2:
                    count = db.RecruitJobs.Where(x => x.RJStatus == (int?)Common.Enum.EnumStatusJob.Active).Count();
                    break;
                case 3:
                    count = db.Recruits.Where(x => x.RIStatus == (int?)Common.Enum.StatusRecruit.Active).Count();
                    break;
                case 5:
                    count = db.RecruitJobs.Where(x => x.RJPostDate == DateTime.Today).Count();
                    break;
                default:
                    count = db.Candidates.Where(x => x.CPStatus == (int?)Common.Enum.StatusCandidate.Active).Count();
                    break;
            }
            db = null;
            return count;
        }
        // Update status
      /*  public int UpdateStatus(int typeChoose)
        {
            db = new JobHuntEntities();
            var check = 0;
            check = db.RecruitJobs.Where(x => x.RJStatus == (int?)Common.Enum.StatusRecruit.Active).check();
        }*/
        public IEnumerable<CandidateDTO> GetListCandidatesAppliedPaging(int page, int pageSize)
        {
            var listDTOs = new List<CandidateDTO>();
            return listDTOs.ToPagedList(page, pageSize);
        }

        //
        public List<RecruitJobDTO> GetListRecruitJobsBySearch(string keyWord, int? status)
        {
            db = new JobHuntEntities();
            List<RecruitJobDTO> listRecruitJobDTO = new List<RecruitJobDTO>();
            var getLists = db.RecruitJobs.Where(x => x.RJStatus != (int)Common.Enum.EnumStatusJob.Deleted).OrderByDescending(x => x.RJPostDate).ToList();
            foreach (var cdd in getLists)
            {
                listRecruitJobDTO.Add(convertData.ConvertToDTO(cdd));
            }
            if (!string.IsNullOrEmpty(keyWord))
                //getLists = getLists.Where(x => x.RJTitle.Contains(keyWord) || x.Recruit.RIUserName.Equals(keyWord)).ToList();
                listRecruitJobDTO = (from item in listRecruitJobDTO where (item.RecruitDTO != null && (item.RJTitle.Contains(keyWord) || item.RecruitDTO.RIUserName.Contains(keyWord) || item.RJExpirationDateString.Contains(keyWord) || item.NameType.Contains(keyWord) || item.TrangThai.Contains(keyWord))) || (item.RecruitDTO == null) select item).ToList();
            if (status != null)
                listRecruitJobDTO = listRecruitJobDTO.Where(x => x.RJStatus == status).ToList();
            return listRecruitJobDTO;
        }
    }
}
