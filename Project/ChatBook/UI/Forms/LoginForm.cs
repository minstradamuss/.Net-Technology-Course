using ChatBook.Services;
using System;
using System.Windows.Forms;
using ChatBook.Domain.Models;

namespace ChatBook.UI.Forms
{
    public partial class LoginForm : Form
    {
        private readonly UserService _userService;
        public LoginForm(UserService userService)
        {
            InitializeComponent();
            _userService = userService;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string nickname = txtNickname.Text.Trim();
            string password = txtPassword.Text.Trim();

            var user = _userService.GetUserByNickname(nickname);

            if (user != null && user.Password == password) // ❗ Здесь добавь хеширование
            {
                // ✅ Устанавливаем глобального пользователя
                AppSession.SetLoggedUser(user);

                // ✅ Передаём его в `MainForm`
                MainForm mainForm = new MainForm(user, _userService);
                this.Hide();
                mainForm.Show();
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void btnRegister_Click(object sender, EventArgs e)
        {
            var user = new User
            {
                Nickname = txtNickname.Text,
                Password = txtPassword.Text 
            };

            bool isRegistered = _userService.Register(user);
            if (isRegistered)
            {
                MessageBox.Show("Регистрация прошла успешно!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Ошибка регистрации. Возможно, такой никнейм уже существует.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

}
