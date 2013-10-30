using BlogEngine.Core.ViewModels;
using BlogEngine.Core.Work;
using BlogEngine.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogEngine.Web.Controllers
{
    public class CategoryController : Controller
    {
        private IUnitOfWork _unit;

        public CategoryController(IUnitOfWork unit)
        {
            _unit = unit;
        }

        //
        // GET: /Category/
        public ActionResult Index()
        {
            return View();
        }
    }
}
