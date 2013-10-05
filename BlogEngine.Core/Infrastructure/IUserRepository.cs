using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogEngine.Core.Models;

namespace BlogEngine.Core.Infrastructure
{
    public interface IUserRepository
    {
        User GetUser(string username);
        int GetUserId(string username);
    }
}
