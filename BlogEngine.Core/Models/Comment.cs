using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogEngine.Core.Models
{
    public class Comment
    {
        public int CommentId { get; set; }

        public string CommentText { get; set; }
        public DateTime CommentDate { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }

    }
}
