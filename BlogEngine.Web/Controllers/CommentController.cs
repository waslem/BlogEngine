using System.Web.Mvc;
using WebMatrix.WebData;
using BlogEngine.Core.Models;
using BlogEngine.Core.Work;

namespace BlogEngine.Web.Controllers
{
    public class CommentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CommentController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //
        // GET: /Comment/
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Comment/Edit/1
        public ActionResult Edit(int id)
        {
            var model = _unitOfWork.CommentRepository.GetById(id);
            return View(model);
        }

        //
        // POST: /Comment/Edit/1
        [HttpPost]
        public ActionResult Edit(Comment comment)
        {
            if (ModelState.IsValid)
            {
                if(WebSecurity.CurrentUserId == comment.UserId)
                {
                    _unitOfWork.CommentRepository.Edit(comment);
                    _unitOfWork.Save();
                    return RedirectToAction("Index", "Blog");
                }
                // error here because you can only edit your own comments
                return RedirectToAction("Index", "Blog");
            }

            return View(comment);
        }
    }
}
