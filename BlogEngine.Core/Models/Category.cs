using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogEngine.Core.Models
{
    public class Category
    {
        public int CategoryId { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        // TODO: Implement image for category
        //public CategoryImage CategoryImage { get; set; }

        public virtual ICollection<BlogEntry> BlogEntries { get; set; }
    }
}
