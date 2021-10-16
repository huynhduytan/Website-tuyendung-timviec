using JobHunt.BU.ConvertData;
using JobHunt.BU.DTO;
using JobHunt.Model.DAO;
using JobHunt.Model.EF;
using JobHunt.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobHunt.BU.Manage
{
    public class SignUpNewsletterManage : JobHuntRepository<SignUpNewsletter>
    {
        readonly ConvertDataSignUpNewsletter convertData = new ConvertDataSignUpNewsletter();
        private JobHuntEntities db = null;
        private JobHuntRepository<SignUpNewsletter> repo = new JobHuntRepository<SignUpNewsletter>();

        readonly SignUpNewsletterDAO dao = new SignUpNewsletterDAO();
        public bool Insert(SignUpNewsletterDTO DTO)
        {
            try
            {
                return dao.Insert(convertData.ConvertToEF(DTO));
            }
            catch (Exception)
            {
                return false;
            }
        }


        public bool Delete(int idSignUpNewsletterInfo)
        {
            try
            {
                repo.Delete(idSignUpNewsletterInfo);
                repo.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<SignUpNewsletterDTO> GetListSignUpByType(int Type, int? idPro)
        {
            var listDTOs = new List<SignUpNewsletterDTO>();
            using (db = new JobHuntEntities())
            {
                var listEFs = new List<SignUpNewsletter>();
                //Type = 1: list jobs
                if(Type == 1)
                {
                    listEFs = db.SignUpNewsletters.Where(x => x.CheckPost == true && (x.IdProfession == idPro || x.IdProfession == null)).ToList();
                }
                //Type != 1: list news
                else
                {
                    listEFs = db.SignUpNewsletters.Where(x => x.CheckNew == true).ToList();
                }
                foreach (var job in listEFs)
                {
                    listDTOs.Add(convertData.ConvertToDTO(job));
                }
            }
            return listDTOs;
        }
    }
}
