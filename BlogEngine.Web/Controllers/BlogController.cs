using BlogEngine.Core.Infrastructure;
using BlogEngine.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogEngine.Web.Controllers
{
    public class BlogController : Controller
    {
        private IBlogRepository _blogRepository;

        public BlogController(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }

        //
        // GET: /Blog/
        public ActionResult Index()
        {
            var blogs = _blogRepository.GetAll();
            return View(blogs);
        }
    }
}
