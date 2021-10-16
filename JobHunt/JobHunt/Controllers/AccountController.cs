using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using JobHunt.Models;
using JobHunt.BU.DTO;
using JobHunt.BU.Manage;
using System.Web.Security;

namespace JobHunt.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        CandidateManage candidateManage = new CandidateManage();
        RecruitManage recruitManage = new RecruitManage();
        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
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

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            //var userId = User.Identity.GetUserId();
            //if (string.IsNullOrEmpty(userId))
            //{
            //    return RedirectToLocal(returnUrl);
            //}
            //else
            //{
            if (!string.IsNullOrEmpty(returnUrl) && returnUrl.ToLower().Contains("admin"))
            {
                return RedirectToAction("Login", "AccountManage", new { area = "Admin", returnUrl = returnUrl });
            }
            ViewBag.ReturnUrl = returnUrl;
            return View();
            //}
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(string type, LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { message = "Sai định dạng. Hãy nhập lại!", status = "warn" }, JsonRequestBehavior.AllowGet);
            }
            if (string.IsNullOrEmpty(type))
                return Json(new { message = "Hãy chọn người dùng bạn muốn đăng nhập!", status = "warn" }, JsonRequestBehavior.AllowGet);
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
                        var role = type.Remove(type.Length - 5);
                        var nameRole = role.Equals("candidate") ? "Người tìm việc" : "Người tuyển dụng";
                        if (UserManager.IsInRole(userFind.Id, nameRole))
                        {
                            var loginUser = await UserManager.FindAsync(userFind.UserName, model.Password);
                            if (!loginUser.EmailConfirmed)
                            {
                                messageResult = "Hãy kiểm tra email và xác nhận tài khoản!";
                                statusResult = "error";
                                AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                            }
                            else
                            {
                                messageResult = "Đăng nhập thành công!";
                                statusResult = "success";
                            }
                        }
                        else
                        {
                            messageResult = "Tài khoản hoặc mật khẩu không chính xác";
                            statusResult = "error";
                            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                        }
                        return Json(new { message = messageResult, status = statusResult }, JsonRequestBehavior.AllowGet);

                    }
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                default:
                    return Json(new { message = "Sai tên tài khoản hoặc mật khẩu!", status = "error" }, JsonRequestBehavior.AllowGet);
            }
        }

        //
        // GET: /Account/VerifyCode
        [AllowAnonymous]
        public async Task<ActionResult> VerifyCode(string provider, string returnUrl, bool rememberMe)
        {
            // Require that the user has already logged in via username/password or external login
            if (!await SignInManager.HasBeenVerifiedAsync())
            {
                return View("Error");
            }
            return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/VerifyCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyCode(VerifyCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // The following code protects for brute force attacks against the two factor codes. 
            // If a user enters incorrect codes for a specified amount of time then the user account 
            // will be locked out for a specified amount of time. 
            // You can configure the account lockout settings in IdentityConfig
            var result = await SignInManager.TwoFactorSignInAsync(model.Provider, model.Code, isPersistent: model.RememberMe, rememberBrowser: model.RememberBrowser);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(model.ReturnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid code.");
                    return View(model);
            }
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(string type, RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(type))
                    return Json(new { message = "Hãy chọn người dùng bạn muốn đăng ký!", status = "warn" }, JsonRequestBehavior.AllowGet);
                var role = type.Remove(type.Length - 8);
                var nameRole = role.Equals("candidate") ? "Người tìm việc" : "Người tuyển dụng";
                var user = new ApplicationUser { UserName = model.UserName, Email = model.Email, PhoneNumber = model.Phone, EmailConfirmed = true };
                var result = await UserManager.CreateAsync(user, model.Password);
                var error = "";
                if (result.Succeeded)
                {
                    UserManager.AddToRole(user.Id, nameRole);
                    // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    //string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    //var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    //await UserManager.SendEmailAsync(user.Id, "Xác nhận tài khoản của bạn", "Vui lòng xác nhận tài khoản của bạn bằng cách nhấp vào <a href=\"" + callbackUrl + "\">đây</a>");
                    var checkUser = UserManager.FindByEmail(model.Email);
                    if (role.Equals("candidate"))
                    {
                        var candidateDTO = new CandidateDTO()
                        {
                            CddEmail = model.Email,
                            CddUserName = model.UserName,
                            Cdd_AspNetUserId = user.Id,
                            CPAvatar = "/Assets/client/images/avatar-default.png",
                            CddRegisterDate = DateTime.Now,
                            CPStatus = (int?)BU.Common.Enum.StatusCandidate.Approvaling,
                            CddPhone = model.Phone,
                            CddPassword = checkUser.PasswordHash
                        };

                        var insert = new CandidateManage().Insert(candidateDTO);
                    }
                    else if (role.Equals("employer"))
                    {
                        var recruitDTO = new RecruitDTO()
                        {
                            RIEmail = model.Email,
                            RIUserName = model.UserName,
                            RI_AspNetUserId = user.Id,
                            RIAvatar = "/Assets/client/images/avatar-default.png",
                            RICoverImage = "/Assets/client/images/coverimage.jpg",
                            RIRegisterDate = DateTime.Now,
                            RIStatus = (int?)BU.Common.Enum.StatusRecruit.Approvaling,
                            RIPhone = model.Phone,
                            RIPassword = checkUser.PasswordHash
                        };
                        var insertRI = new RecruitManage().Insert(recruitDTO);
                    }
                    else
                    {
                    }

                    return Json(new { message = "Đăng ký thành công! Xin mời đăng nhập!", status = "success" }, JsonRequestBehavior.AllowGet);
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

        //
        // GET: /Account/ConfirmEmail
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error404");
            }
            var result = await UserManager.ConfirmEmailAsync(userId, code);
            if (result.Succeeded)
            {
                var CheckCandidate = candidateManage.GetCandidateInfoByIdAspNetUser(userId);
                var CheckRecruit = recruitManage.GetRecruitInfoByIdAspNetUser(userId);
                if (CheckCandidate != null)
                {
                    candidateManage.UpdateStatusCandidate((int?)BU.Common.Enum.StatusCandidate.Active, CheckCandidate.CandidateId);
                }
                else if (CheckRecruit != null)
                {
                    recruitManage.UpdateStatusRecruit((int?)BU.Common.Enum.StatusRecruit.Active, CheckRecruit.RecruitId);
                }
            }
            return View(result.Succeeded ? "ConfirmEmail" : "Error404");
        }

        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(model.Email);
                if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return View("ForgotPasswordConfirmation");
                }

                // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                // string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                // var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);		
                // await UserManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");
                // return RedirectToAction("ForgotPasswordConfirmation", "Account");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        //
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View();
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await UserManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            AddErrors(result);
            return View();
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/SendCode
        [AllowAnonymous]
        public async Task<ActionResult> SendCode(string returnUrl, bool rememberMe)
        {
            var userId = await SignInManager.GetVerifiedUserIdAsync();
            if (userId == null)
            {
                return View("Error");
            }
            var userFactors = await UserManager.GetValidTwoFactorProvidersAsync(userId);
            var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
            return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/SendCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendCode(SendCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            // Generate the token and send it
            if (!await SignInManager.SendTwoFactorCodeAsync(model.SelectedProvider))
            {
                return View("Error");
            }
            return RedirectToAction("VerifyCode", new { Provider = model.SelectedProvider, ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // Sign in the user with this external login provider if the user already has a login
            var result = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });
                case SignInStatus.Failure:
                default:
                    // If the user does not have an account, then prompt the user to create an account
                    ViewBag.ReturnUrl = returnUrl;
                    ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                    return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
            }
        }

        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Manage");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
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