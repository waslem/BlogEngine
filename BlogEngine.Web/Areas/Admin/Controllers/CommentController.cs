using BlogEngine.Core.Infrastructure;
using BlogEngine.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using BlogEngine.Core.Models;
using BlogEngine.Core.Work;

namespace BlogEngine.Web.Areas.Admin.Controllers
{
    // Inherits the AdminController abstract class which manages security for all controllers 
    public class CommentController : AdminController
    {
        private UnitOfWork _unitOfWork;

        public CommentController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //
        // GET: /Admin/Comment/
        public ActionResult Index(int? page)
        {
            var comments = _unitOfWork.CommentRepository.GetAll();

            // just doing this for now, would like to add feature of user selected page size.
            int pageSize = 5;

            // if page is null, default to first page
            int pageNumber = (page ?? 1);

            // using paged.mvc here
            return View(comments.ToPagedList(pageNumber, pageSize));
        }

        //
        // GET: /Admin/Edit/1
        public ActionResult Edit(int id)
        {
            var model = _unitOfWork.CommentRepository.GetById(id);
            return View(model);
        }

        //
        // POST: /Admin/Edit/1
        [HttpPost]
        public ActionResult Edit(Comment comment)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.CommentRepository.Edit(comment);
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }

            return View(comment);
        }
    }
}
