using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogEngine.Core.Models
{
    public class Reply
    {
        public int ReplyId { get; set; }

        public string ReplyText { get; set; }
        public DateTime ReplyDate { get; set; }

        public int CommentId { get; set; }
        public virtual Comment Comment { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
