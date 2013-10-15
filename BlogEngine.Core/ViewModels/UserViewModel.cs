using System.Collections.Generic;
using System.Web.Mvc;

namespace BlogEngine.Core.ViewModels
{
    public class UserViewModel
    {
        public string SelectedUser { get; set; }
        public IEnumerable<SelectListItem> users { get; set; }
    }
}
