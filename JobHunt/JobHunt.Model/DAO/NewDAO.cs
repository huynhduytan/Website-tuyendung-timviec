using JobHunt.Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace JobHunt.Model.DAO
{

    public class NewDAO
    {
        JobHuntEntities db = null;
        public NewDAO()
        {
            db = new JobHuntEntities();
        }

        //update
        public int Update(New n)
        {
            try
            {
                var getNew = db.News.Find(n.NewsId);

                getNew.NTitle = n.NTitle;
                getNew.NQuote = n.NQuote;
                getNew.NDetail = n.NDetail;
                getNew.NAvatar = n.NAvatar;
                getNew.N_CategoryId = n.N_CategoryId;
                getNew.NType = n.NType;
                getNew.Nstatus = n.Nstatus;

                db.SaveChanges();

                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        //Insert
        public New Insert(New n)
        {
            try
            {
                var news = db.News.Add(n);
                db.SaveChanges();
                return news;
            }
            catch (Exception)
            {
                return null;
            }
        }


        //Delete
        public int Delete(int idnew)
        {
            try
            {
                var getnew = db.News.Find(idnew);
                getnew.Nstatus = 4;
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
