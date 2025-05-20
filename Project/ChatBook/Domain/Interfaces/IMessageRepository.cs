using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatBook.Domain.Interfaces
{
    using ChatBook.Entities;
    using System.Collections.Generic;

    public interface IMessageRepository
    {
        List<Message> GetMessages(string senderNickname, string receiverNickname);
        void SaveMessage(Message message);
        List<User> GetChatPartners(string nickname);
    }
}
