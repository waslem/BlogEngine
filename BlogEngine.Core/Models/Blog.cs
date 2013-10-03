using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogEngine.Core.Models
{
    public class Blog
    {
        public int BlogId { get; set; }
        public string BlogEntry { get; set; }
        public DateTime DateCreated { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }

        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public Blog()
        {
            Comments = new List<Comment>();
        }
    }
}
