using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogEngine.Core.ViewModels
{
    public class BlogViewModel
    {
        public int UserId { get; set; }
        public DateTime DateCreated { get; set; }
        public int CategoryId { get; set; }
        public string BlogEntry { get; set; }

    }
}
