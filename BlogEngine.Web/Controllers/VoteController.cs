using System.Web.Mvc;
using WebMatrix.WebData;
using BlogEngine.Core.Work;

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

        [Authorize]
        [HttpPost]
        public ActionResult UpVote(int commentId)
        {
            // can implement if user has not already voted ?
            _unitOfWork.VoteRepository.UpVote(commentId, WebSecurity.GetUserId(User.Identity.Name));
            _unitOfWork.Save();

            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        [HttpPost]
        public ActionResult DownVote(int commentId)
        {
            _unitOfWork.VoteRepository.DownVote(commentId, WebSecurity.GetUserId(User.Identity.Name));
            _unitOfWork.Save();

            return RedirectToAction("Index", "Home");
        }
    }
}
