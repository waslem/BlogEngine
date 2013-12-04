using BlogEngine.Core.Infrastructure;
using BlogEngine.Core.Models;
using BlogEngine.Core.ViewModels;
using BlogEngine.Core.Work;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GridMvc.Html;
using BlogEngine.Web.Helpers;
using WebMatrix.WebData;
using System.Web.Security;

namespace BlogEngine.Web.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        private IUnitOfWork _unitOfWork;

        public UserController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //
        // GET: /Admin/User/
        public ActionResult Index()
        {
            return RedirectToAction("Users");
        }

        //
        // GET: /admin/User/Users
        public ActionResult Users()
        {
            var users = _unitOfWork.UserRepository.GetAllUsers();

            var model = ModelBinder.Users(users);

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var user = _unitOfWork.UserRepository.GetUserById(id);

            if (user != null)
            {
                UserEditViewModel model = ModelBinder.User(user);
                return View(model);
            }

            return RedirectToAction("Users");
        }

        [HttpPost]
        public ActionResult Edit(int id, UserEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                // update the users details
                _unitOfWork.UserRepository.UpdateMembers(id, model);

                // update the users roles
                _unitOfWork.UserRepository.EditRoles(id, model.Roles);

                _unitOfWork.Save();

                return RedirectToAction("Users");
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            if (ModelState.IsValid)
            { 
                // remove user from all roles
                _unitOfWork.UserRepository.RemoveUserFromRoles(id);
                // tell EF to remove user
                _unitOfWork.UserRepository.DeleteUser(id);
                // use membership api to delete user account
                ((SimpleMembershipProvider)Membership.Provider).DeleteAccount(_unitOfWork.UserRepository.GetUsername(id));

                _unitOfWork.Save();

                return RedirectToAction("Users");
            }
            return HttpNotFound();
        }
    }
}
