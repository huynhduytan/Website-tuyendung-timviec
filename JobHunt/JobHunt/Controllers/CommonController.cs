using JobHunt.BU.Manage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JobHunt.Controllers
{
    public class CommonController : Controller
    {
        CandidateManage candidateManage = new CandidateManage();
        // GET: Common
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult DownloadFile(string path, string name)
        {
            string fullpath = Path.Combine(Server.MapPath(path));
            string fileName = name;
            return File(fullpath, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }

        // Upload Image
        [HttpPost]
        public string UploadFileImage(HttpPostedFileBase file)
        {
            var pic = System.Web.HttpContext.Current.Request.Files["file"];
            // Validate
            // Xử lý upload lưu vào sv
            file.SaveAs(Server.MapPath("~/Assets/client/upload/images/" + file.FileName));
            // Trả về link ảnh
            return "/Assets/client/upload/images/" + file.FileName;
        }


        // Lấy danh sách tỉnh
        public JsonResult GetListCities()
        {
            var ListCities = new CityManage().GetAllCities();
            return Json(ListCities, JsonRequestBehavior.AllowGet);
        }

        //Lấy ra danh sách huyện theo idTinh
        public JsonResult GetListDistrictsByIdCity(int idCity)
        {
            var GetListDistricts = new DistrictManage().GetListDistrictsByIdCity(idCity);
            return Json(GetListDistricts, JsonRequestBehavior.AllowGet);
        }

        //Lấy ra danh sách xã theo idHuyen
        public JsonResult GetListWardsByIdDistrict(int idDistrict)
        {
            var GetListXa = new WardManage().GetListWardsByIdDistrict(idDistrict);
            return Json(GetListXa, JsonRequestBehavior.AllowGet);
        }


    }
}