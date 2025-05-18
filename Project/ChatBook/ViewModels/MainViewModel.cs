using ChatBook.Entities;
using ChatBook.Services;
using System.Collections.Generic;

namespace ChatBook.ViewModels
{
    public class MainViewModel
    {
        private readonly UserService _userService;
        public UserService UserService => _userService;
        public MainViewModel(UserService userService)
        {
            _userService = userService;
        }

        public List<Book> GetUserBooks(string nickname)
        {
            return _userService.GetUserBooks(nickname);
        }

        public bool RemoveFriend(string current, string friend)
        {
            return _userService.RemoveFriend(current, friend);
        }

        public List<Message> GetChatMessages(string from, string to)
        {
            return _userService.GetChatMessages(from, to);
        }

        public List<User> GetFriends(string nickname)
        {
            return _userService.GetFriends(nickname);
        }

        public User GetUserByNickname(string nickname)
        {
            return _userService.GetUserByNickname(nickname);
        }

        public void SaveMessage(Message message)
        {
            _userService.SaveMessage(message);
        }
    }
}
