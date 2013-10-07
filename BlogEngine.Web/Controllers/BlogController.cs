﻿using BlogEngine.Core.Infrastructure;
using BlogEngine.Core.Models;
using BlogEngine.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace BlogEngine.Web.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogRepository _blogRepository;

        public BlogController(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }

        //
        // GET: /Blog/
        public ActionResult Index()
        {
            //var blogs = _blogRepository.GetAll();
            var blogs = _blogRepository.GetAllView();
            
            return View(blogs);
        }

        //
        // GET: /Blog/Comment/1
        public ActionResult Comment(int id)
        {
            var model = new CommentViewModel
                {
                    UserId = WebSecurity.CurrentUserId,
                    BlogId = id
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
                _blogRepository.AddComment(comment);

                return RedirectToAction("Index");
            }

            return View(comment);
        }
    }
}
