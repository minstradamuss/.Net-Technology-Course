using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using ChatBook.Domain.Models;

namespace ChatBook.UI.Forms
{
    public partial class FriendProfileForm : Form
    {
        private User _user;
        private HashSet<string> _friends;
        private Button btnAddFriend;
        private FlowLayoutPanel flowLayoutPanelBooks;

        public FriendProfileForm(User user, HashSet<string> friends)
        {
            InitializeComponent();
            _user = user;
            _friends = friends;

            lblFullName.Text = $"{user.FirstName} {user.LastName}";
            lblNickname.Text = user.Nickname;

            if (!string.IsNullOrEmpty(user.AvatarPath) && File.Exists(user.AvatarPath))
            {
                pictureBoxAvatar.Image = Image.FromFile(user.AvatarPath);
            }
            else
            {
                pictureBoxAvatar.BackColor = Color.Gray;
            }

            LoadUserBooks();

            if (!_friends.Contains(user.Nickname))
            {
                btnAddFriend = new Button
                {
                    Text = "Добавить в друзья",
                    Dock = DockStyle.Bottom,
                    Height = 30
                };
                btnAddFriend.Click += BtnAddFriend_Click;
                Controls.Add(btnAddFriend);
            }
        }

        private void LoadUserBooks()
        {
            flowLayoutPanelBooks.Controls.Clear();
            List<string> userBooks = new List<string> { "Книга 1", "Книга 2", "Книга 3" };

            foreach (var bookTitle in userBooks)
            {
                Label bookLabel = new Label
                {
                    Text = bookTitle,
                    AutoSize = true,
                    Padding = new Padding(5)
                };
                flowLayoutPanelBooks.Controls.Add(bookLabel);
            }
        }

        private void BtnAddFriend_Click(object sender, EventArgs e)
        {
            _friends.Add(_user.Nickname);
            MessageBox.Show($"{_user.Nickname} добавлен в друзья!", "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
            btnAddFriend.Enabled = false;
        }
    }
}
