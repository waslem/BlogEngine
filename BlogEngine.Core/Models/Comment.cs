using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

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
    }
}
