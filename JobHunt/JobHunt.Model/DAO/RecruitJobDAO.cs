using JobHunt.Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobHunt.Model.DAO
{
    public class RecruitJobDAO
    {
        JobHuntEntities db = null;
        public RecruitJobDAO()
        {
            db = new JobHuntEntities();
        }

        public bool Delete(int idJob)
        {
            try
            {
                var getJob = db.RecruitJobs.Find(idJob);
                getJob.RJStatus = 4;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateCounter(int? idJob)
        {
            try
            {
                var getJob = db.RecruitJobs.Find(idJob);
                if (getJob.RJCount == null)
                    getJob.RJCount = 1;
                getJob.RJCount += 1;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public int Update(RecruitJob rcjob)
        {
            try
            {
                var get = db.RecruitJobs.Find(rcjob.RecruitJobId);

                get.RJTitle = rcjob.RJTitle;
                get.RJ_ProfessionId = rcjob.RJ_ProfessionId;
                get.RJPosition = rcjob.RJPosition;
                get.RJAmount = rcjob.RJAmount;
                get.RJ_SalaryId = rcjob.RJ_SalaryId;
                get.RJ_LevelId = rcjob.RJ_LevelId;
                get.RJ_ExperienceId = rcjob.RJ_ExperienceId;
                get.RJ_WorkTypeId = rcjob.RJ_WorkTypeId;
                get.RJGender = rcjob.RJGender;
                get.RJExpirationDate = rcjob.RJExpirationDate;
                get.RJ_Describe = rcjob.RJ_Describe;
                get.RJ_Require = rcjob.RJ_Require;
                get.RJBenefit = rcjob.RJBenefit;
                get.RJEmailContact = rcjob.RJEmailContact;
                get.RJNameContact = rcjob.RJNameContact;
                get.RJPhoneContact = rcjob.RJPhoneContact;
                get.RJCityId = rcjob.RJCityId;
                get.RJDistrictId = rcjob.RJDistrictId;
                get.RJ_WardId = rcjob.RJ_WardId;
                get.RJ_WorkPlace = rcjob.RJ_WorkPlace;
                get.RJStatus = rcjob.RJStatus;
                get.RJType = rcjob.RJType;

                db.SaveChanges();
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        
    }
}
