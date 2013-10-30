using BlogEngine.Core.Models;
using BlogEngine.Core.ViewModels;
using System;
using System.Collections.Generic;

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

        public static List<CategoryViewModel> Categories(List<Category> categories)
        {
            List<CategoryViewModel> cvm = new List<CategoryViewModel>();

            foreach (var cat in categories)
            {
                var model = new CategoryViewModel
                {
                    CategoryId = cat.CategoryId,
                    CreatedDate = cat.CreatedDate,
                    Description = cat.Description,
                    Name = cat.Name
                };
                cvm.Add(model);
            }
            return cvm;
        }

        public static BlogViewModel Blog(BlogEntry blogEntry)
        {
            var blogViewModel = new BlogViewModel
                {
                    SelectedCategory = blogEntry.CategoryId.ToString(),
                    BlogShortDescription = blogEntry.BlogShortDescription,
                    BlogEntryText = blogEntry.BlogEntryText,
                    BlogTitle = blogEntry.BlogTitle,
                    DateCreated = blogEntry.DateCreated,
                    BlogId = blogEntry.BlogEntryId
                };

            return blogViewModel;
        }

        public static BlogEntry BlogCreate(BlogViewModel model)
        {
            var blogEntry = new BlogEntry();

            blogEntry.UserId = model.UserId;
            blogEntry.BlogShortDescription = model.BlogShortDescription;
            blogEntry.BlogEntryText = model.BlogEntryText;
            blogEntry.BlogTitle = model.BlogTitle;
            blogEntry.DateCreated = DateTime.Now;
            blogEntry.CategoryId = Int32.Parse(model.SelectedCategory);

            if (model.BlogImage != null)
            {
                blogEntry.Image = new BlogImage { ImagePath = "~/Images/" + model.BlogImage.FileName };
            }
            else
            {
                blogEntry.Image = new BlogImage();
            }

            blogEntry.Tags = new List<Tag>();
            //todo add tags to blog here
            foreach (var tag in model.Tags)
            {
                if (tag.Checked)
                {
                    blogEntry.Tags.Add(tag);
                }
            }


            return blogEntry;
        }

        public static ICollection<BlogSummaryView> BlogSummary(ICollection<BlogEntry> blogs)
        {
            List<BlogSummaryView> blogSummary = new List<BlogSummaryView>();
            foreach (var blog in blogs)
            {
                blogSummary.Add(new BlogSummaryView { BlogId = blog.BlogEntryId, BlogTitle = blog.BlogTitle, Comments = blog.Comments.Count });
            }

            return blogSummary;
        }

        public static IEnumerable<BlogEntryView> BlogEntryView(List<BlogEntry> blogs)
        {
            List<BlogEntryView> blogEntries = new List<BlogEntryView>();

            foreach (var blog in blogs)
            {
                blogEntries.Add(new BlogEntryView
                {
                    BlogEntryId = blog.BlogEntryId,
                    BlogEntryText = blog.BlogEntryText,
                    BlogShortDescription = blog.BlogShortDescription,
                    BlogTitle = blog.BlogTitle,
                    Category = blog.Category,
                    CategoryId = blog.CategoryId,
                    Comments = blog.Comments,
                    DateCreated = blog.DateCreated.ToShortDateString(),
                    Image = blog.Image,
                    User = blog.User,
                    UserId = blog.UserId
                });
            }

            return blogEntries;
        }
    }
}