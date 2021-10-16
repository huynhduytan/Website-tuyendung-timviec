using JobHunt.BU.DTO;
using JobHunt.BU.Manage;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace JobHunt.Controllers
{
    public class RecruitJobController : Controller
    {

        RecruitJobManage recruitJobManage = new RecruitJobManage();
        ProfessionManage ProfessionManage = new ProfessionManage();
        CityManage cityManage = new CityManage();
        RecruitManage recruitManage = new RecruitManage();
        CandidateManage candidateManage = new CandidateManage();
        // GET: RecruitJob
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult ListJobs(string keyWord, int? idcity, int? idprofession, int page = 1, int pageSize = 10)
        {
            //Lấy ra danh sách tất cả danh mục công việc
            ViewBag.ListProfesstions = new SelectList(ProfessionManage.GetAllProfessions(), "ProfessionId", "PFName");

            //Lấy danh sách tỉnh
            ViewBag.ListCities = new SelectList(cityManage.GetAllCities(), "CityId", "CName");

            //Lấy danh sách thể loại công việc
            ViewBag.ListWorkTypes = new WorkTypeManage().GetAllWorkTypes();

            //Lấy danh sách lương
            ViewBag.ListSalaries = new SalaryMange().GetAllSalaries();

            //Lấy danh sách trình độ
            ViewBag.ListLevels = new LevelManage().GetAllLevels();

            //Lấy danh sách kinh nghiệm
            ViewBag.ListExperiences = new ExperienceManage().GetAllExperiences();

            //Search
            var model = recruitJobManage.GetListRecruitJobHaveSearchAndPaging(keyWord, idcity, idprofession, page, pageSize);

            //var suggest
            //ViewBag.SuggestJobs =
            var ListSuggest = new List<RecruitJobDTO>();
            ListSuggest = recruitJobManage.GetAllListRecruitJobs();
            if (idprofession != null)
            {
                var findProfesstion = new ProfessionManage().GetDetailProfession((int)idprofession);
                ListSuggest = ListSuggest.Where(x => x.ProfessionDTO.CareerDTO.CareerId == findProfesstion.CareerDTO.CareerId).ToList();
            }
            else
            {
                ListSuggest = ListSuggest.OrderByDescending(x=>x.RJCount).ToList();
            }
            //if(model.Count() == 0)
            //{

            //}
            ViewBag.ListSuggest = ListSuggest;
            //get search

            ViewBag.keyWord = keyWord;

            return View(model);
        }

        public ActionResult DetailJob(int? id)
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

            recruitJobManage.UpdateCounter(model.RecruitJobId);

            //Lấy danh sách công việc phù hợp
            ViewBag.ListRecentJobs = recruitJobManage.GetListRecentJobs(model.WorkTypeDTO.WorkTypeId, model.SalaryDTO.SalaryId, model.RJDistrictId);


            return View(model);
        }
        [Authorize(Roles = "Người tuyển dụng")]
        [ValidateInput(false)]
        public JsonResult PostANewJob(RecruitJobDTO recruitjobdto, int typeFix)
        {
            var message = "";
            var status = "error";
            if (string.IsNullOrEmpty(recruitjobdto.RJTitle) || string.IsNullOrEmpty(recruitjobdto.RJ_Describe) || string.IsNullOrEmpty(recruitjobdto.RJ_Require) || string.IsNullOrEmpty(recruitjobdto.RJBenefit) ||
                recruitjobdto.RJ_ProfessionId == 0 || recruitjobdto.RJ_SalaryId == 0 || recruitjobdto.RJ_LevelId == 0 || recruitjobdto.RJ_ExperienceId == 0 || recruitjobdto.RJ_WorkTypeId == 0
                || string.IsNullOrEmpty(recruitjobdto.RJPosition) || recruitjobdto.RJAmount == 0 ||
                string.IsNullOrEmpty(recruitjobdto.RJPhoneContact) || string.IsNullOrEmpty(recruitjobdto.RJNameContact) || string.IsNullOrEmpty(recruitjobdto.RJEmailContact) || recruitjobdto.RJCityId == 0 || recruitjobdto.RJDistrictId == 0 || string.IsNullOrEmpty(recruitjobdto.RJ_WorkPlace))
            {
                //Điền đủ thông tin bắt buộc.
                message = "Hãy điền đủ thông tin bắt buộc.";
            }
            else
            {
                if (typeFix == (int)JobHunt.BU.Common.Enum.TypeFix.Add)
                {
                    recruitjobdto.RJCount = 0;
                    recruitjobdto.RJPostDate = DateTime.Now;
                    recruitjobdto.RJStatus = (int?)BU.Common.Enum.EnumStatusJob.Approvaling;
                    recruitjobdto.RJ_RecruitId = recruitManage.GetRecruitInfoByIdAspNetUser(User.Identity.GetUserId()).RecruitId;
                    recruitjobdto.RJType = (int?)BU.Common.Enum.EnumTypeJob.Normal;
                    var insert = recruitJobManage.Insert(recruitjobdto);
                    if (insert != null)
                    {
                        message = "Tin đăng thành công. Tin đang đợi admin phê duyệt. Thời gian phê duyệt trong vòng 24h.";
                        status = "success";
                        string path = Server.MapPath("/Assets/template/SendMailToRegisterTinRao.html");
                        ParameterizedThreadStart job = new ParameterizedThreadStart(SendEmail);
                        Thread thread = new Thread(job);
                        thread.Start(new EmailClient() { TieuDe = insert.RJTitle, Path = path, IdPro = insert.RJ_ProfessionId, job = insert });
                    }
                }
                else
                {
                    var getJob = recruitJobManage.GetRecruitJobByID(recruitjobdto.RecruitJobId);
                    recruitjobdto.RJStatus = getJob.RJStatus;
                    recruitjobdto.RJType = (int?)BU.Common.Enum.EnumTypeJob.Normal;
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
                var senderEmail = new MailAddress("demoproject.3fgroup@gmail.com", "3FGROUP");
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


            return View(model);
        }

        public JsonResult DeleteJob(int idJob)
        {
            return Json(recruitJobManage.DeleteJob(idJob), JsonRequestBehavior.AllowGet);
        }

    }
}