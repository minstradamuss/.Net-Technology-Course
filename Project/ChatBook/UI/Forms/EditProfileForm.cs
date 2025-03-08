using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using ChatBook.Domain.Models;

namespace ChatBook.UI.Forms
{
    public partial class EditProfileForm : Form
    {
        public event Action<string, string, string, string> ProfileUpdated;
        private string _avatarPath;
        private User _currentUser;

        public EditProfileForm(User user)
        {
            InitializeComponent();
            _currentUser = user;
            txtFirstName.Text = user.FirstName;
            txtLastName.Text = user.LastName;
            txtPhone.Text = user.Phone;

            if (!string.IsNullOrEmpty(user.AvatarPath) && File.Exists(user.AvatarPath))
            {
                pictureBoxAvatar.Image = Image.FromFile(user.AvatarPath);
                _avatarPath = user.AvatarPath;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            ProfileUpdated?.Invoke(txtFirstName.Text, txtLastName.Text, txtPhone.Text, _avatarPath);
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
                }
            }
        }
    }
}
