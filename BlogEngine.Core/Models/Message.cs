using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogEngine.Core.Models
{
    public class Message
    {
        public int MessageId { get; set; }

        public int UserId { get; set; }
        //public string Username { get; set; }

        public int RecievedById { get; set; }
        //public string RecievedByUsername { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string Attachment { get; set; }

        public DateTime Created { get; set; }

        public bool DeletedBySender { get; set; }
        public bool DeletedByRecipient { get; set; }

        public bool ReadByRecipient { get; set; }
    }
}
