using BlogEngine.Core.Contexts;
using BlogEngine.Core.Infrastructure;
using BlogEngine.Core.Models;
using BlogEngine.Core.Work;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogEngine.Core.Repositorys
{
    public class MessageRepository : IMessageRepository
    {
        private readonly BlogContext _context;

        public MessageRepository(BlogContext context)
        {
            _context = context;
        }

        public bool Send(Message message)
        {
            _context.Messages.Add(message);

            return true;
        }

        public int GetUnreadMessageCount(int userId)
        {
            var unreadCount = _context.Messages.Where(u => u.RecievedById == userId).Count(c => c.ReadByRecipient == true);

            return unreadCount;
        }

        public ICollection<Message> GetSentMessages(int userId)
        {
            var messages = _context.Messages
                        .Where(u => u.SentById == userId)
                        .Where(d => d.DeletedBySender == false);
            
            return messages.ToList();
        }

        public ICollection<Message> GetRecievedMessages(int userId)
        {
            var messages = _context.Messages
                        .Where(u => u.RecievedById == userId)
                        .Where(d => d.DeletedByRecipient == false);

            return messages.ToList();
        }

        public bool DeleteSentMessage(int userId, int messageId)
        {
            var outcome = false;

            var message = _context.Messages.FirstOrDefault(u => u.MessageId == messageId);

            if (message != null)
            {
                message.DeletedBySender = true;
                _context.Entry(message).State = System.Data.EntityState.Modified;
                outcome = true;

                return outcome;
            }

            return outcome;
        }

        public bool DeleteRecievedMessage(int userId, int messageId)
        {
            var outcome = false;

            var message = _context.Messages.FirstOrDefault(u => u.MessageId == messageId);

            if (message != null)
            {
                message.DeletedByRecipient = true;
                _context.Entry(message).State = System.Data.EntityState.Modified;
                outcome = true;

                return outcome;
            }

            return outcome;
        }
    }
}
