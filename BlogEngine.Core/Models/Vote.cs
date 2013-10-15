using BlogEngine.Core.Models.Enums;

namespace BlogEngine.Core.Models
{
    public class Vote
    {
        public int VoteId { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }

        public int CommentId { get; set; }
        public virtual Comment Comment { get; set; }

        public VoteType VoteType { get; set; }
    }
}
