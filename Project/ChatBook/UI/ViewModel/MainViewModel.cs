using ChatBook.Domain.Services;
using ChatBook.Entities;
using System.Collections.Generic;

namespace ChatBook.ViewModels
{
    public class MainViewModel
    {
        private readonly UserService _userService;

        public MainViewModel(UserService userService)
        {
            _userService = userService;
        }

        public List<Book> GetUserBooks(string nickname)
        {
            return _userService.GetUserBooks(nickname);
        }

        public User GetUser(string nickname)
        {
            return _userService.GetUserByNickname(nickname);
        }

        public bool RemoveFriend(string userNickname, string friendNickname)
        {
            return _userService.RemoveFriend(userNickname, friendNickname);
        }

        public List<Entities.Message> GetMessages(string from, string to)
        {
            return _userService.GetChatMessages(from, to);
        }

        public List<Message> GetChatMessages(string nickname1, string nickname2)
        {
            return _userService.GetChatMessages(nickname1, nickname2);
        }

        public void AddBook(Book book, string nickname)
        {
            _userService.AddBook(book, nickname);
        }

        public void UpdateBook(Book book)
        {
            _userService.UpdateBook(book);
        }

        public void DeleteBook(int id)
        {
            _userService.DeleteBook(id);
        }

        public bool UpdateProfile(User user)
        {
            if (user == null)
                return false;

            return _userService.UpdateProfile(user);
        }
    }
}
