﻿using BlogEngine.Core.Work;
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

            return View(messages);
        }

    }
}
