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
            string nickname = txtNickname.Text;
            string password = txtPassword.Text;

            var user = _userService?.GetUserByNickname(nickname); // ✅ Загружаем полные данные

            if (user != null && user.Password == password)
            {
                // ✅ Передаём весь объект `User` в MainForm
                MainForm mainForm = new MainForm(user, _userService);
                mainForm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Неверный никнейм или пароль.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
