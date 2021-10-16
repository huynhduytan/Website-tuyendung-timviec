using JobHunt.BU.ConvertData;
using JobHunt.BU.DTO;
using JobHunt.Model.DAO;
using JobHunt.Model.EF;
using JobHunt.Repository;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobHunt.BU.Manage
{
    public class NewManage
    {
        readonly ConvertDataNew convertData = new ConvertDataNew();
        private JobHuntEntities db = null;
        private JobHuntRepository<New> repo = new JobHuntRepository<New>();
        NewDAO newdao = new NewDAO();

        public NewManage()
        {
            db = new JobHuntEntities();
        }

        //readonly NewDAO dao = new NewDAO();
        public IEnumerable<NewDTO> GetAllNews()
        {

            var getEF = repo.SelectAll();
            var listDTO = new List<NewDTO>();
            getEF = getEF.Where(x => x.Nstatus != (int)Common.Enum.StatusNew.Deleted).ToList();
            foreach (var cdd in getEF)
            {
                listDTO.Add(convertData.ConvertToDTO(cdd));
            }
            listDTO = listDTO.OrderByDescending(x => x.NPublicDate).ToList();
            return listDTO;
        }

        public IEnumerable<NewDTO> GetAllNewsPaging(int page, int pageSize)
        {

            var getEF = repo.SelectAll();
            var listDTO = new List<NewDTO>();
            getEF = getEF.Where(x => x.Nstatus != (int)Common.Enum.StatusNew.Deleted).ToList();
            foreach (var cdd in getEF)
            {
                listDTO.Add(convertData.ConvertToDTO(cdd));
            }
            listDTO = listDTO.OrderByDescending(x => x.NPublicDate).ToList();
            return listDTO.ToPagedList(page, pageSize);
        }

        //Insert
        public NewDTO Insert(NewDTO ndto)
        {
            try
            {
                return convertData.ConvertToDTO(new NewDAO().Insert(convertData.ConvertToEF(ndto)));

            }
            catch (Exception)
            {
                return null;
            }
        }

        //Insert
        public int Update(NewDTO ndto)
        {
            var result = 0;
            try
            {
                result = new NewDAO().Update(convertData.ConvertToEF(ndto));
                return result;

            }
            catch (Exception)
            {
                return result;
            }
        }

        //get list categories
        public List<CategoryDTO> GetListCategories()
        {
            var listDTO = new List<CategoryDTO>();
            var getListEF = db.Categories.ToList();
            foreach (var c in getListEF)
            {
                listDTO.Add(new ConvertDataCategory().ConvertToDTO(c));
            }
            return listDTO;
        }

        public NewDTO GetBlogById(int id)
        {
            var getNew = db.News.Find(id);
            if (getNew == null)
                return null;
            var dto = convertData.ConvertToDTO(getNew);
            return dto;
        }

        //
        public int Delete(int idnew)
        {
            try
            {
                return newdao.Delete(idnew);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        //
        public List<NewDTO> GetListNewsBySearch(string keyWord, int? status)
        {
            db = new JobHuntEntities();

            List<NewDTO> listNewDTO = new List<NewDTO>();
            var getLists = db.News.Where(x => x.Nstatus != (int)Common.Enum.StatusNew.Deleted).OrderByDescending(x => x.NPostDate).ToList();
            foreach (var cdd in getLists)
            {
                listNewDTO.Add(convertData.ConvertToDTO(cdd));
            }
            if (!string.IsNullOrEmpty(keyWord))
                listNewDTO = listNewDTO.Where(x => x.NTitle.Contains(keyWord) || x.NQuote.Contains(keyWord) || x.NameStatus.Contains(keyWord) || x.NameType.Contains(keyWord)).ToList();
            if (status != null)
                listNewDTO = listNewDTO.Where(x => x.Nstatus == status).ToList();
            return listNewDTO;
        }
    }
}
