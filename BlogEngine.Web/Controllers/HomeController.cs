using System.Web.Mvc;
using BlogEngine.Core.Infrastructure;
using BlogEngine.Core.Models;
using BlogEngine.Core.Work;

namespace BlogEngine.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMailService _mailService;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(IMailService mailService, IUnitOfWork unitOfWork)
        {
            _mailService = mailService;
            _unitOfWork = unitOfWork;
        }

        public ActionResult Index()
        {
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

        [ChildActionOnly]
        public PartialViewResult Messages(string username)
        {
            int messageCount = _unitOfWork.MessageRepository.GetUnreadMessageCount(username);

            return PartialView("_Messages", messageCount);
        }
    }
}
