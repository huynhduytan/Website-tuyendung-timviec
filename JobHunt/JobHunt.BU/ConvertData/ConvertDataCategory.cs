using JobHunt.BU.DTO;
using JobHunt.Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobHunt.BU.ConvertData
{
    public class ConvertDataCategory
    {
        public CategoryDTO ConvertToDTO(Category EF)
        {
            var DTO = new CategoryDTO()
            {
                CategoryId = EF.CategoryId,
                CName = EF.CName
            };
            return DTO;
        }

        public Category ConvertToEF(CategoryDTO DTO)
        {
            var EF = new Category()
            {
                CategoryId = DTO.CategoryId,
                CName = DTO.CName
            };
            return EF;
        }
    }
}
