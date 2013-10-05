using BlogEngine.Core.Infrastructure;
using BlogEngine.Core.Models;
using BlogEngine.Core.ViewModels;
using BlogEngine.Web.Helpers;
using System;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace BlogEngine.Web.Areas.Admin.Controllers
{
    // Inherits the AdminController abstract class which manages security for all controllers 
    public class BlogController : AdminController
    {
        private readonly IBlogRepository _blogRepository;
        private readonly ICategoryRepository _categoryRepository;

        public BlogController(IBlogRepository blogRepository, ICategoryRepository categoryRepository)
        {
            _blogRepository = blogRepository;
            _categoryRepository = categoryRepository;
        }

        //
        // GET: /Admin/Blog/
        public ActionResult Index()
        {
            var blogList = _blogRepository.GetAllViewModel();

            return View(blogList);
        }

        //
        // GET: /Admin/Blog/Create
        public ActionResult Create()
        {
            var model = new BlogViewModel
                {
                   SelectedCategory = _categoryRepository.GetCategoryById(1).ToString(),
                   Categories = _categoryRepository.GetCategoriesForBlogView(), 
                   UserId = WebSecurity.CurrentUserId
                };

            return View(model);
        }

        //
        // POST: /Admin/Blog/Create
        [HttpPost]
        public ActionResult Create(BlogViewModel model)
        {
            _blogRepository.Create(ModelBinder.BlogCreate(model));

            return RedirectToAction("Index");
        }

        //
        // GET: /Admin/Blog/Edit
        public ActionResult Edit(int id)
        {
            var blog = _blogRepository.GetBlogById(id);
            var blogViewModel = ModelBinder.Blog(blog);

            blogViewModel.Categories = _categoryRepository.GetCategoriesForBlogView();
            blogViewModel.UserId = WebSecurity.CurrentUserId;

            return View(blogViewModel);
        }

        //
        // POST: /Admin/Blog/Edit
        [HttpPost]
        public ActionResult Edit(BlogViewModel model)
        {
            if (ModelState.IsValid)
            {
                _blogRepository.Edit(model);

                return RedirectToAction("Index");
            }

            return View(model);
        }

        //
        // GET: /Admin/Blog/Delete
        public ActionResult Delete(int id)
        {
            var model = _blogRepository.GetBlogById(id);

            return View(model);
        }

        //
        // POST: /Admin/Blog/Delete
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirm(int id)
        {
            _blogRepository.Delete(id);

            return RedirectToAction("Index");
        }
    }
}
