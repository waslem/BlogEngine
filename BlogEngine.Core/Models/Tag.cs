using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogEngine.Core.Models
{
    public class Tag
    {
        public int TagId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<BlogEntry> BlogEntries { get; set; }
    }
}
