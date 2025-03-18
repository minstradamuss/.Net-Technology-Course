using ChatBook.Services;
using ChatBook.Domain.Models;

namespace ChatBook.UI.ViewModels
{
    public class RegisterViewModel
    {
        private readonly UserService _userService;

        // Конструктор, который принимает UserService через DI
        public RegisterViewModel(UserService userService)
        {
            _userService = userService;
        }

        public string Nickname { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }

        public bool RegisterUser()
        {
            User user = new User
            {
                Nickname = Nickname,
                FirstName = FirstName,
                LastName = LastName,
                Password = Password
            };

            return _userService.Register(user);
        }
    }
}
