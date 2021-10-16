using JobHunt.BU.DTO;
using JobHunt.Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobHunt.BU.ConvertData
{
    public class ConvertDataLevelInfo
    {
        public LevelInfoDTO ConvertToDTO(LevelInfo EF)
        {
            var DTO = new LevelInfoDTO()
            {
                LevelInfoId = EF.LevelInfoId,
                LIName = EF.LIName
            };
            return DTO;
        }

        public LevelInfo ConvertToEF(LevelInfoDTO DTO)
        {
            var EF = new LevelInfo()
            {
                LevelInfoId = DTO.LevelInfoId,
                LIName = DTO.LIName
            };
            return EF;
        }
    }
}
