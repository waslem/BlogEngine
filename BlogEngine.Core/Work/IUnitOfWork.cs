using BlogEngine.Core.Infrastructure;
using BlogEngine.Core.Repositorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogEngine.Core.Work
{
    public interface IUnitOfWork
    {
        void Save();

        ICategoryRepository CategoryRepository { get; }
        IBlogRepository BlogRepository { get; }
        ICommentRepository CommentRepository { get; }
        IUserRepository UserRepository { get; }
        IVoteRepository VoteRepository { get; }
    }
}
