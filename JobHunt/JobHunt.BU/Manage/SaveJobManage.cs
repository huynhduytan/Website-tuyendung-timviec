using JobHunt.BU.ConvertData;
using JobHunt.BU.DTO;
using JobHunt.Model.EF;
using JobHunt.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobHunt.BU.Manage
{
    public class SaveJobManage
    {
        ConvertDataSaveJob convertData = new ConvertDataSaveJob();
        //private JobHuntEntities db = null;
        JobHuntRepository<SaveJob> repo = new JobHuntRepository<SaveJob>();
        public bool Insert(SaveJobDTO DTO)
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


        public bool Delete(SaveJobDTO DTO)
        {
            try
            {
                repo.Delete(DTO.SaveJobId);
                repo.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
