using ClosedXML.Excel;
using JobHunt.BU.DTO;
using JobHunt.BU.Manage;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace JobHunt.Areas.Admin.Controllers
{
    [Authorize(Roles = "Quản trị, Quản lý người tuyển dụng, Quản lý người tìm việc")]
    public class RecruitManagementController : Controller
    {
        // GET: Admin/RecruitManagement
        ProfessionManage ProfessionManage = new ProfessionManage();
        CityManage cityManage = new CityManage();
        RecruitJobManage recruitjobManage = new RecruitJobManage();
        RecruitManage recruitManage = new RecruitManage();
        CandidateManage candidateManage = new CandidateManage();
        public ActionResult Index()
        {
            ViewBag.ListProfessions = new SelectList(ProfessionManage.GetAllProfessions(), "ProfessionId", "PFName");

            //Lấy danh sách tỉnh
            ViewBag.ListCities = new SelectList(cityManage.GetAllCities(), "CityId", "CName");

            var model = recruitManage.GetListRecruits();
            return View(model);
        }
        [HttpPost]
        public ActionResult Index(DateTime startdate , DateTime enddate)
        {

            return View(recruitManage.GetListRecruits(startdate,enddate));
        }
        public ActionResult CreateRecruit()
        {
            //Lấy ra danh sách tất cả danh mục công việc
            ViewBag.ListProfessions = new SelectList(ProfessionManage.GetAllProfessions(), "ProfessionId", "PFName");

            //Lấy danh sách tỉnh
            ViewBag.ListCities = new SelectList(cityManage.GetAllCities(), "CityId", "CName");

            //Lấy danh sách trình độ
            ViewBag.ListLevels = new SelectList(new LevelManage().GetAllLevels(), "LevelInfoId", "LIName");

            //Lấy danh sách kinh nghiệm
            ViewBag.ListExperiences = new SelectList(new ExperienceManage().GetAllExperiences(), "ExperienceId", "EShow");

            //Lấy danh sách kinh Lương
            ViewBag.ListSalaries = new SelectList(new SalaryMange().GetAllSalaries(), "SalaryId", "SShow");

            //Lấy danh sách hình thức làm việc
            ViewBag.ListWorkTypes = new SelectList(new WorkTypeManage().GetAllWorkTypes(), "WorkTypeId", "WTName");

            //Lấy ra danh sách size của công ty
            ViewBag.ListCompanySizes = new SelectList(new CompanySizeManage().GetAllCompanySizes(), "CompanySizeId", "CSShow");

           
            return View();
        }
        public ActionResult DetailRecruit(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var model = recruitManage.GetRecruitInfoById((int) id);
            if(model == null)
            {
                return HttpNotFound();
            }
            //Lấy ra danh sách tất cả danh mục công việc
            ViewBag.ListProfessions = new SelectList(ProfessionManage.GetAllProfessions(), "ProfessionId", "PFName");

            //Lấy danh sách tỉnh
            ViewBag.ListCities = new SelectList(cityManage.GetAllCities(), "CityId", "CName");

            //Lấy danh sách trình độ
            ViewBag.ListLevels = new SelectList(new LevelManage().GetAllLevels(), "LevelInfoId", "LIName");

            //Lấy danh sách kinh nghiệm
            ViewBag.ListExperiences = new SelectList(new ExperienceManage().GetAllExperiences(), "ExperienceId", "EShow");

            //Lấy danh sách kinh Lương
            ViewBag.ListSalaries = new SelectList(new SalaryMange().GetAllSalaries(), "SalaryId", "SShow");

            //Lấy danh sách hình thức làm việc
            ViewBag.ListWorkTypes = new SelectList(new WorkTypeManage().GetAllWorkTypes(), "WorkTypeId", "WTName");

            //Lấy ra danh sách size của công ty
            ViewBag.ListCompanySizes = new SelectList(new CompanySizeManage().GetAllCompanySizes(), "CompanySizeId", "CSShow");

            return View(model);
        }
        public JsonResult UpdateRecruit(RecruitDTO rcdto, int? type)
        {
            var resultCode = 0;
            var resultMessage = "";
            var resultStatus = "eror";
            if (rcdto.RecruitId == 0)
            {
                resultMessage = "Mã NTD không được để trống";
            }
            else
            {
                if (type == 1)
                {
                    resultCode = recruitManage.Insert(rcdto);
                    if (resultCode == 1)
                    {
                        resultMessage = "Thêm thành công";
                        resultStatus = "success";
                    }
                    else
                    {
                        resultMessage = "Thêm không thành công";
                    }
                }
                else
                {
                    resultCode = recruitManage.UpdateRecruit(rcdto);
                    if (resultCode == 1)
                    {
                        resultMessage = "Cập nhật thành công";
                        resultStatus = "success";
                    }
                    else
                    {
                        resultMessage = "Sửa không thành công";
                    }
                }
            }
            return Json(new { message = resultMessage, status = resultStatus, code = resultCode }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteRecruit(int? idRecruit)
        {
            var resultCode = 0;
            var resultMessage = "";
            var resultStatus = "error";
            if (idRecruit == null || idRecruit == 0)
            {
                resultMessage = "Mã NTD không được để trống";
            }
            else
            {
                resultCode = recruitManage.Delete((int)idRecruit);
                if (resultCode == 1)
                {
                    resultMessage = "Xóa thành công";
                    resultStatus = "success";
                }
                else
                {
                    resultMessage = "Đã xảy ra lỗi khi xóa";
                }
            }
            return Json(new { message = resultMessage, status = resultStatus, code = resultCode }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public FileResult ExportToExcel(string keyWord, int? Status)
        {
            //Sheet Tổng Quan
            DataTable dtIndex = new DataTable("DanhSachNTD");

            dtIndex.Columns.AddRange(new DataColumn[7] {new DataColumn("STT"),
                                                    new DataColumn("Tên đăng nhập"),
                                                    new DataColumn("Tên đầy đủ"),
                                                    new DataColumn("Ngày đăng ký"),
                                                    new DataColumn("Vị trí làm việc"),
                                                    new DataColumn("Tỉnh/Thành phố"),
                                                    new DataColumn("Trạng thái")
                                                   });

            var model = recruitManage.GetListRecruitsBySearch(keyWord, Status);

            var stt = 0;
            foreach (var rc in model)
            {
                stt++;
                dtIndex.Rows.Add(stt, rc.AspNetUserDTO.UserName, rc.RIFullName, rc.RIRegisterDate.Value.ToShortDateString(), rc.ProfessionDTO.PFName, rc.CityDTO.CName, rc.StatusString);
            }


            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dtIndex);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "DanhSachNTD.xlsx");
                }
            }
        }
    }
}