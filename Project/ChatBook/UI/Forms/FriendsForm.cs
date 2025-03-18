using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ChatBook.Domain.Models;

namespace ChatBook.UI.Forms
{
    public partial class FriendsForm : Form
    {
        private Dictionary<string, User> allUsers = new Dictionary<string, User>
        {
            { "testUser", new User { Nickname = "testUser", FirstName = "John", LastName = "Doe", AvatarPath = "avatar.jpg" } },
            { "testUsersearch", new User { Nickname = "testUsersearch", FirstName = "Test", LastName = "Search", AvatarPath = "avatar2.jpg" } }
        };

        private HashSet<string> friends = new HashSet<string> { "testUser" };

        public FriendsForm()
        {
            InitializeComponent();
            LoadFriends();
        }

        private void LoadFriends()
        {
            flowLayoutPanelFriends.Controls.Clear();
            foreach (var nickname in friends)
            {
                if (allUsers.ContainsKey(nickname))
                {
                    Panel friendPanel = CreateFriendPanel(allUsers[nickname]);
                    flowLayoutPanelFriends.Controls.Add(friendPanel);
                }
            }
        }

        private Panel CreateFriendPanel(User user)
        {
            Panel panel = new Panel
            {
                Size = new Size(150, 180),
                BorderStyle = BorderStyle.FixedSingle,
                Margin = new Padding(10),
                Tag = user
            };

            PictureBox avatar = new PictureBox
            {
                Size = new Size(120, 120),
                BorderStyle = BorderStyle.FixedSingle,
                SizeMode = PictureBoxSizeMode.Zoom,
                Location = new Point(15, 10),
                Tag = user
            };

            if (!string.IsNullOrEmpty(user.AvatarPath) && System.IO.File.Exists(user.AvatarPath))
            {
                avatar.Image = Image.FromFile(user.AvatarPath);
            }
            else
            {
                avatar.BackColor = Color.Gray;
            }

            Label nicknameLabel = new Label
            {
                Text = user.Nickname,
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Bottom,
                Height = 20,
                Tag = user
            };

            Label fullNameLabel = new Label
            {
                Text = $"{user.FirstName} {user.LastName}",
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Bottom,
                Height = 20,
                Tag = user
            };

            panel.DoubleClick += OpenUserProfile;
            avatar.DoubleClick += OpenUserProfile;
            nicknameLabel.DoubleClick += OpenUserProfile;
            fullNameLabel.DoubleClick += OpenUserProfile;

            panel.Controls.Add(avatar);
            panel.Controls.Add(nicknameLabel);
            panel.Controls.Add(fullNameLabel);

            return panel;
        }

        private void OpenUserProfile(object sender, EventArgs e)
        {
            User user = null;

            if (sender is Panel panel && panel.Tag is User panelUser)
                user = panelUser;
            else if (sender is PictureBox pictureBox && pictureBox.Tag is User pictureUser)
                user = pictureUser;
            else if (sender is Label label && label.Tag is User labelUser)
                user = labelUser;

            if (user == null) return;

            var userProfile = new FriendProfileForm(user, friends);
            userProfile.ShowDialog();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchNickname = txtSearch.Text.Trim().ToLower();

            if (string.IsNullOrEmpty(searchNickname))
            {
                MessageBox.Show("Введите никнейм для поиска.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var filteredUsers = allUsers.Values
                .Where(user => user.Nickname.ToLower().Contains(searchNickname))
                .ToList();

            flowLayoutPanelFriends.Controls.Clear();

            if (filteredUsers.Count == 0)
            {
                MessageBox.Show("Пользователь не найден.", "Результаты поиска", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            foreach (var user in filteredUsers)
            {
                flowLayoutPanelFriends.Controls.Add(CreateFriendPanel(user));
            }
        }

        private void btnShowFriends_Click(object sender, EventArgs e)
        {
            LoadFriends();
        }
    }
}
