using ChatBook.Domain.Interfaces;
using ChatBook.Domain.Models;
using System.Collections.Generic;

namespace ChatBook.DataAccess.Repositories
{
    public class UserRepository : IUserService
    {
        private readonly List<User> _users = new List<User>();


        public void Register(string nickname, string password)
        {
            _users.Add(new User { Id = _users.Count + 1, Nickname = nickname, Password = password });
        }

        public User Login(string nickname, string password)
        {
            return _users.Find(user => user.Nickname == nickname && user.Password == password);
        }
    }
}
