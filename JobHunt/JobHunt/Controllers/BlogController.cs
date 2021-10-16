using JobHunt.BU.Manage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace JobHunt.Controllers
{
    public class BlogController : Controller
    {
        NewManage newmanage = new NewManage();
        // GET: Blog
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ListBlogs(int page = 1, int pageSize = 5)
        {
            var model = newmanage.GetAllNewsPaging(page, pageSize);
            return View(model);
        }

        public ActionResult DetailBlog(int? id)
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
            return View(model);
        }
    }
}