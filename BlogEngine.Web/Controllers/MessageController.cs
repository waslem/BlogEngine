using BlogEngine.Core.Models;
using BlogEngine.Core.ViewModels;
using BlogEngine.Core.Work;
using BlogEngine.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

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

            List<MessageView> model = BindMessageView(messages);

            return View(model);
        }

        public ActionResult Sent()
        {
            var messages = _unitOfWork.MessageRepository.GetSentMessages(User.Identity.Name);

            List<MessageView> model = BindMessageView(messages);

            return View(model);

        }
        public ActionResult Trash()
        {
            var messages = _unitOfWork.MessageRepository.GetDeletedMessages(User.Identity.Name);

            List<MessageView> model = BindMessageView(messages);

            return View(model);

        }
        public ActionResult Send()
        {
            var model = new MessageCreateView();
            model.To = _unitOfWork.UserRepository.GetUsersSelectList();
            model.From = User.Identity.Name;
                        
            return View(model);
        }

        [HttpPost]
        public ActionResult Send(MessageCreateView model)
        {
            if (ModelState.IsValid)
            {
                var toId = Int32.Parse(model.ToId);

                // model, recipientId, senderId
                Message message = ModelBinder.Message(model,toId,_unitOfWork.UserRepository.GetUserId(User.Identity.Name));

                if (_unitOfWork.MessageRepository.Send(message))
                {
                    _unitOfWork.Save();
                    return RedirectToAction("Index");
                }

                // TODO: do this differently
                return View(model);
            }
            //send message
            
            model.To = _unitOfWork.UserRepository.GetUsersSelectList();
            model.From = User.Identity.Name;

            return View(model);

        }
        public ActionResult DetailsSent(int messageId, string username)
        {
            var message = _unitOfWork.MessageRepository.GetMessageById(messageId, username);

            if (message == null)
                return HttpNotFound();

            MessageDetailsView model = ModelBinder.Message(message,
                _unitOfWork.UserRepository.GetUsername(message.UserId),
                _unitOfWork.UserRepository.GetUsername(message.RecievedById));

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

        public ActionResult DeleteFromTrash(int id)
        {
            if (_unitOfWork.MessageRepository.DeleteFromTrash(id, User.Identity.Name))
            {
                _unitOfWork.Save();
                return RedirectToAction("Trash");
            }

            return RedirectToAction("Trash");
        }

        public ActionResult UnDelete(int id)
        {
            if (_unitOfWork.MessageRepository.UnDeleteMessage(id, User.Identity.Name))
            {
                _unitOfWork.Save();
                return RedirectToAction("Trash");
            }

            return RedirectToAction("Trash");
        }

        public ActionResult DeleteSent(int id)
        {
            if (_unitOfWork.MessageRepository.DeleteSentMessage(id, User.Identity.Name))
            {
                _unitOfWork.Save();
                return RedirectToAction("Sent");
            }

            return RedirectToAction("Sent");
        }

        [ChildActionOnly]
        public ActionResult _Folders (string page)
        {
            var model = new BlogEngine.Core.ViewModels.FolderPartialView();

            model.PageName = page;
            model.UnreadCount = _unitOfWork.MessageRepository.GetUnreadMessageCount(User.Identity.Name);

            return PartialView("_Folders", model);

        }
        private List<MessageView> BindMessageView(ICollection<Message> messages)
        {
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
            return model;
        } 

    }
}
