using ClosedXML.Excel;
using JobHunt.BU.ConvertData;
using JobHunt.BU.DTO;
using JobHunt.BU.Manage;
using Microsoft.AspNet.Identity;
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
    public class CandidateManagementController : Controller
    {
        // GET: Admin/CandidateManagement
        CandidateManage cddmanage = new CandidateManage();
        ProfessionManage ProfessionManage = new ProfessionManage();
        CityManage cityManage = new CityManage();
        ConvertDataCandidate convertData = new ConvertDataCandidate();
        RecruitJobManage recruitjobManage = new RecruitJobManage();
        CandidatePostResumeManage cddPostResumeManage = new CandidatePostResumeManage();
        public ActionResult Index(int? status = null)
        {
            //Lấy ra danh sách tất cả danh mục công việc
            ViewBag.ListProfesstions = new SelectList(ProfessionManage.GetAllProfessions(), "ProfessionId", "PFName");

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


            var model = cddmanage.GetListCandidates(status);
            return View(model);
        }
        [HttpPost]
        public ActionResult Index(DateTime? startdate, DateTime? enddate)
        {
            return View(cddmanage.GetListCandidatesByDate(startdate,enddate));
        }

        //Get infor candidate by id candidate
        public JsonResult GetInforCandidateById(int? idCandidate)
        {
            var resultCode = 0;
            var result = new CandidateDTO();
            if (idCandidate == null)
                resultCode = 2;
            else
            {
                resultCode = 1;
                result = cddmanage.GetCandidateInfoById((int)idCandidate);
            }
            return Json(new { resultCode = resultCode, resultObject = result }, JsonRequestBehavior.AllowGet);
        }

        //
        public JsonResult UpdateProfileCandidate(CandidateDTO cdddto, HttpPostedFileBase fileCV)
        {
            var resultCode = 0;
            var resultMessage = "";
            var resultStatus = "error";
            if (cdddto.CandidateId == 0)
            {
                resultMessage = "Mã NTV không được để trống";
            }
            else
            {
                var InfoCandidate = new CandidateManage().GetCandidateInfoById(cdddto.CandidateId);
                if (fileCV != null)
                    cdddto.CddPathCV = UploadFile(fileCV);
                else
                    cdddto.CddPathCV = InfoCandidate.CddPathCV;

                resultCode = cddmanage.UpdateCandidate(cdddto);

                if (resultCode == 1)
                {
                    resultMessage = "Cập nhật thành công";
                    resultStatus = "success";
                }
            }
            return Json(new { message = resultMessage, status = resultStatus, code = resultCode, cdddto = cddmanage.GetCandidateInfoById((int)cdddto.CandidateId) }, JsonRequestBehavior.AllowGet);
        }

        //
        public JsonResult DeleteCandidate(int? idCandidate)
        {
            var resultCode = 0;
            var resultMessage = "";
            var resultStatus = "error";
            if (idCandidate == null || idCandidate == 0)
            {
                resultMessage = "Mã NTV không được để trống";
            }
            else
            {
                resultCode = cddmanage.DeleteCandidate((int)idCandidate);
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

        public JsonResult ActiveCandidate(int? idCandidate)
        {
            var resultCode = 0;
            var resultMessage = "";
            var resultStatus = "error";
            if (idCandidate == null || idCandidate == 0)
            {
                resultMessage = "Mã NTV không được để trống";
            }
            else
            {
                resultCode = cddmanage.ActiveCdd((int)idCandidate);
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
        //create candidate
        public ActionResult CreateCandidate()
        {
            //Lấy ra danh sách tất cả danh mục công việc
            ViewBag.ListProfesstions = new SelectList(ProfessionManage.GetAllProfessions(), "ProfessionId", "PFName");

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

            return View();
        }

        public ActionResult DetailCandidate(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var model = cddmanage.GetCandidateInfoById((int)id);
            if (model == null)
            {
                return HttpNotFound();
            }

            //Lấy ra danh sách tất cả danh mục công việc
            ViewBag.ListProfesstions = new SelectList(ProfessionManage.GetAllProfessions(), "ProfessionId", "PFName");

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
            //get list bank to create new customer
            //ViewBag.ListBanks = new SelectList(new BankManage().GetListAllBanks(), "BankID", "BankName");

            //List years
            //ViewBag.ListYears = new SelectList(ListYears(), "Value", "Text");

            //Get list historyoffer by idCustomer
            //ViewBag.ListHistoryOffers =  

            return View(model);
        }


        public string UploadFile(HttpPostedFileBase file)
        {
            var hashDateTimeNow = DateTime.Now.ToString("yyyyMMddHHmmss");
            var NameUser = User.Identity.Name;
            string targetpath = Server.MapPath("~/Assets/client/upload/cv");
            string getFilePath = Server.MapPath("~/Assets/");
            string FileNameWithoutExtension = file.FileName.Split('.')[0];
            string Extension = Path.GetExtension(file.FileName);
            DirectoryInfo di = Directory.CreateDirectory(targetpath + "/" + NameUser);
            file.SaveAs(targetpath + "/" + NameUser + "/" + FileNameWithoutExtension + hashDateTimeNow + Extension);
            string path = "/Assets/client/upload/cv/" + NameUser + "/" + FileNameWithoutExtension + hashDateTimeNow + Extension;
            return path;
        }


        //export to excel
        [HttpPost]
        public FileResult ExportToExcel(string keyWord, int? Status)
        {
            //Sheet Tổng Quan
            DataTable dtIndex = new DataTable("DanhSachNTV");

            dtIndex.Columns.AddRange(new DataColumn[7] {new DataColumn("STT"),
                                                    new DataColumn("Tên đăng nhập"),
                                                    new DataColumn("Tên đầy đủ"),
                                                    new DataColumn("Ngày đăng ký"),
                                                    new DataColumn("Vị trí làm việc"),
                                                    new DataColumn("Tỉnh/Thành phố"),
                                                    new DataColumn("Trạng thái")
                                                   });

            var model = cddmanage.GetListCandidatesBySearch(keyWord, Status);

            var stt = 0;
            foreach (var cdd in model)
            {
                stt++;
                dtIndex.Rows.Add(stt, cdd.AspNetUserDTO.UserName, cdd.CddFullName, cdd.registerDateString, cdd.ProfessionDTO.PFName, cdd.CityDTO.CName, cdd.statusString);
            }


            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dtIndex);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "DanhSachNTV.xlsx");
                }
            }
        }
    }
}