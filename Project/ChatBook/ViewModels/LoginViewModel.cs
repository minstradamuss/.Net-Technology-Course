using ChatBook.Entities;
using ChatBook.Services;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ChatBook.ViewModels
{
    public class LoginViewModel
    {
        private readonly UserService _userService;
        private readonly HttpClient _httpClient = new HttpClient();

        public LoginViewModel(UserService userService)
        {
            _userService = userService;
        }

        public async Task<User> LoginAsync(string nickname, string password)
        {
            var content = new StringContent(JsonSerializer.Serialize(new
            {
                Username = nickname,
                Password = password
            }), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("http://localhost:52695/api/auth/login", content);

            if (!response.IsSuccessStatusCode)
                return null;

            var responseBody = await response.Content.ReadAsStringAsync();
            using (JsonDocument doc = JsonDocument.Parse(responseBody))
            {
                string token = doc.RootElement.GetProperty("token").GetString();

                var existingUser = _userService.GetUserByNickname(nickname);
                return existingUser;
            }
        }

        public async Task<bool> RegisterAsync(string nickname, string password)
        {
            var content = new StringContent(JsonSerializer.Serialize(new
            {
                Username = nickname,
                Password = password
            }), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("http://localhost:52695/api/auth/register", content);
            if (!response.IsSuccessStatusCode)
                return false;

            var user = new User
            {
                Nickname = nickname,
                Password = password
            };

            return _userService.Register(user);
        }
    }
}
