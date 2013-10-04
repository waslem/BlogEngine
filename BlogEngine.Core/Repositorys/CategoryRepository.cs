using BlogEngine.Core.Contexts;
using BlogEngine.Core.Infrastructure;
using BlogEngine.Core.Models;
using BlogEngine.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
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

        public bool Create(CategoryViewModel model)
        {
            // if the category name does not exist in the database
            if (!GetCategories().Any(c => c.Name.Contains(model.Name)))
            {
                var category = new Category
                {
                    Name = model.Name,
                    Description = model.Description,
                    CreatedDate = model.CreatedDate
                };

                _context.Categories.Add(category);
                _context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public Category GetCategoryById(int id)
        {
            var category = _context.Categories.Find(id);

            return category;
        }

        public void EditCategory(Category model)
        {
            _context.Entry(model).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void EditCategory(CategoryViewModel model)
        {
            var category = GetCategoryById(model.CategoryId);

            category.Description = model.Description;
            category.Name = model.Name;

            _context.Entry(category).State = EntityState.Modified;
            _context.SaveChanges();
        }


        public bool Delete(int id)
        {
            var category = new Category { CategoryId = id };

            _context.Entry(category).State = EntityState.Deleted;
            _context.SaveChanges();

            return true;
        }
    }
}
