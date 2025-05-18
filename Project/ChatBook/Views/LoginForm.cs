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
        private readonly HttpClient _httpClient = new HttpClient();
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

            var content = new StringContent(JsonSerializer.Serialize(new
            {
                Username = nickname,
                Password = password
            }), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("http://localhost:52695/api/auth/login", content);

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                string token;

                using (JsonDocument doc = JsonDocument.Parse(responseBody))
                {
                    token = doc.RootElement.GetProperty("token").GetString();
                }

                var existingUser = _userService.GetUserByNickname(nickname);

                if (existingUser == null)
                {
                    MessageBox.Show("Пользователь не найден в системе. Пожалуйста, зарегистрируйтесь.",
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                AppSession.SetLoggedUser(existingUser);

                var mainForm = Program.ServiceProvider.GetRequiredService<MainForm>();
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

            var content = new StringContent(JsonSerializer.Serialize(new
            {
                Username = nickname,
                Password = password
            }), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("http://localhost:52695/api/auth/register", content);

            if (response.IsSuccessStatusCode)
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
}
