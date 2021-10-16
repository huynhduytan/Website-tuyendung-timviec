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
    public class CareerManage
    {
        readonly ConvertDataCareer convertData = new ConvertDataCareer();
        private JobHuntEntities db = null;
        private JobHuntRepository<Career> repo = new JobHuntRepository<Career>();

        //Tên kết nối khi mọi người tạo.
        //JobHuntEntities db = null;

        public List<CareerDTO> GetAllCareers()
        {
            db = new JobHuntEntities();
            var getEF = repo.SelectAll();
            var listDTO = new List<CareerDTO>();
            foreach (var ct in getEF)
            {
                var crdto = convertData.ConvertToDTO(ct);
                crdto.CountJob = db.RecruitJobs.Where(x => x.Profession.PFCareerId == ct.CareerId && x.RJExpirationDate >= DateTime.Now && x.RJStatus != (int?)Common.Enum.EnumStatusJob.Inactive).Count();
                listDTO.Add(crdto);
            }
            db = null;
            return listDTO;
        }
    }
}
