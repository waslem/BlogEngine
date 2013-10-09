using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BlogEngine.Core.Infrastructure;
using BlogEngine.Core.Models;
using WebMatrix.WebData;

namespace BlogEngine.Web.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentRepository _commentRepo;

        public CommentController(ICommentRepository commentRepo)
        {
            _commentRepo = commentRepo;
        }

        //
        // GET: /Comment/
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Comment/Edit/1
        public ActionResult Edit(int id)
        {
            var model = _commentRepo.GetById(id);

            return View(model);
        }

        //
        // POST: /Comment/Edit/1
        [HttpPost]
        public ActionResult Edit(Comment comment)
        {
            if (ModelState.IsValid)
            {
                if(WebSecurity.CurrentUserId == comment.UserId)
                {
                    _commentRepo.Edit(comment);
                    return RedirectToAction("Index", "Blog");
                }
                // error here because you can only edit your own comments
                return RedirectToAction("Index", "Blog");
            }

            return View(comment);
        }
    }
}
