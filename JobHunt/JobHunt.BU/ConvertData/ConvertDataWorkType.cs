using JobHunt.BU.DTO;
using JobHunt.Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobHunt.BU.ConvertData
{
    public class ConvertDataWorkType
    {
        public WorkTypeDTO ConvertToDTO(WorkType EF)
        {
            var DTO = new WorkTypeDTO()
            {
                WorkTypeId = EF.WorkTypeId,
                WTName = EF.WTName
            };
            return DTO;
        }

        public WorkType ConvertToEF(WorkTypeDTO DTO)
        {
            var EF = new WorkType()
            {
                WorkTypeId = DTO.WorkTypeId,
                WTName = DTO.WTName
            };
            return EF;
        }
    }
}
