using System;
using System.Collections.Generic;

namespace BlogEngine.Core.Models
{
    public class Comment
    {
        public int CommentId { get; set; }

        public string CommentText { get; set; }

        public DateTime CommentDate { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }

        public int? ParentId { get; set; }
        public virtual Comment Parent { get; set; }

        public virtual ICollection<Comment> Children { get; set; }

        public int VoteScore { get; set; }
        public virtual ICollection<Vote> Votes { get; set; }
    }
}
