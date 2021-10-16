using JobHunt.BU.ConvertData;
using JobHunt.BU.DTO;
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
    public class CandidatePostResumeManage
    {
        ConvertDataCandidatePostResume convertData = new ConvertDataCandidatePostResume();
        //private JobHuntEntities db = null;
        private JobHuntRepository<CandidatePostResume> repo = new JobHuntRepository<CandidatePostResume>();

        //Tên kết nối khi mọi người tạo.
        JobHuntEntities db = null;

        public bool Insert(CandidatePostResumeDTO DTO)
        {
            try
            {
                repo.Insert(convertData.ConvertToEF(DTO));
                repo.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public int Delete(int id)
        {
            try
            {
                repo.Delete(id);
                repo.SaveChanges();
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }


        public CandidatePostResumeDTO GetDetail(int id)
        {
            var dto = new CandidatePostResumeDTO();
            using (db = new JobHuntEntities())
            {
                dto = convertData.ConvertToDTO(db.CandidatePostResumes.Find(id));
            }
            return dto;
        }


        //check posted resume
        public bool CheckPostedResume(int? idCandidate, int? idJob)
        {
            db = new JobHuntEntities();
            var getListPostedResume = db.CandidatePostResumes.ToList();
            var check = false;
            foreach (var cddprs in getListPostedResume)
            {
                if(cddprs.CPR_CandidateId == idCandidate && cddprs.CPR_RecruitJobId == idJob)
                {
                    check = true;
                    break;
                }
            }

            db = null;
            return check;
        }

        //Get list post resume by id candidate
        public IEnumerable<CandidatePostResumeDTO> GetListCandidatePostResumeHaveSearchAndPaging(int? idCandidate, int page, int pageSize)
        {
            db = new JobHuntEntities();

            List<CandidatePostResumeDTO> listCandidatePostResumeDTO = new List<CandidatePostResumeDTO>();
            List<CandidatePostResume> GetLists = new List<CandidatePostResume>();

            GetLists = db.CandidatePostResumes.Where(x => x.CPR_CandidateId == idCandidate && x.CPRStatus == true).ToList();

            foreach (var mb in GetLists)
            {
                listCandidatePostResumeDTO.Add(convertData.ConvertToDTO(mb));
            }

            db = null;

            return listCandidatePostResumeDTO.ToPagedList(page, pageSize);
        }
    }
}
