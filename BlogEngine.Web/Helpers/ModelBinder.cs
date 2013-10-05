using BlogEngine.Core.Models;
using BlogEngine.Core.ViewModels;
using System;

namespace BlogEngine.Web.Helpers
{
    public class ModelBinder
    {
        public static CategoryViewModel Category(Category category)
        {
            var model = new CategoryViewModel
            {
                CategoryId = category.CategoryId,
                CreatedDate = category.CreatedDate,
                Description = category.Description,
                Name = category.Name
            };

            return model;
        }

        public static BlogViewModel Blog(Blog blog)
        {
            var blogViewModel = new BlogViewModel
                {
                    SelectedCategory = blog.CategoryId.ToString(),
                    BlogEntry = blog.BlogEntry,
                    DateCreated = blog.DateCreated,
                    BlogId = blog.BlogId
                };

            return blogViewModel;
        }

        public static Blog BlogCreate(BlogViewModel model)
        {
            var blog = new Blog
            {
                UserId = model.UserId,
                BlogEntry = model.BlogEntry,
                DateCreated = DateTime.Now,
                CategoryId = Int32.Parse(model.SelectedCategory)
            };

            return blog;
        }
    }
}