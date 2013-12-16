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
            //message.Username = _context.Users.FirstOrDefault(u => u.UserId == message.UserId).UserName;
            //message.RecievedByUsername = _context.Users.FirstOrDefault(u => u.UserId == message.RecievedById).UserName;

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

        public ICollection<Message> GetSentMessages(string username)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserName.ToLower() == username.ToLower());

            if (user != null)
            {
                var messages = _context.Messages
                            .Where(u => u.UserId == user.UserId)
                            .Where(d => d.DeletedBySender == false)
                            .OrderByDescending(d => d.Created);

                return messages.ToList();
            }

            return new List<Message>();
        }

        public ICollection<Message> GetRecievedMessages(string username)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserName.ToLower() == username.ToLower());

            if (user != null)
            {
                var messages = _context.Messages
                            .Where(u => u.RecievedById == user.UserId)
                            .Where(d => d.DeletedByRecipient == false)
                            .OrderByDescending(d => d.Created);

                return messages.ToList();
            }

            return new List<Message>();
        }

        public ICollection<Message> GetDeletedMessages(string username)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserName.ToLower() == username.ToLower());

            if (user != null)
            {
                var messages = _context.Messages
                                .Where(u => u.RecievedById == user.UserId)
                                .Where(d => d.DeletedByRecipient == true)
                                .Where(d => d.DeletedFromTrash == false)
                                .OrderByDescending(d => d.Created);

                return messages.ToList();
            }

            return new List<Message>();
        }

        public bool DeleteSentMessage(int messageId, string username)
        {
            var result = false;

            var userId = _context.Users.FirstOrDefault(u => u.UserName.ToLower() == username.ToLower()).UserId;
            var message = _context.Messages.Find(messageId);
            
            if (message != null && userId > 0)
            {
                message.DeletedBySender = true;

                _context.Entry(message).State = System.Data.EntityState.Modified;
                result = true;
            }

            return result;
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
        public Message GetMessageById(int messageId, string username)
        {
            // get  userId from username
            var userId = _context.Users.FirstOrDefault(u => u.UserName.ToLower() == username.ToLower()).UserId;

            var message = _context.Messages.FirstOrDefault(u => u.MessageId == messageId);

            if (message != null && userId > 0)
            {
                if (message.RecievedById == userId || message.UserId == userId)
                {
                    return message;
                }
                return null;
            }

            return message;
        }

        public bool MarkMessageAsReadByRecipient(int messageId, string username)
        {
            // get  userId from username
            var userId = _context.Users.FirstOrDefault(u => u.UserName.ToLower() == username.ToLower()).UserId;

            var message = _context.Messages.FirstOrDefault(u => u.MessageId == messageId);
            var result = false;

            if (message != null && userId > 0)
            {
                if (message.RecievedById == userId)
                {
                    message.ReadByRecipient = true;
                    _context.Entry(message).State = System.Data.EntityState.Modified;

                    result = true;
                }
            }

            return result;
        }

        public bool MarkMessageAsUnReadByRecipient(int messageId, string username)
        {
            // get  userId from username
            var userId = _context.Users.FirstOrDefault(u => u.UserName.ToLower() == username.ToLower()).UserId;

            var message = _context.Messages.FirstOrDefault(u => u.MessageId == messageId);
            var result = false;

            if (message != null && userId > 0)
            {
                if (message.RecievedById == userId)
                {
                    message.ReadByRecipient = false;
                    _context.Entry(message).State = System.Data.EntityState.Modified;

                    result = true;
                }
            }

            return result;
        }

        // 'deletes' the message, based on the user.
        // i.e if user of message is recipient, sets deletedbyrecipient to true
        //  if user of message is sender, sets deletedbysender to true
        // if both sets both true. (sending message to self)
        public bool DeleteMessage(int messageId, string username)
        {
            // get  userId from username
            var userId = _context.Users.FirstOrDefault(u => u.UserName.ToLower() == username.ToLower()).UserId;

            var message = _context.Messages.Find(messageId);
            var result = false;

            if (message != null && userId > 0)
            {
                if (message.UserId == userId)
                {
                    message.DeletedBySender = true;
                }
                if (message.RecievedById == userId)
                {
                    message.DeletedByRecipient = true;
                }

                _context.Entry(message).State = System.Data.EntityState.Modified;

                result = true;
            }

            return result;
        }

        public bool DeleteFromTrash(int messageId, string username)
        {
            var userId = _context.Users.FirstOrDefault(u => u.UserName.ToLower() == username.ToLower()).UserId;

            var message = _context.Messages.Find(messageId);

            var result = false;

            if (message != null && userId > 0)
            {
                message.DeletedFromTrash = true;
                _context.Entry(message).State = System.Data.EntityState.Modified;

                result = true;
            }

            return result;
        }

        public bool UnDeleteMessage(int messageId, string username)
        {
            var userId = _context.Users.FirstOrDefault(u => u.UserName.ToLower() == username.ToLower()).UserId;

            var message = _context.Messages.Find(messageId);

            var result = false;

            if (message != null && userId > 0)
            {
                message.DeletedByRecipient = false;

                _context.Entry(message).State = System.Data.EntityState.Modified;
                result = true;
            }

            return result;
        }
    }
}
