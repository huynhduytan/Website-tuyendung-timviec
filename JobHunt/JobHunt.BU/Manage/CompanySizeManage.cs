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
    public class CompanySizeManage
    {
        readonly ConvertDataCompanySize convertData = new ConvertDataCompanySize();
        //private JobHuntEntities db = null;
        private JobHuntRepository<CompanySize> repo = new JobHuntRepository<CompanySize>();

        //Tên kết nối khi mọi người tạo.
        //JobHuntEntities db = null;

        public List<CompanySizeDTO> GetAllCompanySizes()
        {
            var getEF = repo.SelectAll();
            var listDTO = new List<CompanySizeDTO>();
            foreach (var ct in getEF)
            {
                listDTO.Add(convertData.ConvertToDTO(ct));
            }
            return listDTO;
        }
    }
}
