﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BlogEngine.Core.ViewModels
{
    public class MessageView
    {
        public int MessageId { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool Read { get; set; }
        public DateTime Sent { get; set; }
    }

    public class MessageDetailsView
    {
        public int MessageId { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public DateTime Sent { get; set; }
        public bool DeletedByRecipient { get; set; }
        public bool ReadByRecipient { get; set; }
    }

    public class MessageCreateView
    {
        public string From { get; set; }
        public string ToId { get; set; }
        public List<SelectListItem> To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
