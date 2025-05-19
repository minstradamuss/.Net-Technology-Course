using System;
using System.Collections.Generic;
using ChatBook.Entities;
using ChatBook.Services;

namespace ChatBook.ViewModels
{
    public class ChatViewModel
    {
        private readonly UserService _userService;

        public ChatViewModel(UserService userService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        public List<User> GetFriends(string nickname)
        {
            return _userService.GetFriends(nickname);
        }

        public List<User> GetAllChatPartners(string nickname)
        {
            return _userService.GetAllChatPartners(nickname);
        }

        public List<Message> GetChatMessages(string fromNickname, string toNickname)
        {
            return _userService.GetChatMessages(fromNickname, toNickname);
        }

        public User GetUser(string nickname)
        {
            return _userService.GetUserByNickname(nickname);
        }

        public void SaveMessage(Message message)
        {
            _userService.SaveMessage(message);
        }

        public User GetUserByNickname(string nickname)
        {
            return _userService.GetUserByNickname(nickname);
        }
    }
}
