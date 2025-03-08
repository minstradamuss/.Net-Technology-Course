using System;
using System.IO;
using ChatBook.Domain.Models;

namespace ChatBook.Data
{
    public class UserRepository
    {
        public User LoadUser(string username)
        {
            // Здесь должен быть код для загрузки пользователя, например, из БД или JSON-файла.
            return new User
            {
                Nickname = username,
                FirstName = "Имя",
                LastName = "Фамилия",
                Phone = "123456789",
                AvatarPath = "default_avatar.jpg"
            };
        }

        public void SaveUser(User user)
        {
            // Здесь должен быть код для сохранения пользователя, например, в БД.
            Console.WriteLine($"Пользователь {user.Nickname} сохранен.");
        }
    }
}
