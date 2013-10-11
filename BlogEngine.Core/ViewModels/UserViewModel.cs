using BlogEngine.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BlogEngine.Core.ViewModels
{
    public class UserViewModel
    {
        public string SelectedUser { get; set; }
        public IEnumerable<SelectListItem> users { get; set; }
    }
}
