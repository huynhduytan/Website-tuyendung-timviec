using JobHunt.Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobHunt.BU.DTO
{
    public class ConvertDataAspNetRole
    {
        public AspNetRoleDTO ConvertToDTO(AspNetRole EF)
        {
            var DTO = new AspNetRoleDTO()
            {
                Id = EF.Id,
                Name = EF.Name
            };

            return DTO;
        }

        public AspNetRole ConvertToEF(AspNetRoleDTO DTO)
        {
            var EF = new AspNetRole()
            {
                Id = DTO.Id,
                Name = DTO.Name
            };
            return EF;
        }
    }
}
