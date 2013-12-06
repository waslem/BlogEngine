using BlogEngine.Core.Infrastructure;
using BlogEngine.Core.ViewModels;
using BlogEngine.Core.Work;
using BlogEngine.Web.Helpers;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace BlogEngine.Web.Areas.Admin.Controllers
{
    // Inherits the AdminController abstract class which manages security for all controllers 
    public class BlogController : AdminController
    {
        private readonly IUnitOfWork _unitOfWork;

        public BlogController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //
        // GET: /Admin/Blog/
        public ActionResult Index()
        {
            var blogList = _unitOfWork.BlogRepository.GetAllViewModel();
            return View(blogList);
        }

        //
        // GET: /Admin/Blog/Create
        public ActionResult Create()
        {
            var model = new BlogViewModel
            {
                Tags = _unitOfWork.TagRepository.GetAllTagsForVM(),
                SelectedCategory = _unitOfWork.CategoryRepository.GetCategoryById(1).ToString(),
                Categories = _unitOfWork.CategoryRepository.GetCategoriesForBlogView(),
                UserId = WebSecurity.CurrentUserId
            };

            return View(model);
        }

        //
        // POST: /Admin/Blog/Create
        [HttpPost]
        public ActionResult Create(BlogViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.BlogImage != null)
                {
                    model.BlogImage.SaveAs(HttpContext.Server.MapPath("~/Images/" + model.BlogImage.FileName));
                }
                _unitOfWork.BlogRepository.Create(ModelBinder.BlogCreate(model));
                _unitOfWork.Save();

                return RedirectToAction("Index", "Blog");
            }

            model.Tags = _unitOfWork.TagRepository.GetAllTagsForVM();
            //todo better way of doing this
            model.SelectedCategory = _unitOfWork.CategoryRepository.GetCategoryById(1).ToString();
            model.Categories = _unitOfWork.CategoryRepository.GetCategoriesForBlogView();
            model.UserId = WebSecurity.CurrentUserId;

            return View(model);
        }

        //
        // GET: /Admin/Blog/Edit
        public ActionResult Edit(int id)
        {
            var blog = _unitOfWork.BlogRepository.GetBlogById(id);
            BlogViewModel blogViewModel = ModelBinder.Blog(blog);

            blogViewModel.Categories = _unitOfWork.CategoryRepository.GetCategoriesForBlogView();
            blogViewModel.UserId = WebSecurity.CurrentUserId;

            blogViewModel.Tags = _unitOfWork.TagRepository.GetTagsSelectedForVM(blogViewModel.BlogId);

            return View(blogViewModel);
        }

        //
        // POST: /Admin/Blog/Edit
        [HttpPost]
        public ActionResult Edit(BlogViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.BlogImage != null)
                {
                    model.BlogImage.SaveAs(HttpContext.Server.MapPath("~/Images/" + model.BlogImage.FileName));
                }
                _unitOfWork.BlogRepository.Edit(model);
                _unitOfWork.Save();

                return RedirectToAction("Index");
            }

            return View(model);
        }

        //
        // POST: /Admin/Blog/Delete
        [HttpPost]
        public ActionResult Delete(int id)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.BlogRepository.Delete(id);
                _unitOfWork.Save();

                return RedirectToAction("Index");
            }

            return HttpNotFound();
        }

        public ActionResult Details(int id)
        {
            var model = _unitOfWork.BlogRepository.GetBlogById(id);
            return View(model);
        }

        [HttpGet]
        public ActionResult GetBlogInfo(int id)
        {
            var blog = _unitOfWork.BlogRepository.GetBlogById(id);
            BlogViewModel blogViewModel = ModelBinder.Blog(blog);

            return PartialView("_modalBlogDetails", blogViewModel);
        }
    }
}
