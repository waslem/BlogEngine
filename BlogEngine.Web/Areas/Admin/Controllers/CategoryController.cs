using BlogEngine.Core.Infrastructure;
using BlogEngine.Core.ViewModels;
using BlogEngine.Core.Work;
using BlogEngine.Web.Helpers;
using System;
using System.Web.Mvc;

namespace BlogEngine.Web.Areas.Admin.Controllers
{
    public class CategoryController : AdminController
    {

        //
        // GET: /Admin/Category/
        public ActionResult Index()
        {
            return View();
        }
    }
}
