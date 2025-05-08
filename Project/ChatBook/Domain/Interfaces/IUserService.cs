using ChatBook.Entities;

namespace ChatBook.Domain.Interfaces
{
    public interface IUserService
    {
        void Register(string nickname, string password);
        User Login(string nickname, string password);
    }
}
