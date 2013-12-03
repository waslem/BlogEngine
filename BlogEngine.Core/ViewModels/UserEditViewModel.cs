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
        public IList<string> Roles { get; set; }

        public UserEditViewModel()
        {
            Roles = new List<string>();
        }
    }
}
