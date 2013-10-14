using BlogEngine.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogEngine.Core.ViewModels
{
    public class BlogEntryView
    {
        public int BlogEntryId { get; set; }

        public string BlogTitle { get; set; }

        public string BlogShortDescription { get; set; }
        public string BlogEntryText { get; set; }
        public string DateCreated { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }

        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    }
}
