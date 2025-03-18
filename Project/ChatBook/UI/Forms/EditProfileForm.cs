using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using ChatBook.Domain.Models;

namespace ChatBook.UI.Forms
{
    public partial class EditProfileForm : Form
    {
        public event Action<User> ProfileUpdated;
        private User _currentUser;
        private byte[] _avatarBytes;

        public EditProfileForm(User user)
        {
            InitializeComponent();
            _currentUser = user;

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
