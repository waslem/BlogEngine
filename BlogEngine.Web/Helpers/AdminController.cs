using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogEngine.Web.Helpers
{
    // Base controller for all of the Admin area

    [Authorize(Roles = "Administrator")]
    public abstract class AdminController : Controller { }
}