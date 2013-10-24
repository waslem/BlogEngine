﻿using System.Collections.Generic;
using BlogEngine.Core.Models;

namespace BlogEngine.Core.Infrastructure
{
    public interface IUserRepository
    {
        User GetUser(string username);
        int GetUserId(string username);
        ICollection<User> GetAllUsers();

        void AddUserToAdminRole(int id);
        void RemoveUserFromAdminRole(int id);

        bool UserExists(string username);
    }
}
