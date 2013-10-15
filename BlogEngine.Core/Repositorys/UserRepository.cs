using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogEngine.Core.Contexts;
using BlogEngine.Core.Infrastructure;
using BlogEngine.Core.Models;
using BlogEngine.Core.ViewModels;
using System.Web.Mvc;
using System.Web.Security;

namespace BlogEngine.Core.Repositorys
{
    public class UserRepository : IUserRepository
    {
        private  BlogContext _context;

        public UserRepository(BlogContext context)
        {
            _context = context;
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

        public ICollection<User> GetAllUsers()
        {
            var users = _context.Users.ToList() as ICollection<User>;
            return users;
        }

        public void AddUserToAdminRole(int id)
        {
            // get the user by the id
            var user = _context.Users.Where(u => u.UserId == id).FirstOrDefault();

            // using the websecurity roles api to add the user to the role
            if (!Roles.IsUserInRole(user.UserName, "Administrator"))
            {
                Roles.AddUserToRole(user.UserName, "Administrator");
            }
        }

        public void RemoveUserFromAdminRole(int id)
        {
            var user = _context.Users.Where(u => u.UserId == id).FirstOrDefault();

            if (!Roles.IsUserInRole(user.UserName, "Administrator"))
            {
                Roles.RemoveUserFromRole(user.UserName, "Administrator");
            }
        }
    }
}
