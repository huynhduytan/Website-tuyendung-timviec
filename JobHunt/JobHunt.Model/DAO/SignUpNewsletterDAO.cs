using JobHunt.Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobHunt.Model.DAO
{
    public class SignUpNewsletterDAO
    {
        JobHuntEntities db = null;
        public SignUpNewsletterDAO()
        {
            db = new JobHuntEntities();
        }

        //Insert
        public bool Insert(SignUpNewsletter ef)
        {
            try
            {
                db.SignUpNewsletters.Add(ef);
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
