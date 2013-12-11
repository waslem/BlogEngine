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

            string[] from = new string[messages.Count];
            string[] to = new string[messages.Count];
            int i = 0;

            foreach (var mess in messages)
	        {
                from[i] = _unitOfWork.UserRepository.GetUsername(mess.UserId);
                to[i] = _unitOfWork.UserRepository.GetUsername(mess.RecievedById);
                i++;
	        }

            List<MessageView> model = ModelBinder.Message(messages, from, to).ToList();

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

                MessageDetailsView model = ModelBinder.Message(message, 
                    _unitOfWork.UserRepository.GetUsername(message.UserId),
                    _unitOfWork.UserRepository.GetUsername(message.RecievedById));

                return View(model);
            }

            return HttpNotFound();
        }

        [HttpPost]
        public ActionResult MarkAsUnread(int id)
        {
           if (_unitOfWork.MessageRepository.MarkMessageAsUnReadByRecipient(id, User.Identity.Name))
           {
               _unitOfWork.Save();
               return RedirectToAction("Index");
           }

           return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult MarkAsRead(int id)
        {
            if (_unitOfWork.MessageRepository.MarkMessageAsReadByRecipient(id, User.Identity.Name))
            {
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            if (_unitOfWork.MessageRepository.DeleteMessage(id, User.Identity.Name))
            {
                _unitOfWork.Save();

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }
    }
}
