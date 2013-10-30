using System;
using System.Linq;
using System.Web.Mvc;
using BlogEngine.Core.Work;
using BlogEngine.Core.ViewModels;
using BlogEngine.Web.Helpers;
using WebMatrix.WebData;
using PagedList;
using System.Collections.Generic;
using System.Web;

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
        public ActionResult Index(int? page)
        {
            var blogs = _unitOfWork.BlogRepository.GetAllView();

            int pageSize = 5;
            int pageNumber = (page ?? 1);

            return View(blogs.ToPagedList(pageNumber, pageSize));
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

        [HttpGet]
        public ActionResult GetTopBlogs()
        {
            var blogs = _unitOfWork.BlogRepository.GetTopBlogs(5);
            ICollection<BlogSummaryView> blogSummaryList = ModelBinder.BlogSummary(blogs);

            return PartialView("_getTopBlogs", blogSummaryList);
        }

        //
        // GET: /category/{category}
        public ActionResult Category(string category)
        {
            var blogs = _unitOfWork.BlogRepository.GetBlogsByCategory(category).ToList();

            List<BlogEntryView> blogsView = ModelBinder.BlogEntryView(blogs).ToList();

            if (blogsView.Count < 1)
            {
                throw new HttpException(404, "Category not found");
            }

            return View(blogsView);
        }

        //
        // GET: /Tag/{tag}
        public ActionResult Tag(string tag)
        {
            var blogs = _unitOfWork.BlogRepository.GetBlogsByTag(tag).ToList();

            List<BlogEntryView> blogsView = ModelBinder.BlogEntryView(blogs).ToList();

            if (blogsView.Count < 1)
            {
                throw new HttpException(404, "Category not found");
            }

            return View(blogsView);
        }

        public ActionResult GetCategories()
        {
            var categories = _unitOfWork.CategoryRepository.GetCategories();
            List<CategoryViewModel> categoryViewModel = ModelBinder.Categories(categories);

            return PartialView("_getCategories", categoryViewModel);
        }

        public ActionResult GetTags()
        {
            List<TagCheckViewModel> tags = _unitOfWork.TagRepository.GetAllTagsForVM();

            return PartialView("_getTags", tags);
        }
    }
}
