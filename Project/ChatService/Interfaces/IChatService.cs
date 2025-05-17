using ChatService.Entities;
using System.Collections.Generic;

namespace ChatService.Interfaces
{
    public interface IChatService
    {
        void SendMessage(string fromUser, string toUser, string text);
        List<Message> GetChatHistory(string user1, string user2);
    }
}
