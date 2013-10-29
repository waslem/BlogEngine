using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using BlogEngine.Core.ViewModels;
using BlogEngine.Core.Helpers;
using BlogEngine.Core.Contexts;
using BlogEngine.Core.Infrastructure;
using BlogEngine.Core.Models;
using System.Web.Mvc;

namespace BlogEngine.Core.Repositorys
{
    public class BlogRepository : IBlogRepository
    {
        private readonly BlogContext _context;

        public BlogRepository(BlogContext context)
        {
            _context = context;
        }

        public ICollection<BlogEntry> GetAll()
        {
            var blogs = _context
                            .BlogEntries
                            .Include("Comments")
                            .OrderByDescending(o => o.DateCreated)
                            .ToList();
            return blogs;
        }

        public ICollection<BlogListViewModel> GetAllViewModel()
        {
            var blogs = GetAll();

            return blogs.Select(blog => new BlogListViewModel
                {
                    BlogId = blog.BlogEntryId,
                    ShortDescription = blog.BlogShortDescription,
                    Entry = blog.BlogEntryText,
                    Title = blog.BlogTitle,
                    Category = blog.Category.Name,
                    CreatedBy = blog.User.UserName,
                    CreatedDate = blog.DateCreated.ToShortDateString() + "-" + blog.DateCreated.ToShortTimeString(),
                    CommentCount = blog.Comments.Count()
                }).ToList();
        }

        public void Create(BlogEntry blogEntry)
        {
            _context.BlogEntries.Add(blogEntry);
        }

        public BlogEntry GetBlogById(int id)
        {
            var blog = _context.BlogEntries.Find(id);

            return blog;
        }

        public void Edit(BlogViewModel model)
        {
            var blog = _context.BlogEntries.Find(model.BlogId);

            blog.CategoryId = Int32.Parse(model.SelectedCategory);
            blog.BlogTitle = model.BlogTitle;
            blog.BlogShortDescription = model.BlogShortDescription;
            blog.BlogEntryText = model.BlogEntryText;

            if (blog.Image == null)
            {
                blog.Image = new BlogImage { ImagePath = "~/Images/" + model.BlogImage.FileName };
            }
            else
            {
                blog.Image.ImagePath = "~/Images/" + model.BlogImage.FileName;
            }

            _context.Entry(blog).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            var blog = new BlogEntry {BlogEntryId = id};

            _context.Entry(blog).State = EntityState.Deleted;
        }

        public ICollection<BlogEntryView> GetAllView()
        {
            var blogs = GetAll();

            return blogs.Select(blog => new BlogEntryView
            {
                BlogEntryId = blog.BlogEntryId,
                BlogShortDescription = blog.BlogShortDescription.Truncate(300),
                BlogEntryText = blog.BlogEntryText,
                BlogTitle = blog.BlogTitle,
                Category = blog.Category,
                User = blog.User,
                DateCreated = blog.DateCreated.ToShortDateString(),
                Comments = blog.Comments, 
                Image = blog.Image
            })
            .ToList();
        }

        public ICollection<BlogEntry> GetTopBlogs(int blogCount)
        {
            ICollection<BlogEntry> blogs = _context.BlogEntries.OrderByDescending(b => b.Comments.Count).Take(blogCount).ToList();

            return blogs;
        }
    }
}
