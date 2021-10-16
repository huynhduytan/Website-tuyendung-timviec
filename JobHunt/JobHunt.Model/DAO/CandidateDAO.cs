using JobHunt.Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobHunt.Model.DAO
{
    public class CandidateDAO
    {
        JobHuntEntities db = null;
        public CandidateDAO()
        {
            db = new JobHuntEntities();
        }

        public int UpdateCandidate(Candidate cdd)
        {
            try
            {

                var getCandidate = db.Candidates.Find(cdd.CandidateId);

                getCandidate.CddFullName = cdd.CddFullName;
                getCandidate.CPAvatar = cdd.CPAvatar;
                getCandidate.CddAge = cdd.CddAge;
                getCandidate.CddSex = cdd.CddSex;
                getCandidate.CPExperience = cdd.CPExperience;
                getCandidate.CP_ProfessionId = cdd.CP_ProfessionId;
                getCandidate.CP_SalaryId = cdd.CP_SalaryId;
                getCandidate.CP_ExperienceId = cdd.CP_ExperienceId;
                getCandidate.CP_LevelId = cdd.CP_LevelId;
                getCandidate.CP_WorkTypeId = cdd.CP_WorkTypeId;
                getCandidate.CddAbout = cdd.CddAbout;
                getCandidate.CddPhone = cdd.CddPhone;
                getCandidate.CddEmail = cdd.CddEmail;
                getCandidate.CddFacebook = cdd.CddFacebook;
                getCandidate.CddGoogle = cdd.CddGoogle;
                getCandidate.Cdd_CityId = cdd.Cdd_CityId;
                getCandidate.Cdd_DistrictId = cdd.Cdd_DistrictId;
                getCandidate.Cdd_WardId = cdd.Cdd_WardId;
                getCandidate.CPStatus = cdd.CPStatus;
                getCandidate.CddPathCV = cdd.CddPathCV;
                getCandidate.CddDescribeCV = cdd.CddDescribeCV;

                db.SaveChanges();

                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public bool UpdateProfileCandidate(Candidate cdd)
        {
            try
            {

                var getCandidate = db.Candidates.Find(cdd.CandidateId);

                getCandidate.CddFullName = cdd.CddFullName;
                getCandidate.CddAge = cdd.CddAge;
                getCandidate.CddSex = cdd.CddSex;
                getCandidate.CPExperience = cdd.CPExperience;
                getCandidate.CP_ProfessionId = cdd.CP_ProfessionId;
                getCandidate.CP_SalaryId = cdd.CP_SalaryId;
                getCandidate.CP_ExperienceId = cdd.CP_ExperienceId;
                getCandidate.CP_LevelId = cdd.CP_LevelId;
                getCandidate.CP_WorkTypeId = cdd.CP_WorkTypeId;
                getCandidate.CddAbout = cdd.CddAbout;
                getCandidate.CPAvatar = cdd.CPAvatar;

                db.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateSocialAndContact(Candidate cdd)
        {
            try
            {

                var getCandidate = db.Candidates.Find(cdd.CandidateId);

                getCandidate.CddFacebook = cdd.CddFacebook;
                getCandidate.CddGoogle = cdd.CddGoogle;
                getCandidate.CddPhone = cdd.CddPhone;
                getCandidate.CddEmail = cdd.CddEmail;
                getCandidate.Cdd_CityId = cdd.Cdd_CityId;
                getCandidate.Cdd_DistrictId = cdd.Cdd_DistrictId;
                getCandidate.Cdd_WardId = cdd.Cdd_WardId;
                getCandidate.CddAddress = cdd.CddAddress;

                db.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        public bool UploadCV(Candidate cdd)
        {
            try
            {
                var getCandidate = db.Candidates.FirstOrDefault(x => x.Cdd_AspNetUserId.Equals(cdd.Cdd_AspNetUserId));

                getCandidate.CddPathCV = cdd.CddPathCV;
                getCandidate.CddDescribeCV = cdd.CddDescribeCV;

                db.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateStatus(int? status, int idCandidate)
        {
            try
            {
                var getCandidate = db.Candidates.Find(idCandidate);

                getCandidate.CPStatus = status;

                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public int DeleteCandidate(int idCandidate)
        {
            try
            {
                var getCdd = db.Candidates.Find(idCandidate);
                getCdd.CPStatus = 4;
                db.SaveChanges();
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public int ActiveCandidate(int idCandidate)
        {
            try
            {
                var getCdd = db.Candidates.Find(idCandidate);
                getCdd.CPStatus = 1;
                db.SaveChanges();
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public bool Insert(Candidate cdd)
        {
            try
            {
                db.Candidates.Add(cdd);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
