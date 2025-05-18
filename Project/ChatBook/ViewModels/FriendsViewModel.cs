using ChatBook.Entities;
using ChatBook.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatBook.ViewModels
{
    public class FriendsViewModel
    {
        private readonly UserService _userService;

        public FriendsViewModel(UserService userService)
        {
            _userService = userService;
        }

        public List<User> GetFriends(string nickname) => _userService.GetFriends(nickname);
        public List<User> SearchUsers(string query) => _userService.SearchUsers(query);
        public List<User> GetFollowers(string nickname) => _userService.GetFollowers(nickname);
        public bool AddFriend(string from, string to) => _userService.AddFriend(from, to);

        public bool AreFriends(string user1, string user2) =>
        _userService.AreFriends(user1, user2);
    }

}
