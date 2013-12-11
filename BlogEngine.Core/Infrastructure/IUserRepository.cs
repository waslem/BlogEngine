using System.Collections.Generic;
using BlogEngine.Core.Models;

namespace BlogEngine.Core.Infrastructure
{
    public interface IUserRepository
    {
        User GetUser(string username);
        int GetUserId(string username);
        User GetUserById(int id);
        ICollection<User> GetAllUsers();

        void AddUserToAdminRole(int id);
        void RemoveUserFromAdminRole(int id);

        bool UserExists(string username);


        void EditRoles(int userId, IList<ViewModels.RoleCheckBox> roles);

        void RemoveUserFromRoles(int userId);

        void DeleteUser(int userId);

        string GetUsername(int userId);

        void UpdateMembers(int id, ViewModels.UserEditViewModel model);

        List<System.Web.Mvc.SelectListItem> GetUsersSelectList();
    }
}
