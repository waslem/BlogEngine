using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BlogEngine.Core.ViewModels
{
    public class BlogViewModel
    {
        public int UserId { get; set; }
        public DateTime DateCreated { get; set; }
        public string SelectedCategory { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; }
        public string BlogEntry { get; set; }
    }
}
