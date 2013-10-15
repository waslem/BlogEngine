using BlogEngine.Core.Infrastructure;

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
