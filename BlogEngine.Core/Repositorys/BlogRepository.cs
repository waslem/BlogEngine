using System.Data;
using BlogEngine.Core.Contexts;
using BlogEngine.Core.Infrastructure;
using BlogEngine.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using BlogEngine.Core.ViewModels;
using BlogEngine.Core.Helpers;

namespace BlogEngine.Core.Repositorys
{
    public class BlogRepository : IBlogRepository, IDisposable
    {
        private readonly BlogContext _context;
        private bool disposed = false;

        public BlogRepository()
        {
            _context = new BlogContext();
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
                    Entry = blog.BlogEntryText,
                    Title = blog.BlogTitle,
                    Category = blog.Category.Name,
                    CreatedBy = blog.User.UserName,
                    CreatedDate = blog.DateCreated.ToShortDateString() + "-" + blog.DateCreated.ToShortTimeString(),
                    CommentCount = blog.Comments.Count()
                }).OrderByDescending(o => o.CreatedDate).ToList();
        }

        public void Create(BlogEntry blogEntry)
        {
            _context.BlogEntries.Add(blogEntry);
            _context.SaveChanges();
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
            blog.BlogEntryText = model.BlogEntryText;
            blog.BlogTitle = model.BlogTitle;

            _context.Entry(blog).State = EntityState.Modified;
            _context.SaveChanges();

        }

        public void Delete(int id)
        {
            var blog = new BlogEntry {BlogEntryId = id};
            _context.Entry(blog).State = EntityState.Deleted;
            _context.SaveChanges();
        }

        public ICollection<BlogEntryView> GetAllView()
        {
            var blogs = GetAll();

            return blogs.Select(blog => new BlogEntryView
            {

                BlogEntryId = blog.BlogEntryId,
                BlogEntryText = blog.BlogEntryText.Truncate(300),
                BlogTitle = blog.BlogTitle,
                Category = blog.Category,
                User = blog.User,
                DateCreated = blog.DateCreated.ToShortDateString(),
                Comments = blog.Comments
            })
            .ToList();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
