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

        public static BlogViewModel Blog(BlogEntry blogEntry)
        {
            var blogViewModel = new BlogViewModel
                {
                    SelectedCategory = blogEntry.CategoryId.ToString(),
                    BlogEntryText = blogEntry.BlogEntryText,
                    BlogTitle = blogEntry.BlogTitle,
                    DateCreated = blogEntry.DateCreated,
                    BlogId = blogEntry.BlogEntryId
                };

            return blogViewModel;
        }

        public static BlogEntry BlogCreate(BlogViewModel model)
        {
            var blogEntry = new BlogEntry
            {
                UserId = model.UserId,
                BlogEntryText = model.BlogEntryText,
                BlogTitle = model.BlogTitle,
                DateCreated = DateTime.Now,
                CategoryId = Int32.Parse(model.SelectedCategory)
            };

            return blogEntry;
        }
    }
}