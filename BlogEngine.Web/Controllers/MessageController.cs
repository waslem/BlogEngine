using BlogEngine.Core.ViewModels;
using BlogEngine.Core.Work;
using BlogEngine.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogEngine.Web.Controllers
{
    public class MessageController : UserController
    {

        private IUnitOfWork _unitOfWork;

        public MessageController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        //
        // GET: /Message/

        public ActionResult Index()
        {
            var messages = _unitOfWork.MessageRepository.GetRecievedMessages(User.Identity.Name);

            List<MessageView> model = ModelBinder.Message(messages).ToList();

            return View(model);
        }

        public ActionResult Details(int messageId, string username)
        {
            if (_unitOfWork.MessageRepository.MarkMessageAsReadByRecipient(messageId, username))
            {
                _unitOfWork.Save();
                var message = _unitOfWork.MessageRepository.GetMessageById(messageId, username);

                if (message == null)
                    return HttpNotFound();

                MessageDetailsView model = ModelBinder.Message(message);
                return View(model);
            }

            return HttpNotFound();
        }
    }
}
