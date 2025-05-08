using ChatBook.DataAccess;
using ChatBook.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Runtime.Remoting.Contexts;

namespace ChatBook.Services
{
    public class UserService
    {
        private readonly ApplicationDbContext _dbContext;

        public UserService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool Register(User user)
        {
            var existingUser = _dbContext.Users.FirstOrDefault(u => u.Nickname == user.Nickname);
            if (existingUser != null) return false;

            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
            return true;
        }

        public User GetUserByNickname(string nickname)
        {
            return _dbContext.Users.Include(u => u.Profile).FirstOrDefault(u => u.Nickname == nickname);
        }

        public bool UpdateProfile(User user)
        {
            var existingUser = _dbContext.Users.FirstOrDefault(u => u.Id == user.Id);
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
            if (user == null) return false;

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
                .Where(f => f.User1Id == user.Id || f.User2Id == user.Id)
                .Select(f => f.User1Id == user.Id ? f.User2Id : f.User1Id)
                .ToList();

            return _dbContext.Users.Where(u => friendIds.Contains(u.Id)).ToList();
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

        public bool RemoveFriend(string currentUserNickname, string friendNickname)
        {
            var currentUser = _dbContext.Users.FirstOrDefault(u => u.Nickname == currentUserNickname);
            var friend = _dbContext.Users.FirstOrDefault(u => u.Nickname == friendNickname);

            if (currentUser == null || friend == null) return false;

            var friendship = _dbContext.Friendships
                .FirstOrDefault(f => (f.User1Id == currentUser.Id && f.User2Id == friend.Id) ||
                                     (f.User1Id == friend.Id && f.User2Id == currentUser.Id));

            if (friendship == null) return false;

            _dbContext.Friendships.Remove(friendship);
            _dbContext.SaveChanges();

            return true;
        }



        public bool DeleteBook(int bookId)
        {
            var book = _dbContext.Books.FirstOrDefault(b => b.Id == bookId);
            if (book == null) return false;

            _dbContext.Books.Remove(book);
            _dbContext.SaveChanges();
            return true;
        }

       


        public List<ChatBook.Entities.Message> GetChatMessages(int userId, int friendId)
        {
            return _dbContext.Messages
                .Where(m => (m.SenderId == userId && m.ReceiverId == friendId) ||
                            (m.SenderId == friendId && m.ReceiverId == userId))
                .OrderBy(m => m.Timestamp)
                .ToList();
        }

        public bool SendMessage(ChatBook.Entities.Message message)
        {
            try
            {
                _dbContext.Messages.Add(message);
                _dbContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<ChatBook.Entities.Message> GetChatMessages(string senderNickname, string receiverNickname)
        {
            var sender = _dbContext.Users.FirstOrDefault(u => u.Nickname == senderNickname);
            var receiver = _dbContext.Users.FirstOrDefault(u => u.Nickname == receiverNickname);

            if (sender == null || receiver == null)
                return new List<ChatBook.Entities.Message>();

            return _dbContext.Messages
                .Where(m => (m.SenderId == sender.Id && m.ReceiverId == receiver.Id) ||
                            (m.SenderId == receiver.Id && m.ReceiverId == sender.Id))
                .OrderBy(m => m.Timestamp)
                .ToList();
        }

        public void SaveMessage(ChatBook.Entities.Message message)
        {
            _dbContext.Messages.Add(message);
            _dbContext.SaveChanges();
        }

        public List<BookWithReview> SearchBooksWithReviews(string titleQuery)
        {
            return _dbContext.Books
                .Include(b => b.User) // Подгружаем связанного пользователя
                .Where(b =>
                    b.Status == "Read" && // или "Read", в зависимости от языка
                    b.Title.ToLower().Contains(titleQuery.ToLower()))
                .Select(b => new BookWithReview
                {
                    Book = b,
                    ReviewerNickname = b.User.Nickname,
                    Review = b.Review
                })
                .ToList();
        }






        public List<User> GetAllChatPartners(string userNickname)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Nickname == userNickname);
            if (user == null) return new List<User>();

            var chatPartners = _dbContext.Messages
                .Where(m => m.SenderId == user.Id || m.ReceiverId == user.Id)
                .Select(m => m.SenderId == user.Id ? m.Receiver : m.Sender)
                .Distinct()
                .ToList();

            return chatPartners;
        }

        public Book GetBookByUserAndTitle(int userId, string bookTitle)
        {
            return _dbContext.Books
                             .FirstOrDefault(b => b.UserId == userId && b.Title == bookTitle);
        }


    }

}
