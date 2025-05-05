
using AuthService.Models;

namespace AuthService.Services
{
    public interface IUserService
    {
        UserDto? Authenticate(string username, string password);
        bool Register(string username, string password);
    }
}
