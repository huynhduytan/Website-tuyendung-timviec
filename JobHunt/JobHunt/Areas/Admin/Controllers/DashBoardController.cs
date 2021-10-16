using JobHunt.BU.Manage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JobHunt.Areas.Admin.Controllers
{
    [Authorize]
    public class DashBoardController : Controller
    {
        AdvertisementManage adManage = new AdvertisementManage();
        CandidateManage cddmanage = new CandidateManage();
        RecruitJobManage recruitJobManage = new RecruitJobManage();
        // GET: Admin/DashBoard
        public ActionResult Index()
        {
            var model = adManage.GetListAdvertisementDashboard();

            ViewBag.NumberAccountActive = cddmanage.GetListCandidates((int)BU.Common.Enum.StatusCandidate.Active).Count;
            ViewBag.NumberAccountApprovaling = cddmanage.GetListCandidates((int)BU.Common.Enum.StatusCandidate.Approvaling).Count;
            ViewBag.NumberJobActive = recruitJobManage.GetListRecruitJobsUserIdNoPaging(null,(int)BU.Common.Enum.EnumStatusJob.Active).Count;
            ViewBag.NumberJobApprovaling = recruitJobManage.GetListRecruitJobsUserIdNoPaging(null, (int)BU.Common.Enum.EnumStatusJob.Approvaling).Count;

            ViewBag.ListTopFindJob = recruitJobManage.GetListFindJob();

            return View(model);
        }

        // Upload Image
        [HttpPost]
        public string UploadFileImage(HttpPostedFileBase file)
        {
            var pic = System.Web.HttpContext.Current.Request.Files["file"];
            // Validate
            // Xử lý upload lưu vào sv
            file.SaveAs(Server.MapPath("~/Assets/admin/upload/images/" + file.FileName));
            // Trả về link ảnh
            return "/Assets/admin/upload/images/" + file.FileName;
        }

        [Authorize(Roles = "admin")]
        public ActionResult SetupWebsite()
        {
            return View();
        }
    }
}