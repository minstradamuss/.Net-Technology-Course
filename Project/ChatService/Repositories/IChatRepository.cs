using ChatService.Entities;
using System.Collections.Generic;

namespace ChatService.Repositories
{
    public interface IChatRepository
    {
        void SaveMessage(Message message);
        List<Message> GetMessagesBetween(string user1, string user2);
    }
}
