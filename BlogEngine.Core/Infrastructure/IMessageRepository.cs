using BlogEngine.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogEngine.Core.Infrastructure
{
    public interface IMessageRepository
    {
        bool Send(Message message);
        int GetUnreadMessageCount(string username);
        ICollection<Message> GetSentMessages(string username);
        ICollection<Message> GetRecievedMessages(string username);
        bool DeleteSentMessage(int userId, int messageId);
        bool DeleteRecievedMessage(int userId, int messageId);
        Message GetMessageById(int messageId, string username);

        bool MarkMessageAsReadByRecipient(int messageId, string username);

        bool MarkMessageAsUnReadByRecipient(int messageId, string username);

        bool DeleteMessage(int messageId, string username);

    }
}
