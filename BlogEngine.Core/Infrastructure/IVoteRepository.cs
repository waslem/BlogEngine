using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogEngine.Core.Infrastructure
{
    public interface IVoteRepository
    {
        void UpVote(int commentId, int userId);
        void DownVote(int commentId, int userId);

        void CalculateVotesForComment(int commentId);
    }
}
