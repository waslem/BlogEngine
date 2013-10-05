using BlogEngine.Core.Infrastructure;
using BlogEngine.Core.Models;
using BlogEngine.Core.ViewModels;
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
        private readonly IBlogRepository _blogRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUserRepository _userRepository;

        public BlogController(IBlogRepository blogRepository, ICategoryRepository categoryRepository, IUserRepository userRepository)
        {
            _blogRepository = blogRepository;
            _categoryRepository = categoryRepository;
            _userRepository = userRepository;
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
                   UserId = _userRepository.GetUserId(User.Identity.Name)
                };

            return View(model);
        }

        //
        // POST: /Admin/Blog/Create
        [HttpPost]
        public ActionResult Create(BlogViewModel model)
        {
           
            var blog = new Blog
                {
                    UserId = model.UserId,
                    BlogEntry = model.BlogEntry,
                    DateCreated = DateTime.Now, 
                    CategoryId = Int32.Parse(model.SelectedCategory)
                };

            _blogRepository.Create(blog);

            return RedirectToAction("Index");
        }
    }
}
