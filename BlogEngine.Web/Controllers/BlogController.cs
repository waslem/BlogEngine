using System;
using System.Linq;
using System.Web.Mvc;
using BlogEngine.Core.Work;
using BlogEngine.Core.ViewModels;
using BlogEngine.Web.Helpers;
using WebMatrix.WebData;

namespace BlogEngine.Web.Controllers
{
    public class BlogController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public BlogController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //
        // GET: /Blog/
        public ActionResult Index()
        {
            var blogs = _unitOfWork.BlogRepository.GetAllView();
            return View(blogs);
        }

        //
        // GET: /Blog/Comment/1
        public ActionResult Comment(int id, int? parent)
        {
            // if parent not null, create new comment with parentid == parent
            var model = new CommentViewModel
                {
                    UserId = WebSecurity.CurrentUserId,
                    BlogId = id, 
                    ParentId = parent
                };

            return View(model);
        }

        //
        // POST: /Blog/Comment/1
        [HttpPost]
        public ActionResult Comment(CommentViewModel comment)
        {
            if (ModelState.IsValid)
            {
                comment.CommentDate = DateTime.Now;
                _unitOfWork.CommentRepository.Create(comment);
                _unitOfWork.Save();

                return RedirectToAction("Index");
            }
            return View(comment);
        }

        //
        // GET: /Blog/BlogEntry/1/{blogtitle}
        public ActionResult BlogEntry(int id, string blogEntryName)
        {
            var model = _unitOfWork.BlogRepository.GetBlogById(id);

            ViewBag.totalComments = model.Comments.Count();

            // just get the base comments (where there is no ParentId, and then iterate through them in the view
            var comments = model.Comments.Where(c => c.ParentId == null).ToList();
            model.Comments = comments;

            string realTitle = UrlEncoder.ToFriendlyUrl(model.BlogTitle);
            string urlTitle = (blogEntryName ?? "").Trim().ToLower();

            if (realTitle != urlTitle)
            {
                string url = "/BlogEntry/" + model.BlogEntryId + "/" + realTitle;
                return new RedirectResult(url);
            }

            return View(model);
        }
    }
}
