using BlogEngine.Core.Infrastructure;
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

        public BlogController(IBlogRepository blogRepository, ICategoryRepository categoryRepository)
        {
            _blogRepository = blogRepository;
            _categoryRepository = categoryRepository;
        }
        //
        // GET: /Admin/Blog/
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Admin/Blog/Create
        public ActionResult Create()
        {
            var model = new BlogViewModel
                {
                    Category = new SelectList(_categoryRepository.GetCategories(), "CategoryId", "Name")
                };

            return View(model);
        }
    }
}
