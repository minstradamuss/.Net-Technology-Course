using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using ChatBook.Domain.Models;

namespace ChatBook.UI.Forms
{
    public partial class MainForm : Form
    {
        private User _currentUser;
        private Dictionary<Book, Panel> _userBooks = new Dictionary<Book, Panel>();
        private FlowLayoutPanel flowLayoutPanelBooks;
        //private Button btnAddBook;

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
                AutoSize = true,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = true,
                Location = new System.Drawing.Point(20, 200),
                Size = new System.Drawing.Size(500, 200)
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

            editProfileForm.ProfileUpdated += (firstName, lastName, phone, avatarPath) =>
            {
                _currentUser.FirstName = firstName;
                _currentUser.LastName = lastName;
                _currentUser.Phone = phone;
                _currentUser.AvatarPath = avatarPath;

                lblFullName.Text = $"{firstName} {lastName}";
                if (!string.IsNullOrEmpty(avatarPath) && File.Exists(avatarPath))
                {
                    pictureBoxAvatar.Image = Image.FromFile(avatarPath);
                }
            };

            editProfileForm.ShowDialog();
        }


        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.Close(); // Закрываем MainForm
            LoginForm loginForm = new LoginForm();
            loginForm.Show(); // Открываем форму входа
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
                Size = new System.Drawing.Size(120, 180),
                BorderStyle = BorderStyle.FixedSingle
            };

            PictureBox bookCover = new PictureBox
            {
                Size = new System.Drawing.Size(100, 140),
                BorderStyle = BorderStyle.FixedSingle,
                SizeMode = PictureBoxSizeMode.Zoom,
                Location = new System.Drawing.Point(10, 10),
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

            if (!string.IsNullOrEmpty(newBook.CoverImagePath) && File.Exists(newBook.CoverImagePath))
            {
                try
                {
                    using (FileStream fs = new FileStream(newBook.CoverImagePath, FileMode.Open, FileAccess.Read))
                    {
                        bookCover.Image = Image.FromStream(fs);
                    }
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
                editBookForm.ToggleSaveButton(true); // <-- Добавлено!

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
    }
}
