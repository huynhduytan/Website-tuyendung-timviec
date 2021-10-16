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
    [Authorize(Roles = "Quản trị, Quản lý quảng cáo")]
    public class AdvertisementController : Controller
    {
        AdvertisementManage adManage = new AdvertisementManage();
        // GET: Admin/Advertisement
        public ActionResult Index()
        {
            var model = adManage.GetAll();
            return View(model);
        }
        // Search ngay
     /*   public ActionResult SetDate()
        {
            var range = adManage.CheckExistAd();
            return View(range);
        }*/
        //Book quang cao
        public ActionResult BookAdvertisement()
        {
            return View();
        }
        public ActionResult DetailAdvertisement(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var model = adManage.GetAdvertisementByID((int)id);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }
        //Bookkkkkkkkkkkkkkkkkkkk\
        public JsonResult UpdateAdvertisement(AdvertisementDTO addto, int typebook)
        {
            var resultStatus = 0;
            var message = "";
            var status = "false";
            try
            {
                addto.AdStartDate = (DateTime)BU.Common.ConvetDate.ParseRequestDate(addto.AdStartDateString);
                addto.AdEndDate = (DateTime)BU.Common.ConvetDate.ParseRequestDate(addto.AdEndDateString);
                if (!adManage.CheckExistAd(addto))
                {
                    if (typebook == (int)BU.Common.Enum.TypeFix.Add)
                    {
                        resultStatus = adManage.Insert(addto);
                        message = "Thêm thành công";
                        status = "success";
                    }
                    else
                    {
                        resultStatus = adManage.Update(addto);
                        switch (resultStatus)
                        {
                            case 1:
                                {
                                    message = "Sửa thành công";
                                    status = "success";
                                }
                                break;
                            default:
                                message = "Đã xảy ra lỗi trong quá trình sửa";
                                break;
                        }
                    }
                }
                else
                {
                    message = "Đã tồn tại quảng cáo trong thời gian này rồi. Hãy chọn thời gian khác";
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
                resultStatus = 0;
            }
            return Json(new { resultStatus = resultStatus, status = status, message = message }, JsonRequestBehavior.AllowGet);
        }
        // xóa quang cao
        public JsonResult DeleteAdvertisement(int? idad )
        {
            var resultCode = 0;
            var resultMessage = "";
            var resultStatus = "error";
            if(idad==null || idad == 0)
            {
                resultMessage = "Quảng cáo không được để trống";
            }
            else
            {
                resultCode = adManage.Delete((int)idad);
                if (resultCode == 1)
                {
                    resultMessage = "Xóa thành công";
                    resultStatus = "Success";

                }
                else
                {
                    resultMessage = "Đã xảy ra lỗi";
                }
            }
            return Json(new { message= resultMessage, status = resultStatus, code = resultCode},JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public FileResult ExportToExcel(string keyWord, int? Status)
        {
            // Sheet Tổng Quan
            DataTable dtIndex = new DataTable("DanhSachTK");

            dtIndex.Columns.AddRange(new DataColumn[8] {new DataColumn("STT"),
                                                    new DataColumn("Hình ảnh"),
                                                    new DataColumn("Vị trí đặt"),
                                                    new DataColumn("TG bắt đầu"),
                                                    new DataColumn("TG kết thúc"),
                                                    new DataColumn("Bên quảng cáo"),
                                                    new DataColumn("Sdt"),
                                                    new DataColumn("Trạng thái")
                                                   });

            var model = adManage.GetListAdvertisementsBySearch(keyWord, Status);
            var stt = 0;
            foreach (var ad in model)
            {
                stt++;
                dtIndex.Rows.Add(stt, ad.AdImage, "Slider " + ad.AdPosition, ad.AdStartDateString, ad.AdEndDateString, ad.AdName, ad.AdPhone, ad.nameStatus);
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
    }
}
