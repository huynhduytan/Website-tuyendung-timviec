using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace JobHunt
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


            //Nhà tuyển dụng
            #region Recruit
            //Hồ sơ công ty
            //Recruit/ProfileRecruit
            routes.MapRoute(
            name: "ProfileRecruit",
            url: "nha-tuyen-dung/ho-so-cong-ty",
            defaults: new { controller = "Recruit", action = "ProfileRecruit", id = UrlParameter.Optional }
        );

            //Danh sách công việc
            //Recruit/ListJobsPost
            routes.MapRoute(
            name: "ListJobsPost",
            url: "nha-tuyen-dung/danh-sach-cong-viec",
            defaults: new { controller = "Recruit", action = "ListJobsPost", id = UrlParameter.Optional }
        );

            //Danh sách ứng viên
            //Recruit/ListCandidatesApplied
            routes.MapRoute(
            name: "ListCandidatesApplied",
            url: "nha-tuyen-dung/danh-sach-ung-vien",
            defaults: new { controller = "Recruit", action = "ListCandidatesApplied", id = UrlParameter.Optional }
        );

            //Đăng tin tuyển dụng
            //Recruit/PostJob
            routes.MapRoute(
            name: "PostJob",
            url: "nha-tuyen-dung/tin-tuyen-dung/dang-tin",
            defaults: new { controller = "Recruit", action = "PostJob", id = UrlParameter.Optional }
        );



            //Chỉnh sửa tài khoản
            //Recruit/UpdateAccount
            routes.MapRoute(
            name: "UpdateAccount",
            url: "nha-tuyen-dung/chinh-sua-tai-khoan",
            defaults: new { controller = "Recruit", action = "UpdateAccount", id = UrlParameter.Optional }
        );

            //Đổi mật khẩu
            //Recruit/ChangePassword
            routes.MapRoute(
            name: "ChangePassword",
            url: "nha-tuyen-dung/doi-mat-khau",
            defaults: new { controller = "Recruit", action = "ChangePassword", id = UrlParameter.Optional }
        );

            //Xem chi tiết nhà tuyển dụng
            routes.Add("DetailRecruit", new SeoFriendlyRoute("nha-tuyen-dung/{id}",
                new RouteValueDictionary(new { controller = "Recruit", action = "DetailRecruit" }),
                new MvcRouteHandler()));
            #endregion


            //Thành viên
            #region Candidate

            //Quản lý hồ sơ
            //Candidate/CandidateUploadCV
            routes.MapRoute(
            name: "CandidateUploadCV",
            url: "ung-vien/ho-so-dinh-kem",
            defaults: new { controller = "Candidate", action = "CandidateUploadCV", id = UrlParameter.Optional }
        );

            //Quản lý hồ sơ
            //Candidate/CandidateProfile
            routes.MapRoute(
            name: "CandidateProfile",
            url: "ung-vien/ho-so-ca-nhan",
            defaults: new { controller = "Candidate", action = "CandidateProfile", id = UrlParameter.Optional }
        );


            //Những công việc đã ứng tuyển
            //Candidate/AppliedJobs
            routes.MapRoute(
            name: "AppliedJobs",
            url: "ung-vien/cong-viec-da-ung-tuyen",
            defaults: new { controller = "Candidate", action = "AppliedJobs", id = UrlParameter.Optional }
        );

            //Những công việc đã lưu
            //Candidate/SaveJobs
            routes.MapRoute(
            name: "SaveJobs",
            url: "ung-vien/cong-viec-da-luu",
            defaults: new { controller = "Candidate", action = "SaveJobs", id = UrlParameter.Optional }
        );

            //Chỉnh sửa tài khoản
            //Candidate/UpdateAccount
            routes.MapRoute(
            name: "UpdateAccountCandidate",
            url: "ung-vien/chinh-sua-tai-khoan",
            defaults: new { controller = "Candidate", action = "UpdateAccount", id = UrlParameter.Optional }
        );

            //Đổi mật khẩu
            //Candidate/ChangePassword
            routes.MapRoute(
            name: "ChangePasswordCandidate",
            url: "ung-vien/doi-mat-khau",
            defaults: new { controller = "Candidate", action = "ChangePassword", id = UrlParameter.Optional }
        );

            //Xem chi tiết ứng viên
            routes.Add("DetailCandidate", new SeoFriendlyRoute("ung-vien/{id}",
                new RouteValueDictionary(new { controller = "Candidate", action = "DetailCandidate" }),
                new MvcRouteHandler()));

            //Xem chi tiết ứng viên để download
            routes.Add("ShowProfile", new SeoFriendlyRoute("download/{id}",
                new RouteValueDictionary(new { controller = "Candidate", action = "ShowProfile" }),
                new MvcRouteHandler()));

            // download CV
            routes.Add("PrintPartialViewToPdf", new SeoFriendlyRoute("downloadcv/{id}",
                new RouteValueDictionary(new { controller = "Candidate", action = "PrintPartialViewToPdf" }),
                new MvcRouteHandler()));
            #endregion

            //Công việc
            #region
            //Chỉnh sửa đăng tin tuyển dụng
            //RecruitJob/UpdateJob
            routes.Add("UpdateJob", new SeoFriendlyRoute("nha-tuyen-dung/tin-tuyen-dung/chinh-sua/{id}",
    new RouteValueDictionary(new { controller = "RecruitJob", action = "UpdateJob" }),
    new MvcRouteHandler()));
            #endregion

            //Tất cả danh mục việc làm
            //Home/ShowListCategories
            routes.MapRoute(
            name: "ShowListCategories",
            url: "danh-muc-viec-lam",
            defaults: new { controller = "Home", action = "ShowListCategories", id = UrlParameter.Optional }
        );


            //Danh sách các công ty
            //Recruit/ListRecruits
            routes.MapRoute(
            name: "ListRecruits",
            url: "danh-sach-cong-ty",
            defaults: new { controller = "Recruit", action = "ListRecruits", id = UrlParameter.Optional }
        );

            //Danh sách bài viết
            //Blog/ListBlogs
            routes.MapRoute(
            name: "ListBlogs",
            url: "blogs/danh-sach-bai-viet",
            defaults: new { controller = "Blog", action = "ListBlogs", id = UrlParameter.Optional }
        );

            //Danh sách người tìm việc
            //Candidate/ListCandidates
            routes.MapRoute(
            name: "ListCandidates",
            url: "danh-sach-ung-vien",
            defaults: new { controller = "Candidate", action = "ListCandidates", id = UrlParameter.Optional }
        );


            //Chi tiết bài đăng tuyển dụng công việc
            routes.Add("DetailJob", new SeoFriendlyRoute("tuyen-dung/chi-tiet-viec-lam/{id}",
                new RouteValueDictionary(new { controller = "RecruitJob", action = "DetailJob" }),
                new MvcRouteHandler()));

            //Danh sách toàn bộ việc làm
            //RecruitJob / ListJobs
            routes.MapRoute(
            name: "ListJobs",
            url: "tuyen-dung/viec-lam",
            defaults: new { controller = "RecruitJob", action = "ListJobs", id = UrlParameter.Optional }
        );

            //Tin tức
            //Xem chi tiết nhà tuyển dụng
            routes.Add("DetailBlog", new SeoFriendlyRoute("tin-tuc/{id}",
                new RouteValueDictionary(new { controller = "Blog", action = "DetailBlog" }),
                new MvcRouteHandler()));

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
