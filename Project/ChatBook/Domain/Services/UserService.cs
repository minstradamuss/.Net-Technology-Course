using ChatBook.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ChatBook.Services
{
    public class UserService
    {
        private readonly ChatBookDbContext _dbContext;

        public UserService(ChatBookDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public User GetUserByNickname(string nickname)
        {
            return _dbContext.Users
                .Where(u => u.Nickname == nickname)
                .FirstOrDefault();
        }


        public bool Register(User user)
        {
            var existingUser = _dbContext.Users.FirstOrDefault(u => u.Nickname == user.Nickname);
            if (existingUser != null)
            {
                return false;
            }

            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
            return true;
        }

        public bool UpdateProfile(User user)
        {
            var existingUser = _dbContext.Users.FirstOrDefault(u => u.Nickname == user.Nickname);
            if (existingUser != null)
            {
                existingUser.FirstName = user.FirstName;
                existingUser.LastName = user.LastName;
                existingUser.PhoneNumber = user.PhoneNumber;
                existingUser.Avatar = user.Avatar;

                _dbContext.SaveChanges();
                return true;
            }

            return false;
        }

        public bool AddBook(Book book, string username)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Nickname == username);
            if (user == null)
            {
                return false;
            }

            book.UserId = user.Id;
            _dbContext.Books.Add(book);
            _dbContext.SaveChanges();
            return true;
        }

        public List<Book> GetUserBooks(string username)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Nickname == username);
            if (user == null) return new List<Book>();

            return _dbContext.Books.Where(b => b.UserId == user.Id).ToList();
        }

        public List<User> GetFriends(string username)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Nickname == username);
            if (user == null) return new List<User>();

            var friendIds = _dbContext.Friendships
                .Where(f => f.User1Id == user.Id || f.User2Id == user.Id) // ✅ Проверяем по ID
                .Select(f => f.User1Id == user.Id ? f.User2Id : f.User1Id) // ✅ Получаем ID друга
                .ToList();

            return _dbContext.Users.Where(u => friendIds.Contains(u.Id)).ToList(); // ✅ Загружаем друзей
        }


        public List<User> SearchUsers(string searchNickname)
        {
            return _dbContext.Users
                .Where(u => u.Nickname.ToLower().Contains(searchNickname.ToLower()))
                .ToList();
        }

        public List<Book> GetReadBooks(string username)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Nickname == username);
            if (user == null) return new List<Book>(); // ❌ Если нет пользователя, вернем пустой список

            var books = _dbContext.Books
                .Where(b => b.UserId == user.Id && b.Status == "Прочитано") // ❌ Фильтр по "Прочитано"
                .ToList();

            Console.WriteLine($"Найдено {books.Count} книг для {username}"); // ✅ Отладка

            return books;
        }


        public bool AddFriend(string userNickname, string friendNickname)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Nickname == userNickname);
            var friend = _dbContext.Users.FirstOrDefault(u => u.Nickname == friendNickname);

            if (user == null || friend == null) return false;

            if (AreFriends(userNickname, friendNickname)) return false;

            _dbContext.Friendships.Add(new Friendship
            {
                User1Id = user.Id,
                User2Id = friend.Id
            });

            _dbContext.SaveChanges();
            return true;
        }


        public bool UpdateBook(Book updatedBook)
        {
            var existingBook = _dbContext.Books.FirstOrDefault(b => b.Id == updatedBook.Id);
            if (existingBook == null) return false;

            existingBook.Title = updatedBook.Title;
            existingBook.Status = updatedBook.Status;
            existingBook.Rating = updatedBook.Rating;
            existingBook.Review = updatedBook.Review;
            existingBook.CoverImage = updatedBook.CoverImage;

            _dbContext.SaveChanges();
            return true;
        }
        public bool AreFriends(string userNickname, string friendNickname)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Nickname == userNickname);
            var friend = _dbContext.Users.FirstOrDefault(u => u.Nickname == friendNickname);

            if (user == null || friend == null) return false;

            return _dbContext.Friendships
                .Any(f => (f.User1Id == user.Id && f.User2Id == friend.Id) ||
                          (f.User1Id == friend.Id && f.User2Id == user.Id));
        }


    }

}
