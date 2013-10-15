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
using BlogEngine.Core.Models.Enums;

namespace BlogEngine.Core.Repositorys
{
    public class CommentRepository : ICommentRepository
    {
        private readonly BlogContext _context;

        public CommentRepository(BlogContext context)
        {
            _context = context;
        }

        public void Create(CommentViewModel comment)
        {
            var newComment = new Comment
            {
                CommentDate = comment.CommentDate,
                CommentText = comment.CommentText,
                UserId = comment.UserId,
                ParentId = comment.ParentId,
                Children = new List<Comment>(), 
                Votes = new List<Vote>(), 
                VoteScore = 0
            };

            var blog = _context.BlogEntries.Find(comment.BlogId);

            blog.Comments.Add(newComment);
        }

        public void Edit(Comment comment)
        {
            _context.Entry(comment).State = EntityState.Modified;
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
    }
}
