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
    public class SalaryMange
    {
        readonly ConvertDataSalary convertData = new ConvertDataSalary();
        //private JobHuntEntities db = null;
        private JobHuntRepository<Salary> repo = new JobHuntRepository<Salary>();

        public IEnumerable<SalaryDTO> GetAllSalaries()
        {
            var getEF = repo.SelectAll();
            var listDTO = new List<SalaryDTO>();
            foreach (var ef in getEF)
            {
                listDTO.Add(convertData.ConvertToDTO(ef));
            }
            return listDTO;
        }
    }
}
