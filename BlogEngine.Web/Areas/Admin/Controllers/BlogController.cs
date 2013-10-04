using BlogEngine.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogEngine.Web.Areas.Admin.Controllers
{
    // Inherits the AdminController abstract class which manages security for all controllers 
    public class BlogController : AdminController
    {
        //
        // GET: /Admin/Blog/
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Admin/Blog/Create
        public ActionResult Create()
        {
            return View();
        }
    }
}
