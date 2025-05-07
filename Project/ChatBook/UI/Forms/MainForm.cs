using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using ChatBook.Entities;
using ChatBook.Services;
using ChatBook.UI.Windows;
using Microsoft.Extensions.DependencyInjection;

namespace ChatBook.UI.Forms
{
    public partial class MainForm : Form
    {
        private User _currentUser;
        private Dictionary<Book, Panel> _userBooks = new Dictionary<Book, Panel>();
        private FlowLayoutPanel flowLayoutPanelBooks;
        private readonly UserService _userService;
        private User _logged;
        private Button btnSendMessage;

        public string CurrentUserNickname { get; private set; }

        public MainForm(User user, UserService userService, bool isProfileViewOnly = false)
        {
            InitializeComponent();
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _currentUser = user ?? throw new ArgumentNullException(nameof(user));

            _logged = AppSession.LoggedUser;

            CurrentUserNickname = _currentUser.Nickname;

            if (_currentUser == null)
            {
                MessageBox.Show("Ошибка: пользователь не найден!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
                return;
            }

            UpdateProfileInfo(_currentUser);
            lblNickname.Text = _currentUser.Nickname;
            InitializeFlowLayoutPanel();
            LoadUserBooks();

            if (isProfileViewOnly)
            {
                btnAddBook.Visible = false;
                btnEditProfile.Visible = false;
                buttonChats.Visible = false;
                btnSearchBooks.Visible = false;
                buttonSearchFriends.Visible = false;
                btnRemoveFriend.Visible = true;
                btnRefreshBooks.Visible = false;
                InitializeSendMessageButton();
            }
            LoadUserBooks();
            InitializeRefreshButton();
        }

        private void btnRefreshBooks_Click(object sender, EventArgs e)
        {
            LoadUserBooks();
        }

        private void InitializeRefreshButton()
        {
            btnRefreshBooks.FlatAppearance.BorderSize = 0;
            btnRefreshBooks.Click += btnRefreshBooks_Click;
            Controls.Add(btnRefreshBooks);
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

        private void InitializeAddBookButton()
        {
            btnAddBook.Click += btnAddBook_Click;
            Controls.Add(btnAddBook);
        }

        private void btnEditProfile_Click(object sender, EventArgs e)
        {
            var editProfileWindow = new EditProfileWindow(_currentUser, _userService);

            editProfileWindow.ProfileUpdated += (updatedUser) =>
            {
                _currentUser = updatedUser; // обязательно обнови ссылку
                UpdateProfileInfo(_currentUser); // обнови отображение
            };

            editProfileWindow.ShowDialog();
        }






        private void UpdateProfileInfo(User updatedUser)
        {
            if (updatedUser == null) return;

            _currentUser = updatedUser;
            lblFullName.Text = $"{_currentUser.FirstName} {_currentUser.LastName}";
            if (_currentUser.Avatar != null && _currentUser.Avatar.Length > 0)
            {
                using (var ms = new MemoryStream(_currentUser.Avatar))
                {
                    var img = Image.FromStream(ms);
                    pictureBoxAvatar.Image?.Dispose(); // очистить старую
                    pictureBoxAvatar.Image = new Bitmap(img); // обновить
                }
            }
            else
            {
                pictureBoxAvatar.Image?.Dispose();
                pictureBoxAvatar.Image = null;
            }
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.Close();
            LoginForm loginForm = new LoginForm(_userService);
            loginForm.Show();
        }

        private void AddBookForm_BookAdded(Book newBook)
        {
            Panel bookPanel = new Panel
            {
                Size = new Size(120, 180),
                BorderStyle = BorderStyle.FixedSingle,
                Margin = new Padding(10)
            };

            PictureBox bookCover = new PictureBox
            {
                Size = new Size(100, 140),
                BorderStyle = BorderStyle.FixedSingle,
                SizeMode = PictureBoxSizeMode.Zoom,
                Location = new Point(10, 10),
                Tag = newBook
            };

            Label bookTitle = new Label
            {
                Text = newBook.Title,
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Bottom,
                Height = 30
            };

            if (newBook.CoverImage != null && newBook.CoverImage.Length > 0)
            {
                try
                {
                    bookCover.Image = ConvertByteArrayToImage(newBook.CoverImage);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка загрузки изображения: {ex.Message}");
                    bookCover.BackColor = Color.Gray;
                }
            }
            else
            {
                bookCover.BackColor = Color.Gray;
            }

            bookCover.Click += BookCover_Click;
            bookPanel.Controls.Add(bookCover);
            bookPanel.Controls.Add(bookTitle);

            _userBooks[newBook] = bookPanel;
            flowLayoutPanelBooks.Controls.Add(bookPanel);
        }

        private void BookCover_Click(object sender, EventArgs e)
        {
            if (sender is PictureBox bookCover && bookCover.Tag is Book book)
            {
                bool isReadOnly = (_logged.Nickname != _currentUser.Nickname);

                var thread = new System.Threading.Thread(() =>
                {
                    var window = new AddBookWindow(_userService, _currentUser, book, isReadOnly);
                    var result = window.ShowDialog();

                    if (result == true)
                    {
                        this.Invoke(new Action(() =>
                        {
                            LoadUserBooks();
                        }));
                    }
                });

                thread.SetApartmentState(System.Threading.ApartmentState.STA);
                thread.Start();
            }
        }





        private void btnSearchBooks_Click(object sender, EventArgs e)
        {
            var thread = new System.Threading.Thread(() =>
            {
                var window = Program.ServiceProvider.GetRequiredService<BookSearchWindow>();
                window.ShowDialog(); // ✅ вместо Application.Run(window)
            });

            thread.SetApartmentState(System.Threading.ApartmentState.STA);
            thread.Start();
        }



        private ChatForm _chatForm;

        private void buttonChats_Click(object sender, EventArgs e)
        {
            ChatForm chatForm = new ChatForm(_logged.Nickname, _userService, _currentUser.Nickname);
            chatForm.Show();
        }



        private void button2_Click(object sender, EventArgs e)
        {
            FriendsForm friendsForm = new FriendsForm(_currentUser.Nickname, _userService);
            friendsForm.Show();
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

        private void btnAddBook_Click(object sender, EventArgs e)
        {
            var thread = new System.Threading.Thread(() =>
            {
                var window = new AddBookWindow(_userService, _logged, null, false); // можно редактировать
                var result = window.ShowDialog();

                if (result == true)
                {
                    // Вернуться на основной UI-поток WinForms
                    this.Invoke(new Action(() => LoadUserBooks()));
                }
            });

            thread.SetApartmentState(System.Threading.ApartmentState.STA);
            thread.Start();
        }

        private void LoadUserBooks()
        {
            flowLayoutPanelBooks.Controls.Clear();
            _userBooks.Clear();

            var books = _userService.GetUserBooks(_currentUser.Nickname);

            foreach (var book in books)
            {
                AddBookForm_BookAdded(book);
            }

            txtSearchBooks_TextChanged(null, null);
        }

        private void txtSearchBooks_TextChanged(object sender, EventArgs e)
        {
            if (txtSearchBooks.Text == "🔍 Искать книгу...") return;

            string searchText = txtSearchBooks.Text.ToLower().Trim();

            foreach (var bookPanel in _userBooks)
            {
                bool match = bookPanel.Key.Title.ToLower().Contains(searchText);
                bookPanel.Value.Visible = match;
            }
        }


        private void btnRemoveFriend_Click(object sender, EventArgs e)
        {
            if (_currentUser == null)
            {
                Console.WriteLine("Ошибка: _currentUser == null");
                MessageBox.Show("Ошибка: пользователь не найден!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var confirmResult = MessageBox.Show($"Вы уверены, что хотите удалить {_currentUser.Nickname} из друзей?",
                                                "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirmResult == DialogResult.Yes)
            {
                bool success = _userService.RemoveFriend(_logged.Nickname, _currentUser.Nickname);

                if (success)
                {
                    btnRemoveFriend.Visible = false;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Ошибка при удалении друга.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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

        private void txtSearchBooks_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearchBooks.Text))
            {
                txtSearchBooks.Text = "🔍 Искать книгу...";
                txtSearchBooks.ForeColor = Color.Gray;
            }
        }

        private void InitializeSendMessageButton()
        {
            btnSendMessage = new Button
            {
                Text = "Написать",
                Font = new Font("Arial", 10, FontStyle.Bold),
                Size = new Size(120, 30),
                Location = new Point(600, 60),
                BackColor = Color.LightBlue,
                ForeColor = Color.Black,
                FlatStyle = FlatStyle.Flat
            };
            btnSendMessage.FlatAppearance.BorderSize = 0;
            btnSendMessage.Click += btnSendMessage_Click;
            Controls.Add(btnSendMessage);
        }

        private void btnSendMessage_Click(object sender, EventArgs e)
        {
            var existingChat = _userService.GetChatMessages(_logged.Nickname, _currentUser.Nickname);

            ChatForm chatForm = new ChatForm(_logged.Nickname, _userService, _currentUser.Nickname);
            chatForm.Show();
        }
    }
}
