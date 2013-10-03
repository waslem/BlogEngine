using BlogEngine.Core.Contexts;
using BlogEngine.Core.Infrastructure;
using BlogEngine.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogEngine.Core.Repositorys
{
    public class BlogRepository : IBlogRepository
    {
        private BlogContext _context;

        public BlogRepository()
        {
            _context = new BlogContext();
        }

        public ICollection<Blog> GetAll()
        {
            var blogs = _context.Blogs.ToList();

            return blogs;
        }
    }
}
