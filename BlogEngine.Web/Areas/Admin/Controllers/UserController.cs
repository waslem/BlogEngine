using BlogEngine.Core.Infrastructure;
using BlogEngine.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogEngine.Web.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        private IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        //
        // GET: /Admin/User/
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Admin/User/AddUserToAdmins
        public ActionResult AddUserToAdmins()
        {
            IEnumerable<SelectListItem> _users = _userRepository.GetAllUsers()
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

            _userRepository.AddUserToAdminRole(id);

            return RedirectToAction("Success");
        }

        //
        // GET: /Admin/User/RemoveUserFromAdmins
        public ActionResult RemoveUserFromAdmins()
        {
            IEnumerable<SelectListItem> _users = _userRepository.GetAllUsers()
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

            _userRepository.RemoveUserFromAdminRole(id);

            return RedirectToAction("Success");
        }

        public ActionResult Success()
        {
            return View();
        }
    }
}
