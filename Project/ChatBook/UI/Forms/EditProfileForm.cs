using ChatBook.Services;
using ChatBook.Domain.Models;
using System;
using System.IO;
using System.Windows.Forms;
using System.Drawing;

namespace ChatBook.UI.Forms
{
    public partial class EditProfileForm : Form
    {
        public event Action<User> ProfileUpdated;
        private User _currentUser;
        private byte[] _avatarBytes;
        private readonly UserService _userService;

        public EditProfileForm(User user, UserService userService)
        {
            InitializeComponent();
            _currentUser = user;
            _userService = userService;

            txtFirstName.Text = user.FirstName;
            txtLastName.Text = user.LastName;
            txtPhoneNumber.Text = user.PhoneNumber;

            if (user.Avatar != null && user.Avatar.Length > 0)
            {
                pictureBoxAvatar.Image = ConvertByteArrayToImage(user.Avatar);
                _avatarBytes = user.Avatar;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            _currentUser.FirstName = txtFirstName.Text;
            _currentUser.LastName = txtLastName.Text;
            _currentUser.PhoneNumber = txtPhoneNumber.Text;
            _currentUser.Avatar = _avatarBytes;

            bool isUpdated = _userService.UpdateProfile(_currentUser);
            if (isUpdated)
            {
                ProfileUpdated?.Invoke(_currentUser);
                this.Close();
            }
            else
            {
                MessageBox.Show("Ошибка при обновлении профиля.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUploadAvatar_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Изображения|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    _avatarBytes = ConvertImageToByteArray(openFileDialog.FileName);
                    pictureBoxAvatar.Image = Image.FromFile(openFileDialog.FileName);
                    pictureBoxAvatar.SizeMode = PictureBoxSizeMode.Zoom;
                }
            }
        }

        private byte[] ConvertImageToByteArray(string imagePath)
        {
            if (string.IsNullOrEmpty(imagePath) || !File.Exists(imagePath))
                return null;

            try
            {
                return File.ReadAllBytes(imagePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки изображения: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        private Image ConvertByteArrayToImage(byte[] imageData)
        {
            if (imageData == null || imageData.Length == 0)
                return null;

            try
            {
                using (MemoryStream ms = new MemoryStream(imageData))
                {
                    return Image.FromStream(ms);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки изображения: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
    }
}
