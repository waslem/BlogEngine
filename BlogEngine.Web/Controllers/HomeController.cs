using System.Web.Mvc;
using BlogEngine.Core.Infrastructure;
using BlogEngine.Core.Models;
using BlogEngine.Core.Work;

namespace BlogEngine.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMailService _mailService;

        public HomeController(IMailService mailService)
        {
            _mailService = mailService;
        }

        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            return View(new ContactForm());
        }

        [HttpPost]
        public ActionResult Contact(ContactForm contactForm)
        {
            if(ModelState.IsValid)
            {
                _mailService.Send("jvw@westnet.com.au", contactForm.From, contactForm.Subject, contactForm.Message);
                TempData["Successful"] = "Successful";
                return RedirectToAction("Contact");
            }

            return View(contactForm);
        }

        public ActionResult PrivacyPolicy()
        {
            return View();
        }

        public ActionResult Policy()
        {
            if (Request.IsAjaxRequest())
            {
                return PartialView();
            }
            return View();
        }
    }
}
