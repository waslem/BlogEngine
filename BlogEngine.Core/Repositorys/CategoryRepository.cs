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
    public class CategoryRepository : ICategoryRepository
    {
        private BlogContext _context;

        public CategoryRepository()
        {
            _context = new BlogContext();
        }

        public List<Category> GetCategories()
        {
            var categories = _context.Categories.ToList();

            return categories;
        }
    }
}
