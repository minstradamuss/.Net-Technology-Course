using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using ChatBook.Domain.Models;

namespace ChatBook.UI.Forms
{
    public partial class EditProfileForm : Form
    {
        private string _avatarPath;
        private User _currentUser;
        public event Action<User> ProfileUpdated;

        public EditProfileForm(User user)
        {
            InitializeComponent();
            _currentUser = user;

            txtFirstName.Text = user.FirstName;
            txtLastName.Text = user.LastName;
            txtPhone.Text = user.Phone;

            // Добавляем поле для отображения пути к аватару
            txtAvatarPath = new TextBox
            {
                Location = new Point(20, 160),
                Size = new Size(260, 22),
                ReadOnly = true // Запрещаем редактирование вручную
            };
            

            if (!string.IsNullOrEmpty(user.AvatarPath) && File.Exists(user.AvatarPath))
            {
                pictureBoxAvatar.Image = Image.FromFile(user.AvatarPath);
                _avatarPath = user.AvatarPath;
                txtAvatarPath.Text = _avatarPath;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtAvatarPath == null)
            {
                MessageBox.Show("Ошибка: Поле для аватара не инициализировано!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _currentUser.FirstName = txtFirstName.Text;
            _currentUser.LastName = txtLastName.Text;
            _currentUser.Phone = txtPhone.Text;
            _currentUser.AvatarPath = txtAvatarPath.Text; // Сохраняем путь в профиль

            ProfileUpdated?.Invoke(_currentUser);
            this.Close();
        }

        private void btnUploadAvatar_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Изображения|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    pictureBoxAvatar.Image = Image.FromFile(openFileDialog.FileName);
                    _avatarPath = openFileDialog.FileName;
                    txtAvatarPath.Text = _avatarPath; // Обновляем путь в текстовом поле
                }
            }
        }
    }
}
