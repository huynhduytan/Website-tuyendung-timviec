using JobHunt.BU.DTO;
using JobHunt.BU.Common;
using JobHunt.BU.Manage;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.Owin;

namespace JobHunt.Controllers
{
    public class RecruitController : Controller
    {

        ProfessionManage ProfessionManage = new ProfessionManage();
        CityManage cityManage = new CityManage();
        RecruitJobManage recruitjobManage = new RecruitJobManage();
        RecruitManage recruitManage = new RecruitManage();
        CandidateManage candidateManage = new CandidateManage();


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


        // GET: Recruit
        public ActionResult Index()
        {
            return View();
        }


        [Authorize(Roles = "Người tuyển dụng")]
        public ActionResult ProfileRecruit()
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

            //Lấy ra danh sách size của công ty
            ViewBag.ListCompanySizes = new SelectList(new CompanySizeManage().GetAllCompanySizes(), "CompanySizeId", "CSShow");

            var idRecruit = User.Identity.GetUserId();

            var model = new RecruitManage().GetRecruitInfoByIdAspNetUser(User.Identity.GetUserId());

            return View(model);
        }

        public JsonResult UpdateProfileRecruit(RecruitDTO rcdto)
        {
            var resultCode = 0;
            var messageResult = "";
            var statusResult = "error";
            if (string.IsNullOrEmpty(rcdto.RILogo) || string.IsNullOrEmpty(rcdto.RICompanyName) || string.IsNullOrEmpty(rcdto.RICoverImage) || string.IsNullOrEmpty(rcdto.RIAvatar)
                || string.IsNullOrEmpty(rcdto.FoundedYear.ToString()) || string.IsNullOrEmpty(rcdto.RIAbout) || string.IsNullOrEmpty(rcdto.RI_CompanySizeId.ToString()) || string.IsNullOrEmpty(rcdto.RI_ProfessionId.ToString()))
            {

                resultCode = 2;
            }
            else if (rcdto.RICompanyName.Length < 3)
            {

                resultCode = 3;
            }
            else
            {
                resultCode = recruitManage.UpdateProfileRecruit(rcdto, (int)JobHunt.BU.Common.Enum.TypeUpdate.UpdateInformation);
            }

            switch (resultCode)
            {
                case 0:
                    messageResult = "Đã xảy ra lỗi trong quá trình sửa";
                    break;
                case 1:
                    {
                        messageResult = "Sửa thành công";
                        statusResult = "success";
                    }
                    break;
                case 2:
                    messageResult = "Hãy nhập đầy đủ thông tin";
                    break;
                case 3:
                    messageResult = "Hãy điền đúng định dạng";
                    break;
                default:
                    break;
            }
            return Json(new { message = messageResult, status = statusResult }, JsonRequestBehavior.AllowGet);
        }


        public JsonResult UpdateContactRecruit(RecruitDTO rcdto)
        {
            var resultCode = 0;
            var messageResult = "";
            var statusResult = "error";
            if (string.IsNullOrEmpty(rcdto.RIPhone) || string.IsNullOrEmpty(rcdto.RI_CityId.ToString()) || string.IsNullOrEmpty(rcdto.RI_DistrictId.ToString()) || string.IsNullOrEmpty(rcdto.RI_WardId.ToString()))
            {

                resultCode = 2;
            }
            else if (rcdto.RIPhone.Length < 3)
            {

                resultCode = 3;
            }
            else
            {
                if (CheckExistsEmailOfRecruit(rcdto))
                    resultCode = 4;
                else if (CheckExistsPhoneOfRecruit(rcdto))
                    resultCode = 5;
                else
                    resultCode = recruitManage.UpdateProfileRecruit(rcdto, (int)JobHunt.BU.Common.Enum.TypeUpdate.UpdateContact);
            }

            switch (resultCode)
            {
                case 1:
                    {
                        messageResult = "Sửa thành công";
                        statusResult = "success";
                    }
                    break;
                case 2:
                    messageResult = "Hãy nhập đầy đủ thông tin";
                    break;
                case 3:
                    messageResult = "Hãy điền đúng định dạng";
                    break;
                case 4:
                    messageResult = "Email đã được sử dụng";
                    break;
                case 5:
                    messageResult = "Số điện thoại đã được sử dụng";
                    break;
                default:
                    messageResult = "Đã xảy ra lỗi trong quá trình sửa";
                    break;
            }
            return Json(new { message = messageResult, status = statusResult }, JsonRequestBehavior.AllowGet);
        }

        //check exists phone
        public bool CheckExistsPhoneOfRecruit(RecruitDTO rcdto)
        {
            var result = false;
            var getAllRecruit = recruitManage.GetAllRecruits();
            if (getAllRecruit.Any(x => x.RecruitId != rcdto.RecruitId && x.RIPhone == rcdto.RIPhone))
                result = true;
            return result;
        }

        //check exists email
        public bool CheckExistsEmailOfRecruit(RecruitDTO rcdto)
        {
            var result = false;
            var getAllRecruit = recruitManage.GetAllRecruits();
            if (getAllRecruit.Any(x => x.RecruitId != rcdto.RecruitId && x.RIEmail == rcdto.RIEmail))
                result = true;
            return result;
        }

        [Authorize(Roles = "Người tuyển dụng")]
        public ActionResult PostJob()
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

        

        


        //Danh sách công việc
        [Authorize(Roles = "Người tuyển dụng")]
        public ActionResult ListJobsPost(int page = 1, int pageSize = 10)
        {
            var UserId = User.Identity.GetUserId();
            //Lấy thông tin recruit dựa vào id user
            var getRecruit = recruitManage.GetRecruitInfoByIdAspNetUser(UserId);
            //Lấy tổng số công việc đã đăng.
            var model = recruitjobManage.GetListRecruitJobsUserIdPaging(getRecruit.RecruitId, page, pageSize);

            //lấy ra model count

            //Tổng số công việc đã đăng

            var modelCount = recruitjobManage.GetListRecruitJobsUserIdNoPaging(getRecruit.RecruitId, null);
            ViewBag.PostJobs = modelCount.Count;

            //Số ứng viên đã ứng tuyển vào công ty.
            var AppliedCompany = 0;
            foreach (var rcj in modelCount)
            {
                AppliedCompany += rcj.AppliedCount;
            }
            ViewBag.AppliedCompany = AppliedCompany;

            //Số công việc phù hợp
            var listJobsFit = 0;
            foreach (var rcj in modelCount)
            {
                if (rcj.RJStatus == (int?)JobHunt.BU.Common.Enum.EnumStatusJob.Active)
                    listJobsFit += 1;
            }
            ViewBag.ListJobsFit = listJobsFit;

            ViewBag.Recruit = getRecruit;

            return View(model);
        }

        [Authorize(Roles = "Người tuyển dụng")]
        public ActionResult ListCandidatesApplied(int page = 1, int pageSize = 10)
        {
            var UserId = User.Identity.GetUserId();
            //Lấy thông tin recruit dựa vào id user
            var getRecruit = recruitManage.GetRecruitInfoByIdAspNetUser(UserId);

            var model = recruitjobManage.GetListCandidatesAppliedPaging(getRecruit.RecruitId, page, pageSize);
            ViewBag.Recruit = getRecruit;
            return View(model);
        }

        [Authorize(Roles = "Người tuyển dụng")]
        public ActionResult UpdateAccount()
        {
            return View();
        }

        [Authorize(Roles = "Người tuyển dụng")]
        public ActionResult ChangePassword()
        {
            var idRecruit = User.Identity.GetUserId();

            var model = new RecruitManage().GetRecruitInfoByIdAspNetUser(User.Identity.GetUserId());

            return View(model);
        }

        [Authorize(Roles = "Người tuyển dụng")]
        public async Task<ActionResult> ChangePasswordRC(string OddPass, string NewPass, string ConfirmPass)
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

        public ActionResult ListRecruits(string keyWord, int? idcity, int? idprofession, string chooseDate, List<int?> chooseWorkType, List<int?> chooseSalary, List<int?> chooseExperience, List<int?> chooseGender, List<int?> chooseLevel, int page = 1, int pageSize = 10)
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

            //Search
            var model = recruitManage.GetListRecruitHaveSearchAndPaging(keyWord, idcity, idprofession, chooseDate, chooseWorkType, chooseSalary, chooseExperience, chooseGender, chooseLevel, page, pageSize);


            //get search
            ViewBag.keyWord = keyWord;
            ViewBag.chooseWorkType = chooseWorkType;
            ViewBag.chooseSalary = chooseSalary;
            ViewBag.chooseExperience = chooseExperience;
            ViewBag.chooseGender = chooseGender;
            ViewBag.chooseDate = chooseDate;
            ViewBag.chooseLevel = chooseLevel;

            return View(model);
        }


        public ActionResult DetailRecruit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var model = recruitManage.GetRecruitInfoById((int)id);

            if (model == null)
            {
                return HttpNotFound();
            }
            //
            var UserId = User.Identity.GetUserId();

            //Tổng số công việc đã đăng
            ViewBag.PostJobs = recruitjobManage.GetListRecruitJobsUserIdNoPaging((int)id, null);


            return View(model);
        }


    }
}