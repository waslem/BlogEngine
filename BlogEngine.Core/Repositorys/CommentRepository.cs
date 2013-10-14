using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogEngine.Core.Contexts;
using BlogEngine.Core.Infrastructure;
using BlogEngine.Core.Models;
using BlogEngine.Core.ViewModels;

namespace BlogEngine.Core.Repositorys
{
    public class CommentRepository : ICommentRepository
    {
        private readonly BlogContext _context;
        private bool _disposed;

        public CommentRepository()
        {
            _context = new BlogContext();
        }

        public void Create(CommentViewModel comment)
        {
            var newComment = new Comment
            {
                CommentDate = comment.CommentDate,
                CommentText = comment.CommentText,
                UserId = comment.UserId,
                ParentId = comment.ParentId,
                Children = new List<Comment>()
            };

            var blog = _context.BlogEntries.Find(comment.BlogId);

            blog.Comments.Add(newComment);
            _context.SaveChanges();
        }

        public void Edit(Comment comment)
        {
            _context.Entry(comment).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public Comment GetById(int id)
        {
            var comment = _context.Comments.Find(id);

            return comment;
        }

        public ICollection<Comment> GetAll()
        {
            var comments = _context
                                .Comments
                                .OrderByDescending(c => c.CommentDate)
                                .ToList() as ICollection<Comment>;

            return comments;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this._disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
