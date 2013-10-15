using BlogEngine.Core.Contexts;
using BlogEngine.Core.Infrastructure;
using BlogEngine.Core.Models;
using BlogEngine.Core.Models.Enums;

namespace BlogEngine.Core.Repositorys
{
    public class VoteRepository : IVoteRepository
    {
        private readonly BlogContext _context;

        public VoteRepository(BlogContext context)
        {
            _context = context;
        }

        public void UpVote(int commentId, int userId)
        {
            _context.Votes.Add(new Vote
                {
                     UserId = userId, 
                     CommentId = commentId,
                     VoteType = VoteType.UpVote
                });

            CalculateVotesForComment(commentId);
        }

        public void DownVote(int commentId, int userId)
        {
            _context.Votes.Add(new Vote
                { 
                    UserId = userId,
                    CommentId = commentId,
                    VoteType = VoteType.DownVote
                });

            CalculateVotesForComment(commentId);
        }

        public void CalculateVotesForComment(int commentId)
        {
            var comment = _context.Comments.Find(commentId);
            int voteCount = 0;

            if (comment.Votes.Count > 0)
            {
                foreach (var vote in comment.Votes)
                {
                    if (vote.VoteType == VoteType.UpVote)
                    {
                        voteCount++;
                    }
                    else
                    {
                        voteCount--;
                    }
                }
                comment.VoteScore = voteCount;
            }
            else
            {
                comment.VoteScore = 0;
            }
        }
    }
}
