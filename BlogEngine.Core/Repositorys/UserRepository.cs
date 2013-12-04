using System;
using System.Collections.Generic;
using System.Linq;
using BlogEngine.Core.Contexts;
using BlogEngine.Core.Infrastructure;
using BlogEngine.Core.Models;
using System.Web.Security;
using BlogEngine.Core.ViewModels;
using WebMatrix.WebData;

namespace BlogEngine.Core.Repositorys
{
    public class UserRepository : IUserRepository
    {
        private readonly BlogContext _context;

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

        public User GetUserById(int id)
        {
            var user = _context.Users.Find(id);

            return user;
        }

        public ICollection<User> GetAllUsers()
        {
            var users = _context.Users.ToList() as ICollection<User>;
            return users;
        }

        public void AddUserToAdminRole(int id)
        {
            // get the user by the id
            var user = _context.Users.FirstOrDefault(u => u.UserId == id);

            // using the websecurity roles api to add the user to the role
            if (!Roles.IsUserInRole(user.UserName, "Administrator"))
            {
                Roles.AddUserToRole(user.UserName, "Administrator");
            }
        }

        public void RemoveUserFromAdminRole(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserId == id);

            if (!Roles.IsUserInRole(user.UserName, "Administrator"))
            {
                Roles.RemoveUserFromRole(user.UserName, "Administrator");
            }
        }

        public bool UserExists(string username)
        {
            return _context.Users.Any(u => u.UserName == username);
        }


        public void EditEmail(int userId, string email)
        {
            var user = _context.Users.Find(userId);

            if (user != null)
            {
                user.Email = email;
                _context.Entry(user).State = System.Data.EntityState.Modified;
            }
            
        }

        public void EditRoles(int userId, IList<RoleCheckBox> roles)
        {
            var user = _context.Users.Find(userId);

            if (user != null)
            {
                foreach (var role in roles)
                {
                    SyncRole(user, role);
                }
            }
        }

        private static void SyncRole(User user, RoleCheckBox role)
        {
            // only need to check  
            //  - if role is checked and user is not in role, add them
            //  - if role is checked and user is in role, do nothing
            //  - if role is unchecked and user is in role, remove them.
            //  - if role is unchecked and user is not in role, do nothing.

            if (role.IsChecked)
            {
                // if user isn't in the role, add the role is checked, add them to the role
                if (!Roles.IsUserInRole(user.UserName,role.Name))
                {
                    Roles.AddUserToRole(user.UserName, role.Name);
                }
            }
            else
            {
                // if role is unchecked and the user is in the role, remove them from the role
                if (Roles.IsUserInRole(user.UserName, role.Name))
                {
                    Roles.RemoveUserFromRole(user.UserName, role.Name);
                }
            }
        }

        public void RemoveUserFromRoles(int userId)
        {
            var user = _context.Users.Find(userId);

            if (user != null)
            {
                Roles.RemoveUserFromRoles(user.UserName, Roles.GetRolesForUser(user.UserName));
            }
        }

        public void DeleteUser(int userId)
        {
            var user = _context.Users.Find(userId);

            if (user != null)
            {
                _context.Users.Remove(user);
            }
        }

        public string GetUsername(int userId)
        {
            var user = _context.Users.Find(userId);

            if (user != null)
            {
                return user.UserName;
            }

            return "";
        }
    }
}
