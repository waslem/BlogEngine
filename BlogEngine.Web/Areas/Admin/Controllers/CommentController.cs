using BlogEngine.Core.Infrastructure;
using BlogEngine.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace BlogEngine.Web.Areas.Admin.Controllers
{
    // Inherits the AdminController abstract class which manages security for all controllers 
    public class CommentController : AdminController
    {
        private ICommentRepository _commentRepository;

        public CommentController(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        //
        // GET: /Admin/Comment/
        public ActionResult Index(int? page)
        {

            var comments = _commentRepository.GetAll();

            int pageSize = 5;

            // if page is null, default to page 1
            int pageNumber = (page ?? 1);

            // using paged.mvc here
            return View(comments.ToPagedList(pageNumber, pageSize));
        }

    }
}
