using ChatBook.Entities;
using ChatBook.Services;
using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;

namespace ChatBook.UI.Windows
{
    public partial class EditProfileWindow : Window
    {
        private User _currentUser;
        private readonly UserService _userService;
        private byte[] _avatarBytes;

        public event Action<User> ProfileUpdated;

        public EditProfileWindow(User user, UserService userService)
        {
            InitializeComponent();
            _currentUser = user ?? throw new ArgumentNullException(nameof(user));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));

            txtFirstName.Text = _currentUser.FirstName;
            txtLastName.Text = _currentUser.LastName;
            txtPhoneNumber.Text = _currentUser.PhoneNumber;
            if (_currentUser.Avatar != null)
            {
                _avatarBytes = _currentUser.Avatar;

                try
                {
                    BitmapImage bitmap = new BitmapImage();
                    using (var ms = new MemoryStream(_avatarBytes))
                    {
                        bitmap.BeginInit();
                        bitmap.CacheOption = BitmapCacheOption.OnLoad;
                        bitmap.CreateOptions = BitmapCreateOptions.IgnoreColorProfile; 
                        bitmap.StreamSource = ms;
                        bitmap.EndInit();
                        bitmap.Freeze(); 
                    }

                    imgAvatar.Source = bitmap;
                }
                catch (Exception ex)
                {
                    imgAvatar.Source = null; 
                }
            }



        }

        private void btnUploadAvatar_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog
            {
                Filter = "Images|*.jpg;*.jpeg;*.png;*.bmp;*.gif"
            };

            if (dialog.ShowDialog() == true)
            {
                try
                {
                    _avatarBytes = File.ReadAllBytes(dialog.FileName);
                    imgAvatar.Source = new BitmapImage(new Uri(dialog.FileName));
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка загрузки изображения: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
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
                MessageBox.Show("Ошибка при обновлении профиля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
