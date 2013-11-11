using System.Collections.Generic;
using System.Data;
using System.Linq;
using BlogEngine.Core.Contexts;
using BlogEngine.Core.Infrastructure;
using BlogEngine.Core.Models;
using BlogEngine.Core.ViewModels;
using System;
using System.Data.Objects;

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
                VoteScore = 0, 
                Visible = true
            };

            var blog = _context.BlogEntries.Find(comment.BlogId);

            blog.Comments.Add(newComment);
        }

        public void Edit(Comment comment)
        {
            _context.Entry(comment).State = EntityState.Modified;
        }

        // we don't actually delete the comment, just set visible to false
        public bool Delete(int id)
        {
            bool success = false;

            var model = _context.Comments.Where(u => u.UserId == id).FirstOrDefault();

            if (model != null)
            {
                model.Visible = false;
                _context.Entry(model).State = EntityState.Modified;
                success = true;
            }

            return success;
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

        public CommentSummaryVM GetCommentSummaryVM()
        {
            var summary = new CommentSummaryVM();

            summary.TotalComments = _context.Comments.Count();

            // need to use entityfunctions class which allows use of various clr methods in linq to entities queries. 
            summary.CommentsLast24hours = _context.Comments.Count(c => c.CommentDate > EntityFunctions.AddDays(DateTime.Now, -1));

            var commentsByUser = _context.Comments
                                        .Where(c => c.Visible)
                                        .GroupBy(o => o.User.UserName)
                                        .Select(g => new UserCommentSummaryVM
                                            { 
                                                Username = g.Key,
                                                CommentCount = g.Count() 
                                            }).ToList();

            summary.UsersCommentCount = commentsByUser;

            summary.UserMostComments = commentsByUser.OrderByDescending(o => o.CommentCount).FirstOrDefault().Username;

            return summary;
        }


        public bool UpdateVisibleComment(int id, bool isChecked)
        {
            bool success = false;
            var comment = _context.Comments.Where(c => c.CommentId == id).FirstOrDefault();

            if (comment != null)
            {
                if (isChecked)
                    comment.Visible = true;

                comment.Visible = false;
                success = true;
                _context.Entry(comment).State = EntityState.Modified;
            }

            return success;
        }
    }
}
