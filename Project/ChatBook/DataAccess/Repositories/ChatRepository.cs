using ChatBook.Domain.Interfaces;
using ChatBook.Domain.Models;
using System.Collections.Generic;
using System.Linq;

namespace ChatBook.DataAccess.Repositories
{
    public class ChatRepository : IChatService
    {
        private readonly List<Message> _messages = new List<Message>();

        public void SendMessage(Message message)
        {
            _messages.Add(new Message
            {
                SenderId = message.SenderId,
                ReceiverId = message.ReceiverId,
                Content = message.Content,
                Timestamp = message.Timestamp
            });
        }

        public List<Message> GetChatHistory(int user1Id, int user2Id)
        {
            return _messages
                .Where(m => (m.SenderId == user1Id && m.ReceiverId == user2Id) ||
                            (m.SenderId == user2Id && m.ReceiverId == user1Id))
                .OrderBy(m => m.Timestamp)
                .ToList();
        }
    }
}
