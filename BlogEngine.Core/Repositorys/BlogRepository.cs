using BlogEngine.Core.Contexts;
using BlogEngine.Core.Infrastructure;
using BlogEngine.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogEngine.Core.ViewModels;

namespace BlogEngine.Core.Repositorys
{
    public class BlogRepository : IBlogRepository
    {
        private readonly BlogContext _context;

        public BlogRepository()
        {
            _context = new BlogContext();
        }

        public ICollection<Blog> GetAll()
        {
            var blogs = _context.Blogs.ToList();

            return blogs;
        }

        public ICollection<BlogListViewModel> GetAllViewModel()
        {
            var blogs = GetAll();

            return blogs.Select(blog => new BlogListViewModel
                {
                    BlogId = blog.BlogId, 
                    Entry = blog.BlogEntry, 
                    Category = blog.Category.Name,
                    CreatedBy = blog.User.UserName,
                    CreatedDate = blog.DateCreated.ToShortDateString() + "-" + blog.DateCreated.ToShortTimeString(), 
                    CommentCount = blog.Comments.Count()
                }).ToList();
        }

        public void Create(Blog blog)
        {
            _context.Blogs.Add(blog);
            _context.SaveChanges();
        }
    }
}
