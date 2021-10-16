using JobHunt.BU.DTO;
using JobHunt.Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobHunt.BU.ConvertData
{
    public class ConvertDataSignUpNewsletter
    {
        public SignUpNewsletterDTO ConvertToDTO(SignUpNewsletter EF)
        {
            var DTO = new SignUpNewsletterDTO()
            {
                CheckNew = EF.CheckNew,
                CheckPost = EF.CheckPost,
                Email = EF.Email,
                IdProfession = EF.IdProfession,
                Name = EF.Name,
                RegisterID = EF.RegisterID
            };
            return DTO;
        }

        public SignUpNewsletter ConvertToEF(SignUpNewsletterDTO DTO)
        {
            var EF = new SignUpNewsletter()
            {
                CheckNew = DTO.CheckNew,
                CheckPost = DTO.CheckPost,
                Email = DTO.Email,
                IdProfession = DTO.IdProfession,
                Name = DTO.Name,
                RegisterID = DTO.RegisterID
            };
            return EF;
        }
    }
}
