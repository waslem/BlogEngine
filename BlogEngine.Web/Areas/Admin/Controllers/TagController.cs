using BlogEngine.Core.Models;
using BlogEngine.Core.Work;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogEngine.Web.Areas.Admin.Controllers
{
    public class TagController : Controller
    {
        private IUnitOfWork _unitOfWork;

        public TagController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //
        // GET: /Admin/Tag/
        public ActionResult Index()
        {
            var tags = _unitOfWork.TagRepository.GetAllTags();
            return View(tags);
        }

        //
        // GET: /Admin/Tag/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Admin/Tag/Create
        [HttpPost]
        public ActionResult Create(Tag tag)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.TagRepository.Create(tag);
                _unitOfWork.Save();

                return RedirectToAction("Index");
            }
            return View(tag);
        }
    }
}
