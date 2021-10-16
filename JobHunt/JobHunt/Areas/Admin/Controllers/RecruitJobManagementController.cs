using ClosedXML.Excel;
using JobHunt.BU.DTO;
using JobHunt.BU.Manage;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace JobHunt.Areas.Admin.Controllers
{
    [Authorize(Roles = "Quản trị, Quản lý người tuyển dụng, Quản lý người tìm việc, Quản lý công việc")]
    public class RecruitJobManagementController : Controller
    {

        // GET: Admin/RecruitJobManagement
        ProfessionManage ProfessionManage = new ProfessionManage();
        CityManage cityManage = new CityManage();
        RecruitJobManage recruitJobManage = new RecruitJobManage();
        RecruitManage recruitManage = new RecruitManage();
        CandidateManage candidateManage = new CandidateManage();
        public ActionResult Index(int? status = null)
        {
            // Lấy ra danh sách tất cả danh mục công việc
            ViewBag.ListProfessions = new SelectList(ProfessionManage.GetAllProfessions(), "ProfessionId", "PFName");
            // Lấy danh sách các tỉnh
            ViewBag.ListCities = new SelectList(cityManage.GetAllCities(), "CityId", "CName");
            // Lấy danh sách các thể loại công việc
            ViewBag.ListWorkType = new WorkTypeManage().GetAllWorkTypes();
            // Lấy danh sách lươngs
            ViewBag.ListSalaries = new SalaryMange().GetAllSalaries();
            // Lấy danh sách trình độ
            ViewBag.ListLevels = new LevelManage().GetAllLevels();
            // Lấy danh sách kinh nghiem
            ViewBag.ListExperiences = new ExperienceManage().GetAllExperiences();

            // 
            var model = recruitJobManage.GetListRecruitJobsUserIdNoPaging(null, status);
            return View(model);
        }
        // get List by date
        [HttpPost]
        public ActionResult Index(DateTime? startdate, DateTime? enddate)
        {
            return View(recruitJobManage.GetAllListRecruitJobsByDate(startdate,enddate));
        }
        public ActionResult PostJob()
        {
            // Lấy ra danh sách tất cả danh mục công việc
            ViewBag.ListProfessions = new SelectList(ProfessionManage.GetAllProfessions(), "ProfessionId", "PFName");
            // Lấy danh sách các tỉnh
            ViewBag.ListCities = new SelectList(cityManage.GetAllCities(), "CityId", "CName");
            // Lấy danh sách trình độ
            ViewBag.ListLevels = new SelectList(new LevelManage().GetAllLevels(), "LevelInfoId", "LIName");
            // Lấy danh sách kinh nghiệm
            ViewBag.ListExperiences = new SelectList(new ExperienceManage().GetAllExperiences(), "ExperienceId", "EShow");
            // Lấy danh sách lương
            ViewBag.ListSalaries = new SelectList(new SalaryMange().GetAllSalaries(), "SalaryId", "SShow");
            // Lấy danh sách hình thức làm việc
            ViewBag.ListWorkTypes = new SelectList(new WorkTypeManage().GetAllWorkTypes(), "WorkTypeId", "WTName");
            ViewBag.ListCompany = new SelectList(new RecruitManage().GetAllRecruits(),"RecruitId", "RICompanyName");

            return View();
        }
        public ActionResult UpdateJob(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var model = recruitJobManage.GetRecruitJobByID((int)id);
            if (model == null)
            {
                return HttpNotFound();
            }
            // Lấy ra danh sách tất cả danh mục công việc
            ViewBag.ListProfessions = new SelectList(ProfessionManage.GetAllProfessions(), "ProfessionId", "PFName");
            // Lấy danh sách các tỉnh
            ViewBag.ListCities = new SelectList(cityManage.GetAllCities(), "CityId", "CName");
            // Lấy danh sách trình độ
            ViewBag.ListLevels = new SelectList(new LevelManage().GetAllLevels(), "LevelInfoId", "LIName");
            // Lấy danh sách kinh nghiệm
            ViewBag.ListExperiences = new SelectList(new ExperienceManage().GetAllExperiences(), "ExperienceId", "EShow");
            // Lấy danh sách lương
            ViewBag.ListSalaries = new SelectList(new SalaryMange().GetAllSalaries(), "SalaryId", "SShow");
            // Lấy danh sách hình thức làm việc
            ViewBag.ListWorkTypes = new SelectList(new WorkTypeManage().GetAllWorkTypes(), "WorkTypeId", "WTName");
            return View(model);

        }
        public JsonResult DeleteJob(int idJob)
        {
            return Json(recruitJobManage.DeleteJob(idJob), JsonRequestBehavior.AllowGet);
        }
        [ValidateInput(false)]
        public JsonResult PostANewJob(RecruitJobDTO recruitjobdto, int typeFix)
        {
            var message = "";
            var status = "error";
            var testDate = true;
            try
            {
                recruitjobdto.RJExpirationDate = BU.Common.ConvetDate.ParseRequestDate(recruitjobdto.RJExpirationDateString);
            }
            catch (Exception)
            {
                testDate = false;
            }
            if (string.IsNullOrEmpty(recruitjobdto.RJTitle) || string.IsNullOrEmpty(recruitjobdto.RJ_Describe) || string.IsNullOrEmpty(recruitjobdto.RJ_Require) || string.IsNullOrEmpty(recruitjobdto.RJBenefit) ||
                recruitjobdto.RJ_ProfessionId == 0 || recruitjobdto.RJ_SalaryId == 0 || recruitjobdto.RJ_LevelId == 0 || recruitjobdto.RJ_ExperienceId == 0 || recruitjobdto.RJ_WorkTypeId == 0
                || string.IsNullOrEmpty(recruitjobdto.RJPosition) || recruitjobdto.RJAmount == 0 ||
                string.IsNullOrEmpty(recruitjobdto.RJPhoneContact) || string.IsNullOrEmpty(recruitjobdto.RJNameContact) || string.IsNullOrEmpty(recruitjobdto.RJEmailContact) || recruitjobdto.RJCityId == 0 || recruitjobdto.RJDistrictId == 0 || string.IsNullOrEmpty(recruitjobdto.RJ_WorkPlace)
                || recruitjobdto.RJExpirationDate == null)
            {
                //Điền đủ thông tin bắt buộc.
                message = "Hãy điền đủ thông tin bắt buộc.";
            }
            else if (!testDate)
            {
                message = "Sai địng dạng";
            }
            else
            {
                if (typeFix == (int)JobHunt.BU.Common.Enum.TypeFix.Add)
                {
                    recruitjobdto.RJCount = 0;
                    recruitjobdto.RJPostDate = DateTime.Now;
                    recruitjobdto.RJType = (int?)BU.Common.Enum.EnumTypeJob.Normal;
                    var insert = recruitJobManage.Insert(recruitjobdto);
                    if (insert != null)
                    {
                        message = "Tin đăng thành công. Tin đang chờ admin phê duyệt. Thời gian trong 24h.";
                        status = "success";
                        string path = Server.MapPath("/Assets/template/SendMailToRegisterTinRao.html");
                        ParameterizedThreadStart job = new ParameterizedThreadStart(SendEmail);
                        Thread thread = new Thread(job);
                        thread.Start(new EmailClient() { TieuDe = insert.RJTitle, Path = path, IdPro = insert.RJ_ProfessionId, job = insert });
                    }
                }
                else
                {
                    if (recruitJobManage.UpdateJob(recruitjobdto) != 0)
                    {
                        message = "Sửa thông tin thành công.";
                        status = "success";
                    }
                    else
                    {
                        message = "Đã xảy ra lỗi trong quá trình sửa. Hãy thử lại sau.";
                        status = "error";
                    }
                }
            }
            return Json(new { message = message, status = status }, JsonRequestBehavior.AllowGet);
        }
        class EmailClient
        {
            public string TieuDe { get; set; }
            public string Hinhthuc { get; set; }
            public string Path { get; set; }
            public int? IdPro { get; set; }
            public RecruitJobDTO job { get; set; }
        }
        // string tieuDe, int codeInsert, string hinhthuc
        static void SendEmail(object obj)
        {
            var job = obj as EmailClient;
            var getListRegisters = new SignUpNewsletterManage().GetListSignUpByType(1, job.IdPro);
            foreach (var j in getListRegisters)
            {
                SendMailToRegister(job.Path, j.Name, "tin đăng tuyển dụng", "http://103.237.147.20:333/tuyen-dung/chi-tiet-viec-lam/" + BU.Common.GenerateUrl.GenerateSlug(job.job.RJTitle, job.job.RecruitJobId), j.Email);
            }
        }
        public static int SendMailToRegister(string path, string name, string tenht, string link, string mail)
        {
            int resultCode = 0;
            try
            {
                string content = System.IO.File.ReadAllText(path);
                content = content.Replace("{{name}}", name);
                content = content.Replace("{{tenhinhthuc}}", tenht);
                content = content.Replace("{{link}}", link);
                var senderEmail = new MailAddress("demo@gmail.com", "AAA");
                var receiverEmail = new MailAddress(mail);
                var password = "3fk11997";
                var sub = "Thông báo từ JobHunt";
                var body = content;
                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(senderEmail.Address, password)
                };
                using (var mess = new MailMessage(senderEmail, receiverEmail)
                {
                    Subject = sub,
                    Body = body
                })
                {
                    smtp.Send(mess);
                }
                resultCode = 1;
            }
            catch (Exception)
            {
                resultCode = 2;
            }
            return resultCode;
        }
        // Export Excel
        [HttpPost]
        public FileResult ExportToExcel(string keyWord, int? Status)
        {
            //Sheet Tổng Quan
            DataTable dtIndex = new DataTable("DanhSachCongViec");

            dtIndex.Columns.AddRange(new DataColumn[7] {new DataColumn("STT"),
                                                    new DataColumn("Tiêu đề tin"),
                                                    new DataColumn("Ngày đăng tin"),
                                                    new DataColumn("Ngày hết hạn"),
                                                    new DataColumn("Tài khoản đăng tin"),
                                                    new DataColumn("Loại tin"),
                                                    new DataColumn("Trạng thái")
                                                   });

            var model = recruitJobManage.GetListRecruitJobsBySearch(keyWord, Status);

            var stt = 0;

            foreach (var job in model)
            {
                stt++;
                dtIndex.Rows.Add(stt, job.RJTitle, job.RJPostDate.Value.ToString("dd-MM-yyyy"), job.RJExpirationDate > DateTime.Now ? job.RJExpirationDate.Value.ToString("dd-MM-yyyy") : "Đã hết hạn", job.RecruitDTO != null ? job.RecruitDTO.RIUserName : "Admin", job.NameType, job.TrangThai);
            }


            using (XLWorkbook wb = new XLWorkbook())
            {
                var ws = wb.Worksheets.Add(dtIndex);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "DanhSachCongViec.xlsx");
                }
            }

        }
    }
}