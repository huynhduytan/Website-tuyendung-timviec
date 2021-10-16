using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using JobHunt.Models;
using JobHunt.BU.DTO;
using JobHunt.BU.Manage;
using System.Net;
using Microsoft.Owin.Security;
using JobHunt.Model.EF;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Web.Security;
using ClosedXML.Excel;
using System.Data;

namespace JobHunt.Areas.Admin.Controllers
{
    [Authorize(Roles = "Quản trị, Quản lý người tuyển dụng, Quản lý người tìm việc, Quản lý tài khoản")]
    public class AccountManageController : Controller
    {
        private WebMasterManage _webMasterManage = new WebMasterManage();
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public AccountManageController()
        {
        }

        public AccountManageController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
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

        // GET: Admin/AccountManage
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CreateAccount()
        {
            return View();
        }

        public ActionResult Decentralization(string keyWord, int page = 1, int pageSize = 10)
        {
            var model = _webMasterManage.GetListWebmasterInfoHaveSearchAndPaging(keyWord, page, pageSize);
            ViewBag.keyWord = keyWord;
            ViewBag.stt = (page - 1) * pageSize;

            var GetListRoles = new JobHuntEntities().AspNetRoles.ToList();
            var listRolesDTO = new List<AspNetRoleDTO>();
            var convertdatarole = new ConvertDataAspNetRole();
            foreach (var role in GetListRoles)
            {
                listRolesDTO.Add(convertdatarole.ConvertToDTO(role));
            }
            ViewBag.roles = listRolesDTO;
            return View(model);
        }

        public JsonResult GetListDecentralization(string idUser)
        {
            var roles = UserManager.FindById(idUser).Roles.ToList();
            List<string> rolesdto = new List<string>();
            foreach (var role in roles)
            {
                rolesdto.Add(role.RoleId);
            }
            
            return base.Json(rolesdto, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdateDecentralization(List<string> listroles, string idAspNetUser)
        {
            var resultCode = 0;
            try
            {
                //delete all role by iduser
                var getRoles = UserManager.FindById(idAspNetUser).Roles.ToList();
                foreach (var role in getRoles)
                {
                    UserManager.RemoveFromRole(idAspNetUser, _webMasterManage.GetRoleById(role.RoleId).Name);
                }
                //add new role
                foreach (var role in listroles)
                {
                    UserManager.AddToRole(idAspNetUser, role);
                    resultCode = 1;
                }
            }
            catch (Exception)
            {
                resultCode = 0;
            }
            return base.Json(resultCode, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ListAccount()
        {
            var model = _webMasterManage.GetListWebmasterInfos();
            return View(model);
        }
        // get List by date yead
        [HttpPost]
        public ActionResult ListAccount(DateTime? startdate, DateTime? enddate)
        {
            return View(_webMasterManage.GetListWebmasterInforByDate(startdate,enddate));
        }

        public JsonResult DeleteAccount(int? idwebmt)
        {
            var resultCode = 0;
            var resultMessage = "";
            var resultStatus = "error";
            if (idwebmt == null || idwebmt == 0)
            {
                resultMessage = "Mã NTV không được để trống";
            }
            else
            {
                resultCode = _webMasterManage.Delete((int)idwebmt);
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


        public ActionResult DetailAccount(int? id)
        {
            ActionResult result;
            if (id == null)
            {
                result = new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                WebmasterInfoDTO infoWebmasterById = this._webMasterManage.GetInfoWebmasterById(id.Value);
                result = !ReferenceEquals(infoWebmasterById, null) ? ((ActionResult)base.View(infoWebmasterById)) : ((ActionResult)base.HttpNotFound());
            }
            return result;
        }


        //export to excel
        [HttpPost]
        public FileResult ExportToExcel(string keyWord, int? Status)
        {
            //Sheet Tổng Quan
            DataTable dtIndex = new DataTable("DanhSachTK");

            dtIndex.Columns.AddRange(new DataColumn[7] {new DataColumn("STT"),
                                                    new DataColumn("Tên đăng nhập"),
                                                    new DataColumn("Tên đầy đủ"),
                                                    new DataColumn("Ngày vào làm"),
                                                    new DataColumn("Vị trí"),
                                                    new DataColumn("Địa chỉ"),
                                                    new DataColumn("Trạng thái")
                                                   });

            var model = _webMasterManage.GetListWebmasterInfosBySearch(keyWord, Status);

            var stt = 0;
            foreach (var webmt in model)
            {
                stt++;
                dtIndex.Rows.Add(stt, webmt.AspNetUserDTO.UserName, webmt.WIFullName, webmt.WIDateStart.Value.ToString("dd-MM-yyyy"), webmt.nameRole, webmt.WIAddress, webmt.WIStatusString);
            }


            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dtIndex);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "DanhSachTK.xlsx");
                }
            }
        }




        public async Task<ActionResult> UpdateAccount(WebmasterInfoDTO accountdto, int typeFix)
        {
            var resultCode = 0;
            var message = "";
            var status = "error";
            try
            {
                accountdto.WIBirthDay = BU.Common.ConvetDate.ParseRequestDate(accountdto.WIBirthdayString);
                if (typeFix == (int)BU.Common.Enum.TypeFix.Add)
                {
                    //register
                    var user = new ApplicationUser { UserName = accountdto.WIUserName, Email = accountdto.WIEmail, PhoneNumber = accountdto.WIPhoneNumber, EmailConfirmed = true };
                    var result = await UserManager.CreateAsync(user, accountdto.WIPassword);
                    if (result.Succeeded)
                    {
                        //Send mail
                        //string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                        //var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                        //await UserManager.SendEmailAsync(user.Id, "Xác nhận tài khoản của bạn", "Vui lòng xác nhận tài khoản của bạn bằng cách nhấp vào <a href=\"" + callbackUrl + "\">đây</a>");

                        //add role
                        switch (accountdto.WIPosition)
                        {
                            case (int)BU.Common.Enum.WIPosition.Admin:
                                {
                                    UserManager.AddToRole(user.Id, "Quản trị");
                                }
                                break;
                            case (int)BU.Common.Enum.WIPosition.ManageNewAndJob:
                                {
                                    UserManager.AddToRole(user.Id, "Quản lý công việc");
                                    UserManager.AddToRole(user.Id, "Quản lý tin tức");
                                }
                                break;
                            case (int)BU.Common.Enum.WIPosition.ManageUser:
                                {
                                    UserManager.AddToRole(user.Id, "Quản lý người tìm việc");
                                    UserManager.AddToRole(user.Id, "Quản lý người tuyển dụng");
                                }
                                break;
                            case (int)BU.Common.Enum.WIPosition.ManageWebsite:
                                {
                                    UserManager.AddToRole(user.Id, "Quản lý quảng cáo");
                                    UserManager.AddToRole(user.Id, "Quản lý thống kê");
                                    UserManager.AddToRole(user.Id, "Quản lý website");
                                }
                                break;
                            default:
                                break;
                        }

                        //insert to table WebmasterInfo
                        accountdto.WI_AspNetUserId = user.Id;
                        accountdto.WIDateStart = DateTime.Now;
                        resultCode = _webMasterManage.Insert(accountdto);
                        status = "success";
                        message = "Tại tài khoản thành công! Hãy kiểm tra email để xác nhận tài khoản";
                    }
                    else
                    {
                        foreach (var e in result.Errors)
                        {
                            message += e.ToString();
                        }
                    }

                }
                else
                {
                    // get user object from the storage
                    var user = await UserManager.FindByIdAsync(accountdto.WI_AspNetUserId);

                    var checkChange = false;

                    //check change email
                    if (!user.Email.Equals(accountdto.WIEmail))
                    {
                        checkChange = true;
                        user.Email = accountdto.WIEmail;
                    }

                    //check change username
                    if (!user.UserName.Equals(accountdto.WIUserName))
                    {
                        checkChange = true;
                        user.UserName = accountdto.WIUserName;
                    }

                    //check change phone
                    if (!(user.PhoneNumber == accountdto.WIPhoneNumber))
                    {
                        checkChange = true;
                        user.PhoneNumber = accountdto.WIPhoneNumber;
                    }
                    // change username and email
                    //if()
                    // Persiste the changes
                    var checkUpdateAsync = true;
                    var update = new IdentityResult();

                    if (checkChange)
                    {
                        update = await UserManager.UpdateAsync(user);
                        if (update.Succeeded)
                        {
                            //check change email
                            if (!user.Email.Equals(accountdto.WIEmail))
                            {
                                // generage email confirmation code
                                var emailConfirmationCode = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                                var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = emailConfirmationCode }, protocol: Request.Url.Scheme);
                                await UserManager.SendEmailAsync(user.Id, "Xác nhận tài khoản của bạn", "Vui lòng xác nhận tài khoản của bạn bằng cách nhấp vào <a href=\"" + callbackUrl + "\">đây</a>");
                            }
                        }
                        else
                        {
                            checkUpdateAsync = false;
                            foreach (var e in update.Errors)
                            {
                                message += e.ToString();
                            }
                        }
                    }
                    if (checkUpdateAsync)
                    {
                        resultCode = _webMasterManage.Update(accountdto);
                        switch (resultCode)
                        {
                            case 1:
                                {
                                    status = "success";
                                    message = "Cập nhật thành công!";
                                }
                                break;
                            default:
                                message = "Đã xảy ra lỗi trong quá trình cập nhật!";
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }

            return Json(new { resultCode = resultCode, message = message, status = status }, JsonRequestBehavior.AllowGet);
        }

        //Change password
        public async Task<ActionResult> ChangePassword(string idUser, string oldPass, string newPass)
        {
            var message = "";
            var status = "error";
            var result = await UserManager.ChangePasswordAsync(idUser, oldPass, newPass);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    message = "Đổi mật khẩu thành công";
                    status = "success";
                }
                else
                {
                    message = "Đổi mật khẩu không thành công";
                }
            }
            else
            {
                message = "Mật khẩu cũ không chính xác";
            }
            return Json(new { message = message, status = status }, JsonRequestBehavior.AllowGet);
        }



        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
            //}
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { message = "Sai định dạng. Hãy nhập lại!", status = "warn" }, JsonRequestBehavior.AllowGet);
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var userFind = UserManager.FindByEmail(model.Email);
            if (userFind == null)
                return Json(new { message = "Email không tồn tại!", status = "error" }, JsonRequestBehavior.AllowGet);
            var result = await SignInManager.PasswordSignInAsync(userFind.UserName, model.Password, model.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    {
                        string messageResult = "", statusResult = "error";
                        var listRoles = new JobHuntEntities().AspNetRoles.ToList();
                        var listRolesName = new List<string>();
                        var rolenames = await UserManager.GetRolesAsync(userFind.Id);
                        foreach (var role in listRoles)
                        {
                            if (role.Id.Equals("candidate") || role.Id.Equals("employer"))
                            {
                                continue;
                            }
                            else
                            {
                                listRolesName.Add(role.Name);
                            }
                        }
                        if (listRolesName.Intersect(rolenames).Any())
                        {
                            var loginUser = await UserManager.FindAsync(userFind.UserName, model.Password);
                            var getUser = _webMasterManage.GetInfoWebmasterByIdUser(loginUser.Id);
                            if (!loginUser.EmailConfirmed)
                            {
                                messageResult = "Hãy kiểm tra email và xác nhận tài khoản!";
                            }
                            else if(getUser.WIStatus != (int)BU.Common.Enum.StatusAccount.Active)
                            {
                                messageResult = "Tài khoản của bạn đã bị khóa hoặc có vấn đề, hãy liên lạc với admin để được hỗ trợ!";
                            }
                            else
                            {
                                messageResult = "Đăng nhập thành công!";
                                statusResult = "success";
                            }
                        }
                        else
                        {
                            messageResult = "Bạn không có quyền truy cập";
                        }

                        //if error then logout
                        if (statusResult.Equals("error"))
                        {
                            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                        }
                        return Json(new { message = messageResult, status = statusResult }, JsonRequestBehavior.AllowGet);

                    }
                case SignInStatus.LockedOut:
                    return Json(new { message = "Tài khoản của bạn đã bị khóa!", status = "error" }, JsonRequestBehavior.AllowGet);
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                default:
                    return Json(new { message = "Sai tên tài khoản hoặc mật khẩu!", status = "error" }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Register(string typeMember, HttpPostedFileBase fileCV, CandidateDTO cdddto, RecruitDTO rcdto)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(typeMember))
                    return Json(new { message = "Hãy chọn người dùng bạn muốn đăng ký!", status = "warn" }, JsonRequestBehavior.AllowGet);
                var role = typeMember.Remove(typeMember.Length - 8);
                var nameRole = role.Equals("candidate") ? "Người tìm việc" : "Người tuyển dụng";
                var user = new ApplicationUser();
                var result = new IdentityResult();
                if (role.Equals("candidate"))
                {
                    user.UserName = cdddto.CddUserName;
                    user.Email = cdddto.CddEmail;
                    user.PhoneNumber = cdddto.CddPhone;
                    user.EmailConfirmed = true;
                    result = await UserManager.CreateAsync(user, cdddto.CddPassword);
                }
                else if (role.Equals("employer"))
                {
                    user.UserName = rcdto.RIUserName;
                    user.Email = rcdto.RIEmail;
                    user.PhoneNumber = rcdto.RIPhone;
                    user.EmailConfirmed = true;
                    result = await UserManager.CreateAsync(user, rcdto.RIPassword);
                }
                
                var error = "";
                if (result.Succeeded)
                {
                    UserManager.AddToRole(user.Id, nameRole);
                    // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    //string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    //var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    //await UserManager.SendEmailAsync(user.Id, "Xác nhận tài khoản của bạn", "Vui lòng xác nhận tài khoản của bạn bằng cách nhấp vào <a href=\"" + callbackUrl + "\">đây</a>");
                    if (role.Equals("candidate"))
                    {
                        cdddto.Cdd_AspNetUserId = user.Id;
                        cdddto.CddRegisterDate = DateTime.Now;
                        cdddto.CPStatus = (int?)BU.Common.Enum.StatusCandidate.Approvaling;
                        cdddto.CddPassword = user.PasswordHash;
                        cdddto.CddPathCV = UploadFile(fileCV);
                        var insert = new CandidateManage().Insert(cdddto);
                    }
                    else if (role.Equals("employer"))
                    {
                        rcdto.RI_AspNetUserId = user.Id;
                        rcdto.RIRegisterDate = DateTime.Now;
                        rcdto.RIStatus = (int?)BU.Common.Enum.StatusRecruit.Approvaling;
                        rcdto.RIPassword = user.PasswordHash;
                        var insertRI = new RecruitManage().Insert(rcdto);
                    }
                    else
                    {
                    }

                    return Json(new { message = "Đăng ký thành công!", status = "success" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    foreach (var e in result.Errors)
                    {
                        error += e.ToString();
                    }
                    return Json(new { message = error, status = "error" }, JsonRequestBehavior.AllowGet);
                }
            }

            // If we got this far, something failed, redisplay form
            return Json(new { message = "Hãy nhập đủ và đúng định dạng!", status = "error" }, JsonRequestBehavior.AllowGet);
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


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("login", "accountmanage");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }
        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}