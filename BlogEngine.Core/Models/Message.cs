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

        public int SentById { get; set; }
        public virtual User SentByUser { get; set; }

        public int RecievedById { get; set; }
        public virtual User RecievedByUser { get; set; }

        public string Subject { get; set; }
        public string Body { get; set; }
        public string Attachment { get; set; }

        public DateTime Created { get; set; }

        public bool DeletedBySender { get; set; }
        public bool DeletedByRecipient { get; set; }

        public bool ReadByRecipient { get; set; }
    }
}
