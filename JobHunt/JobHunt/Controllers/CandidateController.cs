using JobHunt.BU.ConvertData;
using JobHunt.BU.DTO;
using JobHunt.BU.Manage;
using JobHunt.Model.DAO;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Rotativa;
using JobHunt.Model.EF;

namespace JobHunt.Controllers
{
    public class CandidateController : Controller
    {

        ProfessionManage ProfessionManage = new ProfessionManage();
        CityManage cityManage = new CityManage();
        ConvertDataCandidate convertData = new ConvertDataCandidate();
        CandidateManage candidateManage = new CandidateManage();
        RecruitJobManage recruitjobManage = new RecruitJobManage();
        CandidatePostResumeManage cddPostResumeManage = new CandidatePostResumeManage();

        private ApplicationUserManager _userManager;
        private ApplicationSignInManager _signInManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        [Authorize(Roles = "Người tìm việc")]
        // GET: Candidate
        public ActionResult CandidateProfile()
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

            var InfoCandidate = new CandidateManage().GetCandidateInfoByIdAspNetUser(User.Identity.GetUserId());

            return View(InfoCandidate);
        }


        [Authorize(Roles = "Người tìm việc")]
        //Update profile candidate
        public JsonResult UpdateProfile(CandidateDTO candidateDTO)
        {
            var messageUpdate = "";
            var statusUpdate = "success";
            var updateProfile = candidateManage.UpdateProfileCandidate(candidateDTO);
            if (updateProfile)
                messageUpdate = "Cập nhật thông tin " + candidateDTO.CddFullName + " thành công!";
            else
            {
                statusUpdate = "error";
                messageUpdate = "Cập nhật thông tin " + candidateDTO.CddFullName + " không thành công!";
            }
            return Json(new { message = messageUpdate, status = statusUpdate }, JsonRequestBehavior.AllowGet);
        }


        [Authorize(Roles = "Người tìm việc")]
        public JsonResult UpdateSocialAndContact(CandidateDTO candidateDTO)
        {
            var messageUpdate = "";
            var statusUpdate = "success";
            var updateSAC = candidateManage.UpdateSocialAndContact(candidateDTO);
            if (updateSAC)
                messageUpdate = "Cập nhật thông tin xã hội và liên hệ " + candidateDTO.CddFullName + " thành công!";
            else
            {
                statusUpdate = "error";
                messageUpdate = "Cập nhật thông tin xã hội và liên hệ  " + candidateDTO.CddFullName + " không thành công!";
            }
            return Json(new { message = messageUpdate, status = statusUpdate }, JsonRequestBehavior.AllowGet);
        }


        [Authorize(Roles = "Người tìm việc")]
        public ActionResult CandidateUploadCV()
        {
            var InfoCandidate = new CandidateManage().GetCandidateInfoByIdAspNetUser(User.Identity.GetUserId());

            return View(InfoCandidate);
        }


        public JsonResult UploadCV(HttpPostedFileBase file, string Describe)
        {
            var messageUpdate = "";
            var statusUpdate = "success";
            var path = "";
            var InfoCandidate = new CandidateManage().GetCandidateInfoByIdAspNetUser(User.Identity.GetUserId());
            if (file != null)
                path = UploadFile(file);
            else
                path = InfoCandidate.CddPathCV;

            var candidatedto = new CandidateDTO()
            {
                CddPathCV = path,
                CddDescribeCV = Describe,
                Cdd_AspNetUserId = User.Identity.GetUserId()
            };

            var uploadcv = new CandidateManage().UpdateCV(candidatedto);
            if (uploadcv)
                messageUpdate = "Tải cv lên thành công!";
            else
            {
                messageUpdate = "Đã xảy ra lỗi khi tải lên!";
                statusUpdate = "error";
            }

            return Json(new { message = messageUpdate, status = statusUpdate }, JsonRequestBehavior.AllowGet);
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

        [Authorize(Roles = "Người tìm việc")]
        public ActionResult ChangePassword()
        {
            var InfoCandidate = new CandidateManage().GetCandidateInfoByIdAspNetUser(User.Identity.GetUserId());
            return View(InfoCandidate);
        }

        [Authorize(Roles = "Người tìm việc")]
        public async Task<ActionResult> ChangePasswordCdd(string OddPass, string NewPass, string ConfirmPass)
        {
            var status = "success";
            var message = "";
            if (!NewPass.Equals(ConfirmPass))
            {
                status = "error";
                message = "Xác nhận mật khẩu không đúng";
            }
            else
            {
                var result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), OddPass, NewPass);
                if (result.Succeeded)
                {
                    var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                    if (user != null)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    }
                    message = "Thay đổi mật khẩu thành công";
                }
                else
                {
                    status = "error";
                    foreach (var e in result.Errors)
                    {
                        message += e.ToString();
                    }
                }
            }
            return Json(new { status = status, message = message }, JsonRequestBehavior.AllowGet);

        }

        [Authorize(Roles = "Người tìm việc")]
        public ActionResult UpdateAccount()
        {
            var InfoCandidate = new CandidateManage().GetCandidateInfoByIdAspNetUser(User.Identity.GetUserId());

            return View(InfoCandidate);
        }


        [Authorize(Roles = "Người tìm việc")]
        public ActionResult AppliedJobs(int page = 1, int pageSize = 5)
        {
            var UserID = User.Identity.GetUserId();
            var idCandidate = candidateManage.GetCandidateInfoByIdAspNetUser(UserID).CandidateId;
            var model = cddPostResumeManage.GetListCandidatePostResumeHaveSearchAndPaging(idCandidate, page, pageSize);
            return View(model);
        }


        [Authorize(Roles = "Người tìm việc")]
        public ActionResult SaveJobs(int page = 1, int pageSize = 5)
        {
            var UserID = User.Identity.GetUserId();
            var model = candidateManage.GetListSaveJobs(UserID, page, pageSize);
            return View(model);
        }

        /// Get list candidate
        public ActionResult ListCandidates(string keyWord, int? idcity, int? idprofession, int page = 1, int pageSize = 10)
        {
            ////Lấy ra danh sách tất cả danh mục công việc
            //ViewBag.ListProfesstions = new SelectList(ProfessionManage.GetAllProfessions(), "ProfessionId", "PFName");

            ////Lấy danh sách tỉnh
            //ViewBag.ListCities = new SelectList(cityManage.GetAllCities(), "CityId", "CName");

            ////Lấy danh sách thể loại công việc
            //ViewBag.ListWorkTypes = new WorkTypeManage().GetAllWorkTypes();

            ////Lấy danh sách lương
            //ViewBag.ListSalaries = new SalaryMange().GetAllSalaries();

            ////Lấy danh sách trình độ
            //ViewBag.ListLevels = new LevelManage().GetAllLevels();

            ////Lấy danh sách kinh nghiệm
            //ViewBag.ListExperiences = new ExperienceManage().GetAllExperiences();

            int? idProfessionRecruit;

            var idRecruit = User.Identity.GetUserId();
            if (!string.IsNullOrEmpty(idRecruit))
            {
                var getInfoRecruit = new RecruitManage().GetRecruitInfoByIdAspNetUser(idRecruit);
                if(getInfoRecruit != null)
                    idProfessionRecruit = getInfoRecruit.RI_ProfessionId;
                else
                    idProfessionRecruit = null;
            }
            else
            {
                idProfessionRecruit = null;
            }

            //Search
            var model = candidateManage.GetListCandidateHaveSearchAndPaging(keyWord, idcity, idprofession, idProfessionRecruit, page, pageSize);


            //get search
            ViewBag.keyWord = keyWord;

            return View(model);
        }

        public ActionResult DetailCandidate(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var model = candidateManage.GetCandidateInfoById((int)id);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        //save job
        public JsonResult SaveJob(SaveJobDTO sjdto)
        {
            var resultCode = 0;
            var messageResult = "";
            var statusResult = "success";
            try
            {
                var IdUser = User.Identity.GetUserId();
                if (!string.IsNullOrEmpty(IdUser))
                {
                    //if (sjdto.postBy.Equals("admin"))
                    //{

                    //}
                    //else 
                    if (CheckExistIdJob((int)sjdto.SJ_RecruitJobId))
                    {
                        var getIdCandidateByIdUser = candidateManage.GetCandidateInfoByIdAspNetUser(IdUser).CandidateId;
                        var SaveJob = new SaveJobDTO()
                        {
                            SJStatus = true,
                            SJ_CandidateId = getIdCandidateByIdUser,
                            SJ_RecruitJobId = sjdto.SJ_RecruitJobId
                        };
                        var UpdateSaveJob = true;
                        var checkResult = 0;
                        if (sjdto.status.Equals("save"))
                        {
                            UpdateSaveJob = new SaveJobManage().Insert(SaveJob);
                            checkResult = 1;
                        }
                        else
                        {
                            var GetIDSJ = new CandidateManage().CheckSaveJob(IdUser, (int)sjdto.SJ_RecruitJobId);
                            sjdto.SaveJobId = GetIDSJ;
                            UpdateSaveJob = new SaveJobManage().Delete(sjdto);
                            checkResult = 2;
                        }

                        if (UpdateSaveJob)
                        {
                            if (checkResult == 1)
                                messageResult = "Lưu thành công!";
                            else
                                messageResult = "Đã hủy lưu!";
                        }
                        else
                        {
                            messageResult = "Lưu không thành công!";
                            statusResult = "error";
                        }
                    }
                    else
                    {
                        messageResult = "Công việc hết hạn hoặc không tồn tại!";
                        statusResult = "error";
                    }
                }
                else
                {
                    resultCode = 1;
                    messageResult = "Bạn hãy đăng nhập trước!";
                    statusResult = "error";
                }
            }
            catch (Exception)
            {
                messageResult = "Lưu không thành công!";
                statusResult = "error";
            }

            return Json(new { message = messageResult, status = statusResult, resultCode = resultCode }, JsonRequestBehavior.AllowGet);
        }

        public bool CheckExistIdJob(int idJob)
        {
            var getAllJobs = recruitjobManage.GetAllRecruitJobs();
            var statusResult = false;
            foreach (var job in getAllJobs)
            {
                if (job.RecruitJobId == idJob)
                {
                    statusResult = true;
                    break;
                }
            }
            return statusResult;
        }


        public JsonResult PostResume(string UserID, string JobID, string PhoneUser, string CVOld, HttpPostedFileBase file)
        {
            var messageResult = "";
            var statusResult = "success";
            var path = "";
            var getIdCandidateByIdUser = candidateManage.GetCandidateInfoByIdAspNetUser(UserID);
            if (!string.IsNullOrEmpty(CVOld))
            {
                path = "/Assets/client/upload/cv/" + getIdCandidateByIdUser.AspNetUserDTO.UserName + "/" + CVOld;
            }
            else
            {
                path = UploadFile(file);
            }
            var cddpostresumedto = new CandidatePostResumeDTO()
            {
                CddPhone = PhoneUser,
                CPRPostDate = DateTime.Now,
                CPRStatus = true,
                CPR_CandidateId = getIdCandidateByIdUser.CandidateId,
                CPR_RecruitJobId = int.Parse(JobID),
                CddPathFileCV = path
            };
            var checkPosted = new CandidatePostResumeManage().CheckPostedResume(cddpostresumedto.CPR_CandidateId, cddpostresumedto.CPR_RecruitJobId);
            if (!checkPosted)
            {
                var postResume = new CandidatePostResumeManage().Insert(cddpostresumedto);
                if (postResume)
                {
                    messageResult = "Ứng tuyển thành công";
                }
                else
                {
                    messageResult = "Đã xảy ra lỗi trong quá trình ứng tuyển";
                    statusResult = "error";
                }
            }
            else
            {
                messageResult = "Bạn đã ứng tuyển công việc này rồi. Đợi nhà tuyển dụng liên hệ lại nhé.";
                statusResult = "error";
            }

            return Json(new { message = messageResult, status = statusResult }, JsonRequestBehavior.AllowGet);
        }


        //
        public JsonResult DeleteAppliedJob(int id)
        {
            var resultCode = 0;
            var manage = new CandidatePostResumeManage();
            var status = "error";
            var message = "";
            var getApplied = manage.GetDetail(id);
            if (getApplied != null)
            {
                resultCode = manage.Delete(getApplied.CandidatePostResumeId);
            }
            switch (resultCode)
            {
                case 1:
                    {
                        status = "success";
                        message = "Hủy thành công";
                    }
                    break;
                default:
                    message = "Hủy không thành công";
                    break;
            }
            return Json(new { status = status, resultCode = resultCode, message = message }, JsonRequestBehavior.AllowGet);
        }

        //show profile to download cv
        public ActionResult ShowProfile(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var model = candidateManage.GetCandidateInfoById((int)id);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        public ActionResult PrintPartialViewToPdf(int id)
        {
            using (JobHuntEntities db = new JobHuntEntities())
            {
                var model = candidateManage.GetCandidateInfoById((int)id);

                var report = new PartialViewAsPdf("~/Views/Candidate/ShowProfile.cshtml", model);
                return report;
            }

        }

        public FileResult DownloadCoverLetter(int id)
        {
            using (JobHuntEntities db = new JobHuntEntities())
            {
                var model = candidateManage.GetCandidateInfoById((int)id);
                string fileName = model.nameFileCV;
                string[] namefile = model.CddPathCV.Split('/');
                string full = Path.Combine(Server.MapPath("~/Assets/client/upload/cv/" + model.CddUserName), namefile[namefile.Length - 1]);
                return File(full, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
            }

        }
    }
}