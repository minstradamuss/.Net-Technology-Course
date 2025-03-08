using ChatBook.Domain.Models;
using System.Collections.Generic;

namespace ChatBook.Domain.Interfaces
{
    public interface IChatService
    {
        void SendMessage(Message message);
        List<Message> GetChatHistory(int user1Id, int user2Id);
    }
}
