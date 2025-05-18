using ChatBook.Entities;
using ChatBook.Services;
using System;
using System.Collections.Generic;

namespace ChatBook.UI.ViewModels
{
    public class ChatViewModel
    {
        private readonly UserService _userService;
        private readonly string _currentUserNickname;

        public ChatViewModel(UserService userService, string currentUserNickname)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _currentUserNickname = currentUserNickname ?? throw new ArgumentNullException(nameof(currentUserNickname));
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
