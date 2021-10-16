using ClosedXML.Excel;
using JobHunt.BU.DTO;
using JobHunt.BU.Manage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace JobHunt.Areas.Admin.Controllers
{
    [Authorize(Roles = "Quản trị, Quản lý tin tức")]
    public class NewManagementController : Controller
    {
        NewManage newmanage = new NewManage();
        // GET: Admin/NewManagement
        public ActionResult Index()
        {
            var model = newmanage.GetAllNews();
            return View(model);
        }
        public ActionResult CreateNew()
        {
            ViewBag.ListCategories = new SelectList(new NewManage().GetListCategories(),"CategoryId","CName" );
            return View();
        }
        [ValidateInput(false)]
        public JsonResult CUNew(NewDTO ndto, int? typePost)
        {
            var resultMessage = "";
            var resultStatus = "error";
            var resultCode = 0;

            if (string.IsNullOrEmpty(ndto.NTitle) || string.IsNullOrEmpty(ndto.NQuote) || string.IsNullOrEmpty(ndto.NDetail) || string.IsNullOrEmpty(ndto.NAvatar) || ndto.N_CategoryId == null)
            {
                resultMessage = "Hãy nhập đầy đủ thông tin";
            }
            //typePost = 1: create new
            else if (typePost == 1)
            {
                ndto.NPostDate = DateTime.Now;
                ndto.N_WebmasterInfoId = 2;
                var insert = newmanage.Insert(ndto);
                if (insert != null)
                {
                    resultCode = 1;
                    resultMessage = "Tạo thành công";
                    resultStatus = "success";
                    //    string path = Server.MapPath("/Assets/template/SendMailToRegisterTinRao.html");
                    //    ParameterizedThreadStart job = new ParameterizedThreadStart(SendEmail);
                    //    Thread thread = new Thread(job);
                    //    thread.Start(new EmailClient() { TieuDe = insert.NTitle, Path = path, newdto = insert });
                }
                else
                {
                    resultMessage = "Đăng tin thất bại";
                }
            }
            //typePost = 2: update new
            else
            {
                resultCode = newmanage.Update(ndto);
                if (resultCode == 1)
                {
                    resultMessage = "Sửa thành công";
                    resultStatus = "success";
                }
                else
                {
                    resultMessage = "Sửa tin thất bại";
                }
            }
            return Json(new { code = resultCode, message = resultMessage, status = resultStatus }, JsonRequestBehavior.AllowGet);
        }

        class EmailClient
        {
            public string TieuDe { get; set; }
            public string Hinhthuc { get; set; }
            public string Path { get; set; }
            public int? IdPro { get; set; }
            public NewDTO newdto { get; set; }
        }
        // string tieuDe, int codeInsert, string hinhthuc
        static void SendEmail(object obj)
        {
            var job = obj as EmailClient;
            var getListRegisters = new SignUpNewsletterManage().GetListSignUpByType(1, job.IdPro);
            foreach (var j in getListRegisters)
            {
                SendMailToRegister(job.Path, j.Name, "tin tức", "http://103.237.147.20:333/tin-tuc/" + BU.Common.GenerateUrl.GenerateSlug(job.newdto.NTitle, job.newdto.NewsId), j.Email);
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


        //Delete new
        public JsonResult DeleteNew(int? idNew)
        {
            var resultCode = 0;
            var resultMessage = "";
            var resultStatus = "error";
            if (idNew == null || idNew == 0)
            {
                resultMessage = "Mã tin tức không được để trống";
            }
            else
            {
                resultCode = newmanage.Delete((int)idNew);
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


        public ActionResult DetailNew(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var model = newmanage.GetBlogById((int)id);
            if (model == null)
            {
                return HttpNotFound();
            }
            ViewBag.ListCategories = new SelectList(newmanage.GetListCategories(), "CategoryId", "CName");
            return View(model);
        }
        // Upload Image
        [HttpPost]
        public string UploadFileImageSumernote(HttpPostedFileBase file)
        {
            var pic = System.Web.HttpContext.Current.Request.Files["file"];
            // Validate
            // Xử lý upload lưu vào sv
            file.SaveAs(Server.MapPath("~/Assets/admin/upload/summernoteimages/" + file.FileName));
            // Trả về link ảnh
            return "/Assets/admin/upload/summernoteimages/" + file.FileName;
        }

        // Upload Image
        [HttpPost]
        public string UploadFileImageNew(HttpPostedFileBase file)
        {
            var pic = System.Web.HttpContext.Current.Request.Files["file"];
            // Validate
            // Xử lý upload lưu vào sv
            file.SaveAs(Server.MapPath("~/Assets/admin/upload/newimages/" + file.FileName));
            // Trả về link ảnh
            return "/Assets/admin/upload/newimages/" + file.FileName;
        }

        //export to excel
        [HttpPost]
        public FileResult ExportToExcel(string keyWord, int? Status)
        {
            //Sheet Tổng Quan
            DataTable dtIndex = new DataTable("DanhSachTinTuc");

            dtIndex.Columns.AddRange(new DataColumn[6] {new DataColumn("STT"),
                                                    new DataColumn("Ảnh đại diện"),
                                                    new DataColumn("Tiêu đề"),
                                                    new DataColumn("Nội dung ngắn gọn"),
                                                    new DataColumn("Trạng thái"),
                                                    new DataColumn("Thể loại"),
                                                   });

            var model = newmanage.GetListNewsBySearch(keyWord, Status);

            var stt = 0;

            foreach (var newdto in model)
            {
                stt++;
                Image image1 = Image.FromFile(Server.MapPath("~" + newdto.NAvatar));

                // Draw the image.
                dtIndex.Rows.Add(stt, newdto.NAvatar, newdto.NTitle, newdto.NQuote, newdto.NameStatus, newdto.NameType);
            }


            using (XLWorkbook wb = new XLWorkbook())
            {
                var ws = wb.Worksheets.Add(dtIndex);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "DanhSachTinTuc.xlsx");
                }
            }
           
        }

    }
}