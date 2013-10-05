using System.Data;
using BlogEngine.Core.Contexts;
using BlogEngine.Core.Infrastructure;
using BlogEngine.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
            var blogs = _context.Blogs.Include("Comments").ToList();
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

        public Blog GetBlogById(int id)
        {
            var blog = _context.Blogs.Find(id);

            return blog;
        }

        public void Edit(BlogViewModel model)
        {
            var blog = _context.Blogs.Find(model.BlogId);

            blog.CategoryId = Int32.Parse(model.SelectedCategory);
            blog.BlogEntry = model.BlogEntry;

            _context.Entry(blog).State = EntityState.Modified;
            _context.SaveChanges();

        }

        public void Delete(int id)
        {
            var blog = new Blog {BlogId = id};
            _context.Entry(blog).State = EntityState.Deleted;
            _context.SaveChanges();
        }

        public void AddComment(CommentViewModel comment)
        {
            var newComment = new Comment
                {
                    CommentDate = comment.CommentDate,
                    CommentText = comment.CommentText, 
                    UserId = comment.UserId
                };

            var blog = _context.Blogs.Find(comment.BlogId);

            blog.Comments.Add(newComment);
            _context.SaveChanges();
        }
    }
}
