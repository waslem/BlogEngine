using System.Web.Mvc;

namespace BlogEngine.Web.Helpers
{
    // Base controller for all of the Admin area

    [Authorize(Roles = "Administrator")]
    public abstract class AdminController : Controller { }

    [Authorize(Roles = "User")]
    public abstract class UserController : Controller { }
}