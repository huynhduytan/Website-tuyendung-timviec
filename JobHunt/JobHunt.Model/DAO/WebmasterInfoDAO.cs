using JobHunt.Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobHunt.Model.DAO
{
    public class WebmasterInfoDAO
    {
        JobHuntEntities db = null;
        public WebmasterInfoDAO()
        {
            db = new JobHuntEntities();
        }

        public int Update(WebmasterInfo ef)
        {
            try
            {
                var get = db.WebmasterInfoes.Find(ef.WebmasterInfoId);

                get.WIUserName = ef.WIUserName;
                get.WIFullName = ef.WIFullName;
                get.WIAddress = ef.WIAddress;
                get.WIGender = ef.WIGender;
                get.WIBirthDay = ef.WIBirthDay;
                get.WIStatus = ef.WIStatus;
                get.WIPosition = ef.WIPosition;
                get.WIType = ef.WIType;

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
                var get = db.WebmasterInfoes.Find(id);

                get.WIStatus = 4;

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
