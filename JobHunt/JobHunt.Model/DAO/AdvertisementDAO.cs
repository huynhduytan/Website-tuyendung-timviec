using JobHunt.Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobHunt.Model.DAO
{
    public class AdvertisementDAO
    {
        JobHuntEntities db = null;
        public AdvertisementDAO()
        {
            db = new JobHuntEntities();
        }

        //update
        public int Update(Advertisement n)
        {
            try
            {
                var getAdvertisement = db.Advertisements.Find(n.AdvertisementId);

                getAdvertisement.AdStartDate = n.AdStartDate;
                getAdvertisement.AdEndDate = n.AdEndDate;
                getAdvertisement.AdPosition = n.AdPosition;
                getAdvertisement.AdStatus = n.AdStatus;
                getAdvertisement.AdName = n.AdName;
                getAdvertisement.AdPhone = n.AdPhone;
                getAdvertisement.AdEmail = n.AdEmail;
                getAdvertisement.AdAddress = n.AdAddress;
                getAdvertisement.AdLink = n.AdLink;
                getAdvertisement.AdImage = n.AdImage;

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
                var getAdvertisement = db.Advertisements.Find(id);

                getAdvertisement.AdStatus = 4;

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
