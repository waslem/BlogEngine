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
            return View();
        }

        //
        // GET: /admin/User/Users
        public ActionResult Users()
        {
            var users = _unitOfWork.UserRepository.GetAllUsers();
            return View(users);
        }

        //
        // GET: /Admin/User/AddUserToAdmins
        public ActionResult AddUserToAdmins()
        {
            IEnumerable<SelectListItem> _users = _unitOfWork.UserRepository.GetAllUsers()
                                                    .Select(u => new SelectListItem 
                                                        { 
                                                            Value = u.UserId.ToString(), 
                                                            Text = u.FirstName + " " + u.LastName 
                                                        });
 
            var model = new UserViewModel
            {
                 users = _users,
                 SelectedUser = "-1"
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult AddUserToAdmins(UserViewModel viewModel)
        {
            int id = Int32.Parse(viewModel.SelectedUser);

            _unitOfWork.UserRepository.AddUserToAdminRole(id);
            _unitOfWork.Save();

            return RedirectToAction("Success");
        }

        //
        // GET: /Admin/User/RemoveUserFromAdmins
        public ActionResult RemoveUserFromAdmins()
        {
            IEnumerable<SelectListItem> _users = _unitOfWork.UserRepository.GetAllUsers()
                                                    .Select(u => new SelectListItem 
                                                        { 
                                                            Value = u.UserId.ToString(), 
                                                            Text = u.FirstName + " " + u.LastName 
                                                        });

            var model = new UserViewModel
            {
                users = _users,
                SelectedUser = "-1"
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult RemoveUserFromAdmins(UserViewModel viewModels)
        {
            int id = Int32.Parse(viewModels.SelectedUser);

            _unitOfWork.UserRepository.RemoveUserFromAdminRole(id);
            _unitOfWork.Save();

            return RedirectToAction("Success");
        }

        public ActionResult Success()
        {
            return View();
        }
    }
}
