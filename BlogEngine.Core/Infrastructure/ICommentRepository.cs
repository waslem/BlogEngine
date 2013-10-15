using System.Collections.Generic;
using BlogEngine.Core.Models;
using BlogEngine.Core.ViewModels;

namespace BlogEngine.Core.Infrastructure
{
    public interface ICommentRepository
    {
        void Create(CommentViewModel comment);
        void Edit(Comment comment);

        Comment GetById(int id);

        ICollection<Comment> GetAll();
    }
}
