using JobHunt.Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobHunt.Model.DAO
{
    public class RecruitDAO
    {
        JobHuntEntities db = null;
        public RecruitDAO()
        {
            db = new JobHuntEntities();
        }
        public bool UpdateStatus(int? status, int idRecruit)
        {
            try
            {
                var getRecruit = db.Recruits.Find(idRecruit);

                getRecruit.RIStatus = status;

                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        public int UpdateProfile(Recruit rc, int type)
        {
            try
            {
                var getRC = db.Recruits.Find(rc.RecruitId);

                //type = 1: update information, type = 2: update contact
                if (type == 1)
                {
                    getRC.RICoverImage = rc.RICoverImage;
                    getRC.RIAvatar = rc.RIAvatar;
                    getRC.RICompanyName = rc.RICompanyName;
                    getRC.RILogo = rc.RILogo;
                    getRC.FoundedYear = rc.FoundedYear;
                    getRC.RI_CompanySizeId = rc.RI_CompanySizeId;
                    getRC.RI_ProfessionId = rc.RI_ProfessionId;
                    getRC.RIAbout = rc.RIAbout;
                }
                else
                {
                    getRC.RIPhone = rc.RIPhone;
                    getRC.RIEmail = rc.RIEmail;
                    getRC.RIWebsite = rc.RIWebsite;
                    getRC.RI_CityId = rc.RI_CityId;
                    getRC.RI_DistrictId = rc.RI_DistrictId;
                    getRC.RI_WardId = rc.RI_WardId;
                    getRC.RIAddress = rc.RIAddress;
                }


                db.SaveChanges();
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int Update(Recruit rc)
        {
            try
            {
                var getRC = db.Recruits.Find(rc.RecruitId);

                getRC.RICoverImage = rc.RICoverImage;
                getRC.RIAvatar = rc.RIAvatar;
                getRC.RICompanyName = rc.RICompanyName;
                getRC.RILogo = rc.RILogo;
                getRC.FoundedYear = rc.FoundedYear;
                getRC.RI_CompanySizeId = rc.RI_CompanySizeId;
                getRC.RI_ProfessionId = rc.RI_ProfessionId;
                getRC.RIAbout = rc.RIAbout;
                getRC.RIPhone = rc.RIPhone;
                getRC.RIEmail = rc.RIEmail;
                getRC.RIWebsite = rc.RIWebsite;
                getRC.RI_CityId = rc.RI_CityId;
                getRC.RI_DistrictId = rc.RI_DistrictId;
                getRC.RI_WardId = rc.RI_WardId;
                getRC.RIAddress = rc.RIAddress;


                db.SaveChanges();
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int Delete(int id)
        {
            try
            {
                var getCdd = db.Recruits.Find(id);
                getCdd.RIStatus = 4;
                db.SaveChanges();
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public bool Insert(Recruit rc)
        {
            try
            {
                db.Recruits.Add(rc);
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
