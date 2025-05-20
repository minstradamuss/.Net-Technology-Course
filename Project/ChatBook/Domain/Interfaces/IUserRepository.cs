using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatBook.Domain.Interfaces
{
    using ChatBook.Entities;
    using System.Collections.Generic;

    public interface IUserRepository
    {
        User GetByNickname(string nickname);
        bool Register(User user);
        bool UpdateProfile(User user);
        List<User> SearchUsers(string nickname);
        List<User> GetFriends(string nickname);
        List<User> GetFollowers(string nickname);
        bool AddFriend(string userNickname, string friendNickname);
        bool RemoveFriend(string userNickname, string friendNickname);
        bool AreFriends(string user1, string user2);
    }
}
