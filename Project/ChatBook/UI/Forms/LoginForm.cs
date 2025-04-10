﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ChatBook.UI.Forms
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string nickname = txtNickname.Text;
            string password = txtPassword.Text;

            if (users.ContainsKey(nickname) && users[nickname] == password)
            {
                MainForm mainForm = new MainForm(nickname);
                mainForm.Show();
                Hide();
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль!");
            }
        }

        private static Dictionary<string, string> users = new Dictionary<string, string>(); // 🔹 Временное хранилище пользователей

        private void btnRegister_Click(object sender, EventArgs e)
        {
            string nickname = txtNickname.Text;
            string password = txtPassword.Text;

            if (users.ContainsKey(nickname))
            {
                MessageBox.Show("Этот логин уже зарегистрирован!");
            }
            else
            {
                users[nickname] = password;
                MessageBox.Show("Регистрация успешна!");
            }
        }

    }
}
