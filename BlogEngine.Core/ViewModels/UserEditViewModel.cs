using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogEngine.Core.ViewModels
{
    public class UserEditViewModel
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public IList<RoleCheckBox> Roles { get; set; }

    }

    public class RoleCheckBox
    {
        public bool IsChecked { get; set; }
        public string Name { get; set; }

    }
}
