using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using ChatBook.Entities;
using ChatBook.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ChatBook.UI.Forms
{
    public partial class LoginForm : Form
    {
        private readonly ApiAuthClient _apiClient = new ApiAuthClient();
        private readonly UserService _userService;

        public LoginForm(UserService userService)
        {
            InitializeComponent();
            _userService = userService;
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            string nickname = txtNickname.Text.Trim();
            string password = txtPassword.Text.Trim();

            var userDto = await _apiClient.LoginAsync(nickname, password);

            if (userDto != null)
            {
                var existingUser = _userService.GetUserByNickname(nickname);

                if (existingUser == null)
                {
                    var newUser = new User
                    {
                        Nickname = nickname,
                        Password = password
                    };
                    _userService.Register(newUser);
                    existingUser = newUser;
                }

                AppSession.SetLoggedUser(existingUser);

                // ✅ Получаем MainForm из DI-контейнера
                var mainForm = Program.ServiceProvider.GetRequiredService<MainForm>();

                // ✅ Устанавливаем текущего пользователя после создания
                mainForm.SetCurrentUser(existingUser);

                this.Hide();
                mainForm.Show();
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private async void btnRegister_Click(object sender, EventArgs e)
        {
            string nickname = txtNickname.Text.Trim();
            string password = txtPassword.Text.Trim();

            var success = await _apiClient.RegisterAsync(nickname, password);

            if (success)
            {
                var user = new User
                {
                    Nickname = nickname,
                    Password = password
                };

                _userService.Register(user);

                MessageBox.Show("Регистрация прошла успешно!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Ошибка регистрации. Такой пользователь уже существует.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    public class ApiAuthClient
    {
        private readonly HttpClient _client = new HttpClient();

        public async Task<UserDto> LoginAsync(string username, string password)
        {
            var content = new StringContent(JsonSerializer.Serialize(new
            {
                Username = username,
                Password = password
            }), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("http://localhost:52695/api/auth/login", content);

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Login failed: {response.StatusCode}");
                return null;
            }

            var responseBody = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Response: {responseBody}");

            string token;
            using (JsonDocument doc = JsonDocument.Parse(responseBody))
            {
                token = doc.RootElement.GetProperty("token").GetString();
            }

            return new UserDto { Token = token };
        }



        public async Task<bool> RegisterAsync(string username, string password)
        {
            var content = new StringContent(JsonSerializer.Serialize(new
            {
                Username = username,
                Password = password
            }), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("http://localhost:52695/api/auth/register", content);
            return response.IsSuccessStatusCode;
        }
    }

    public class UserDto
    {
        public string Token { get; set; }
    }

}
