using JobHunt.BU.DTO;
using JobHunt.Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobHunt.BU.ConvertData
{
    public class ConvertDataSalary
    {
        public SalaryDTO ConvertToDTO(Salary EF)
        {
            var DTO = new SalaryDTO()
            {
                SalaryId = EF.SalaryId,
                SMax = EF.SMax,
                SMin = EF.SMin,
                SShow = EF.SShow
            };
            return DTO;
        }

        public Salary ConvertToEF(SalaryDTO DTO)
        {
            var EF = new Salary()
            {
                SalaryId = DTO.SalaryId,
                SMax = DTO.SMax,
                SMin = DTO.SMin,
                SShow = DTO.SShow
            };
            return EF;
        }
    }
}
