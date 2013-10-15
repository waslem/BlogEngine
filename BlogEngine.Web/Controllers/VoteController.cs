using BlogEngine.Core.Contexts;
using BlogEngine.Core.Work;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace BlogEngine.Web.Controllers
{
    public class VoteController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public VoteController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //
        // GET: /Vote/
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UpVote(int commentId)
        {
            // can implement if user has not already voted ?
            _unitOfWork.VoteRepository.UpVote(commentId, WebSecurity.GetUserId(User.Identity.Name));
            _unitOfWork.Save();

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult DownVote(int commentId)
        {
            _unitOfWork.VoteRepository.DownVote(commentId, WebSecurity.GetUserId(User.Identity.Name));
            _unitOfWork.Save();

            return RedirectToAction("Index", "Home");
        }
    }
}
