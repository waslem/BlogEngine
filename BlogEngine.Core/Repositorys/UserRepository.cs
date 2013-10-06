using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogEngine.Core.Contexts;
using BlogEngine.Core.Infrastructure;
using BlogEngine.Core.Models;

namespace BlogEngine.Core.Repositorys
{
    public class UserRepository : IUserRepository, IDisposable
    {
        private readonly BlogContext _context;
        private bool disposed = false;

        public UserRepository()
        {
            _context = new BlogContext();
        }
        public User GetUser(string username)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserName == username);

            return user;
        }

        public int GetUserId(string username)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserName == username);

            int userId = 0;
            if (user != null)
            {
                userId = user.UserId;
            }

            return userId;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
