using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using ChatBook.Entities;
using ChatBook.Services;
using ChatBook.UI.Windows;
using ChatBook.ViewModels;
using ChatService.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace ChatBook.UI.Forms
{
    public partial class MainForm : Form
    {
        private User _currentUser;
        private Dictionary<Book, Panel> _userBooks = new Dictionary<Book, Panel>();
        private FlowLayoutPanel flowLayoutPanelBooks;
        private Button btnSendMessage;
        private User _logged;
        private bool _isProfileViewOnly = false;
        private ChatForm _chatForm;

        private readonly MainViewModel _viewModel;
        private readonly IChatService _chatService;

        public string CurrentUserNickname { get; private set; }

        public MainForm(MainViewModel viewModel, IChatService chatService)
        {
            InitializeComponent();
            _viewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
            _chatService = chatService ?? throw new ArgumentNullException(nameof(chatService));
            _logged = AppSession.LoggedUser;

            InitializeFlowLayoutPanel();
        }

        public void SetCurrentUser(User user, bool isViewOnly = false)
        {
            _currentUser = user ?? throw new ArgumentNullException(nameof(user));
            _logged = AppSession.LoggedUser;
            _isProfileViewOnly = isViewOnly;
            CurrentUserNickname = _currentUser.Nickname;

            UpdateProfileInfo(_currentUser);
            lblNickname.Text = _currentUser.Nickname;
            LoadUserBooks();

            btnAddBook.Visible = !isViewOnly;
            btnEditProfile.Visible = !isViewOnly;
            buttonChats.Visible = !isViewOnly;
            btnSearchBooks.Visible = !isViewOnly;
            buttonSearchFriends.Visible = !isViewOnly;
            btnRemoveFriend.Visible = isViewOnly;

            if (isViewOnly)
                InitializeSendMessageButton();
        }

        private void InitializeFlowLayoutPanel()
        {
            flowLayoutPanelBooks = new FlowLayoutPanel
            {
                AutoScroll = true,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = true,
                Location = new Point(20, 200),
                Size = new Size(650, 300)
            };
            Controls.Add(flowLayoutPanelBooks);
        }

        private void UpdateProfileInfo(User updatedUser)
        {
            if (updatedUser == null) return;

            lblFullName.Text = $"{updatedUser.FirstName} {updatedUser.LastName}";
            pictureBoxAvatar.Image?.Dispose();
            pictureBoxAvatar.Image = updatedUser.Avatar != null ? ConvertByteArrayToImage(updatedUser.Avatar) : null;
        }

        private void btnEditProfile_Click(object sender, EventArgs e)
        {
            var editProfileWindow = new EditProfileWindow(_currentUser, _viewModel.UserService);

            editProfileWindow.ProfileUpdated += (updatedUser) =>
            {
                _currentUser = updatedUser;
                UpdateProfileInfo(_currentUser);
            };

            editProfileWindow.ShowDialog();
        }

        private void btnAddBook_Click(object sender, EventArgs e)
        {
            // Открываем WPF окно на текущем UI потоке WinForms
            var window = new AddBookWindow(_viewModel.UserService, _logged, null, false);

            ElementHost.EnableModelessKeyboardInterop(window); // если нужно, для правильной клавиатурной работы

            var result = window.ShowDialog();
            if (result == true)
            {
                LoadUserBooks();
            }
        }

        private void LoadUserBooks()
        {
            flowLayoutPanelBooks.Controls.Clear();
            _userBooks.Clear();

            var books = _viewModel.GetUserBooks(_currentUser.Nickname);
            foreach (var book in books)
                AddBookForm_BookAdded(book);

            txtSearchBooks_TextChanged(null, null);
        }

        private void AddBookForm_BookAdded(Book book)
        {
            var panel = new Panel
            {
                Size = new Size(120, 180),
                BorderStyle = BorderStyle.FixedSingle,
                Margin = new Padding(10)
            };

            var pictureBox = new PictureBox
            {
                Size = new Size(100, 140),
                Location = new Point(10, 10),
                SizeMode = PictureBoxSizeMode.Zoom,
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = book.CoverImage == null ? Color.Gray : Color.White,
                Tag = book
            };

            if (book.CoverImage != null)
                pictureBox.Image = ConvertByteArrayToImage(book.CoverImage);

            pictureBox.Click += BookCover_Click;

            var label = new Label
            {
                Text = book.Title,
                Dock = DockStyle.Bottom,
                Height = 30,
                TextAlign = ContentAlignment.MiddleCenter
            };

            panel.Controls.Add(pictureBox);
            panel.Controls.Add(label);

            _userBooks[book] = panel;
            flowLayoutPanelBooks.Controls.Add(panel);
        }

        private void BookCover_Click(object sender, EventArgs e)
        {
            if (sender is PictureBox pic && pic.Tag is Book book)
            {
                bool readOnly = _logged.Nickname != _currentUser.Nickname;

                var thread = new System.Threading.Thread(() =>
                {
                    var window = new AddBookWindow(_viewModel.UserService, _currentUser, book, readOnly);
                    var result = window.ShowDialog();
                    if (result == true)
                        this.Invoke(new Action(() => LoadUserBooks()));
                });
                thread.SetApartmentState(System.Threading.ApartmentState.STA);
                thread.Start();
            }
        }

        private void buttonChats_Click(object sender, EventArgs e)
        {
            var chatForm = new ChatForm(_logged.Nickname, _viewModel.UserService, _chatService, _currentUser.Nickname);
            chatForm.Show();
        }

        private void buttonSearchFriends_Click(object sender, EventArgs e)
        {
            var friendsForm = new FriendsForm(_currentUser.Nickname, _viewModel.UserService);
            friendsForm.Show();
        }

        private void btnSearchBooks_Click(object sender, EventArgs e)
        {
            var thread = new System.Threading.Thread(() =>
            {
                var window = Program.ServiceProvider.GetRequiredService<BookSearchWindow>();
                window.ShowDialog();
            });
            thread.SetApartmentState(System.Threading.ApartmentState.STA);
            thread.Start();
        }

        private void btnRemoveFriend_Click(object sender, EventArgs e)
        {
            var confirm = MessageBox.Show($"Удалить {_currentUser.Nickname} из друзей?", "Подтвердите", MessageBoxButtons.YesNo);
            if (confirm == DialogResult.Yes && _viewModel.RemoveFriend(_logged.Nickname, _currentUser.Nickname))
            {
                btnRemoveFriend.Visible = false;
                this.Close();
            }
        }

        private void txtSearchBooks_TextChanged(object sender, EventArgs e)
        {
            string search = txtSearchBooks.Text.ToLower().Trim();
            foreach (var pair in _userBooks)
                pair.Value.Visible = pair.Key.Title.ToLower().Contains(search);
        }

        private void InitializeSendMessageButton()
        {
            btnSendMessage = new Button
            {
                Text = "Написать",
                Font = new Font("Arial", 10, FontStyle.Bold),
                Size = new Size(120, 30),
                Location = new Point(600, 60),
                BackColor = Color.LightBlue
            };
            btnSendMessage.Click += btnSendMessage_Click;
            Controls.Add(btnSendMessage);
        }

        private void btnSendMessage_Click(object sender, EventArgs e)
        {
            var chatForm = new ChatForm(_logged.Nickname, _viewModel.UserService, _chatService, _currentUser.Nickname);
            chatForm.Show();
        }

        private void txtSearchBooks_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearchBooks.Text))
            {
                txtSearchBooks.Text = "🔍 Искать книгу...";
                txtSearchBooks.ForeColor = Color.Gray;
            }
        }

        private void txtSearchBooks_Enter(object sender, EventArgs e)
        {
            if (txtSearchBooks.Text == "🔍 Искать книгу...")
            {
                txtSearchBooks.Text = "";
                txtSearchBooks.ForeColor = Color.Black;
            }
        }

        private Image ConvertByteArrayToImage(byte[] data)
        {
            using (var ms = new MemoryStream(data))
                return Image.FromStream(ms);
        }

        private void btnLogout_Click_1(object sender, EventArgs e)
        {
            this.Close();
            new LoginForm(_viewModel.UserService).Show();
        }
    }
}
