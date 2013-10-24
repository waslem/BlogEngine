using BlogEngine.Core.Work;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogEngine.Web.Controllers
{
    public class ValidationController : Controller
    {
        private readonly IUnitOfWork _unit;

        //
        // GET: /Validation/
        public ValidationController(IUnitOfWork unit)
        {
            _unit = unit;
        }

        public JsonResult IsUsernameAvailable(string username)
        {
            if (!_unit.UserRepository.UserExists(username))
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }

            string suggested = "The username " + username + " is alread taken, please choose another.";

            return Json(suggested, JsonRequestBehavior.AllowGet);
        }
    }
}
