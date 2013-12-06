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

        public int GetUnreadMessageCount(string username)
        {
            int unreadCount = 0;

            var user = _context.Users.FirstOrDefault(u => u.UserName == username);
            if (user != null)
                unreadCount = _context.Messages.Where(u => u.RecievedById == user.UserId).Count(c => c.ReadByRecipient == false);

            return unreadCount;
        }

        public ICollection<Message> GetSentMessages(int userId)
        {
            var messages = _context.Messages
                        .Where(u => u.UserId == userId)
                        .Where(d => d.DeletedBySender == false);
            
            return messages.ToList();
        }

        public ICollection<Message> GetRecievedMessages(string username)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserName.ToLower() == username.ToLower());

            if (user != null)
            {
                var messages = _context.Messages
                            .Where(u => u.RecievedById == user.UserId)
                            .Where(d => d.DeletedByRecipient == false);

                return messages.ToList();
            }

            return new List<Message>();
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
