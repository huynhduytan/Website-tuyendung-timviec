using JobHunt.BU.DTO;
using JobHunt.Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobHunt.BU.ConvertData
{
    public class ConvertDataAspNetUser
    {
        public AspNetUserDTO ConvertToDTO(AspNetUser EF)
        {
            var DTO = new AspNetUserDTO()
            {
                AccessFailedCount = EF.AccessFailedCount,
                Email = EF.Email,
                EmailConfirmed = EF.EmailConfirmed,
                Id = EF.Id,
                LockoutEnabled = EF.LockoutEnabled,
                LockoutEndDateUtc = EF.LockoutEndDateUtc,
                PasswordHash = EF.PasswordHash,
                PhoneNumber = EF.PhoneNumber,
                PhoneNumberConfirmed = EF.PhoneNumberConfirmed,
                SecurityStamp = EF.SecurityStamp,
                TwoFactorEnabled = EF.TwoFactorEnabled,
                UserName = EF.UserName
            };

            return DTO;
        }

        public AspNetUser ConvertToEF(AspNetUserDTO DTO)
        {
            var EF = new AspNetUser()
            {
                AccessFailedCount = DTO.AccessFailedCount,
                Email = DTO.Email,
                EmailConfirmed = DTO.EmailConfirmed,
                Id = DTO.Id,
                LockoutEnabled = DTO.LockoutEnabled,
                LockoutEndDateUtc = DTO.LockoutEndDateUtc,
                PasswordHash = DTO.PasswordHash,
                PhoneNumber = DTO.PhoneNumber,
                PhoneNumberConfirmed = DTO.PhoneNumberConfirmed,
                SecurityStamp = DTO.SecurityStamp,
                TwoFactorEnabled = DTO.TwoFactorEnabled,
                UserName = DTO.UserName
            };
            return EF;
        }
    }
}
