using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using ChatBook.Domain.Models;
using ChatBook.Services;

namespace ChatBook.UI.Forms
{
    public partial class MainForm : Form
    {
        private User _currentUser;
        private Dictionary<Book, Panel> _userBooks = new Dictionary<Book, Panel>();
        private FlowLayoutPanel flowLayoutPanelBooks;

        public MainForm(string username)
        {
            InitializeComponent();
            _currentUser = new User { Nickname = username };
            lblNickname.Text = username;
            InitializeFlowLayoutPanel();
            InitializeAddBookButton();
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
            EditProfileForm editProfileForm = new EditProfileForm(_currentUser);
            editProfileForm.ProfileUpdated += UpdateProfileInfo;
            editProfileForm.ShowDialog();
        }

        private void UpdateProfileInfo(User updatedUser)
        {
            _currentUser = updatedUser;
            lblFullName.Text = $"{_currentUser.FirstName} {_currentUser.LastName}";

            if (_currentUser.Avatar != null && _currentUser.Avatar.Length > 0)
            {
                pictureBoxAvatar.Image = ConvertByteArrayToImage(_currentUser.Avatar);
            }
            else
            {
                pictureBoxAvatar.Image = null;
            }
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.Close();
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
        }

        private void btnAddBook_Click(object sender, EventArgs e)
        {
            AddBookForm addBookForm = new AddBookForm();
            addBookForm.BookAdded += AddBookForm_BookAdded;
            addBookForm.Show();
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
                AddBookForm editBookForm = new AddBookForm(book);
                editBookForm.ToggleSaveButton(true);

                editBookForm.BookUpdated += (updatedBook) =>
                {
                    if (_userBooks.ContainsKey(book))
                    {
                        flowLayoutPanelBooks.Controls.Remove(_userBooks[book]);
                        _userBooks.Remove(book);
                    }
                    AddBookForm_BookAdded(updatedBook);
                };
                editBookForm.Show();
            }
        }

        private void btnSearchBooks_Click(object sender, EventArgs e)
        {
            SearchForm searchForm = new SearchForm();
            searchForm.Show();
        }

        private ChatForm _chatForm;

        private void button1_Click(object sender, EventArgs e)
        {
            if (_chatForm == null || _chatForm.IsDisposed)
            {
                _chatForm = new ChatForm();
                _chatForm.Show();
            }
            else
            {
                _chatForm.Focus();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FriendsForm friendsForm = new FriendsForm();
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
    }
}
