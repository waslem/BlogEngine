using System.Web.Mvc;
using BlogEngine.Core.Contexts;
using BlogEngine.Core.Infrastructure;
using BlogEngine.Core.Models;
using BlogEngine.Core.ViewModels;
using System.Collections.Generic;
using System.Data;
using System.Linq;


namespace BlogEngine.Core.Repositorys
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly BlogContext _context;

        public CategoryRepository(BlogContext context)
        {
            _context = context;
        }

        public List<Category> GetCategories()
        {
            var categories = _context.Categories.ToList();

            return categories;
        }

        public bool Create(CategoryViewModel model)
        {
            bool success = true;
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
            }
            else
            {
                success = false;
            }

            return success;
        }

        public Category GetCategoryById(int id)
        {
            var category = _context.Categories.Find(id);
            return category;
        }

        public void EditCategory(Category model)
        {
            _context.Entry(model).State = EntityState.Modified;
        }

        public void EditCategory(CategoryViewModel model)
        {
            var category = GetCategoryById(model.CategoryId);

            category.Description = model.Description;
            category.Name = model.Name;

            _context.Entry(category).State = EntityState.Modified;
        }

        public bool Delete(int id)
        {
            var category = new Category
                {
                    CategoryId = id
                };

            _context.Entry(category).State = EntityState.Deleted;

            return true;
        }

        public IEnumerable<SelectListItem> GetCategoriesForBlogView()
        {
            var categories = _context.Categories
                .AsEnumerable()
                .Select(c => new SelectListItem
                {
                    Value = c.CategoryId.ToString(),
                    Text = c.Name
                });

            return categories;
        }
    }
}
