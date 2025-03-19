using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using ChatBook.Domain.Models;
using ChatBook.Services;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;

using ChatUser = ChatBook.Domain.Models.User;


namespace ChatBook.UI.Forms
{
    public partial class AddBookForm : Form
    {
        public event Action<Book> BookAdded;
        public event Action<Book> BookUpdated;
        private Book _currentBook;
        private bool isEditMode = false;
        private byte[] _coverImageBytes;
        private int _selectedRating = 1;
        private Label[] stars = new Label[5];
        private ChatUser _currentUser;
        private ChatUser _logged;

        public event Action<int> BookDeleted;

        private readonly UserService _userService;
        public string CurrentUserNickname { get; private set; }

        public AddBookForm(UserService userService, ChatUser user)
        {
            InitializeComponent();
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _currentUser = user ?? throw new ArgumentNullException(nameof(user));
            _logged = AppSession.LoggedUser;
            CurrentUserNickname = _currentUser.Nickname;
            if (_logged.Nickname != CurrentUserNickname)
            {
                DisableEditing();
            }

        }
        public AddBookForm(ChatUser user, UserService userService, bool isProfileViewOnly = false, Book book = null)
        {
            InitializeComponent();
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _currentUser = user ?? throw new ArgumentNullException(nameof(user));
            _logged = AppSession.LoggedUser;
            CurrentUserNickname = _currentUser.Nickname;

            ToggleSaveButton(false);

            if (book != null)
            {
                InitializeStarRating(book.UserId, book.Title);
            }

            if (_logged.Nickname != CurrentUserNickname)
            {
                DisableEditing();
            }
        }


        private void DisableEditing()
        {
            txtBookTitle.ReadOnly = true;
            txtReview.ReadOnly = true;
            cmbStatus.Enabled = false;

            btnSaveBook.Visible = false;
            btnUploadCover.Visible = false;
            buttonDelete.Visible = false;
            btnCancel.Visible = true;

            if (stars == null || stars.Length == 0)
            {
                if (_currentBook != null)
                {
                    LoadBookRatingFromDatabase(_currentBook.UserId, _currentBook.Title);
                }
            }
        }



        private void LoadBookRatingFromDatabase(int userId, string bookTitle)
        {
            Book book = _userService.GetBookByUserAndTitle(userId, bookTitle);

            if (book != null)
            {
                _selectedRating = book.Rating;
            }
            else
            {
                _selectedRating = 0;
            }

            InitializeStarRating(userId, bookTitle);
            UpdateStarRating();
        }



        public AddBookForm(Book book, UserService userService, ChatUser user, bool isProfileViewOnly)
    : this(userService, user)
        {
            _currentBook = book;
            isEditMode = !isProfileViewOnly; 

            txtBookTitle.Text = book.Title;
            cmbStatus.SelectedItem = book.Status;
            txtReview.Text = book.Review;
            _selectedRating = book.Rating;

            if (book.CoverImage != null && book.CoverImage.Length > 0)
            {
                pictureBoxCover.Image = ConvertByteArrayToImage(book.CoverImage);
                _coverImageBytes = book.CoverImage;
            }

            LoadBookRatingFromDatabase(user.Id, book.Title);

            ToggleSaveButton(true);
        }

        private void UpdateStarRating()
        {
            if (stars == null || stars.Length == 0)
            {
                if (_currentBook != null)
                {
                    InitializeStarRating(_currentBook.UserId, _currentBook.Title);
                }
                else
                {
                    return;
                }
            }

            for (int i = 0; i < stars.Length; i++)
            {
                stars[i].Text = (i < _selectedRating) ? "🌟" : "☆";
            }
        }



        private void btnSaveBook_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtBookTitle.Text))
            {
                MessageBox.Show("Введите название книги.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (_currentBook == null) _currentBook = new Book();

            _currentBook.Title = txtBookTitle.Text;
            _currentBook.Status = cmbStatus.SelectedItem?.ToString() ?? "В планах";
            _currentBook.Rating = _selectedRating;
            _currentBook.Review = txtReview.Text;
            _currentBook.CoverImage = _coverImageBytes;

            if (isEditMode)
            {
                BookUpdated?.Invoke(_currentBook);
            }
            else
            {
                BookAdded?.Invoke(_currentBook);
            }

            this.Close();
        }

        private void InitializeStarRating(int userId, string bookTitle)
        {
            if (stars == null)
                stars = new Label[5];

            FlowLayoutPanel starPanel = new FlowLayoutPanel
            {
                Location = new Point(20, 100),
                Size = new Size(200, 40),
                AutoSize = true
            };

            for (int i = 0; i < stars.Length; i++)
            {
                stars[i] = new Label
                {
                    Text = "☆",
                    Font = new Font("Arial", 24, FontStyle.Regular),
                    ForeColor = Color.Crimson,
                    AutoSize = true,
                    Cursor = Cursors.Hand,
                    Tag = i + 1
                };

                stars[i].Click += Star_Click;
                starPanel.Controls.Add(stars[i]);
            }

            this.Controls.Add(starPanel);

            Book book = _userService.GetBookByUserAndTitle(userId, bookTitle);

            if (book != null)
            {
                _selectedRating = book.Rating;
                UpdateStarRating();
            }

            if (_logged.Nickname != CurrentUserNickname)
            {
                foreach (var star in stars)
                {
                    star.Enabled = false;
                }
            }
        }




        private void Star_Click(object sender, EventArgs e)
        {
            if (sender is Label starLabel)
            {
                _selectedRating = (int)starLabel.Tag; 
                UpdateStarRating();
            }
        }

        private void btnUploadCover_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Изображения|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    _coverImageBytes = ConvertImageToByteArray(openFileDialog.FileName);
                    pictureBoxCover.Image = Image.FromFile(openFileDialog.FileName);
                    pictureBoxCover.SizeMode = PictureBoxSizeMode.Zoom;
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

        public void ToggleSaveButton(bool isEdit)
        {
            btnSaveBook.Text = isEdit ? "Сохранить" : "Добавить";
            btnCancel.Text = "Отмена";
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (_currentBook == null)
            {
                MessageBox.Show("Ошибка: Книга не выбрана!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var confirmResult = MessageBox.Show($"Вы уверены, что хотите удалить книгу '{_currentBook.Title}'?",
                                                "Подтверждение удаления", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirmResult == DialogResult.Yes)
            {
                bool success = _userService.DeleteBook(_currentBook.Id);

                if (success)
                {
                    BookDeleted?.Invoke(_currentBook.Id);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Ошибка при удалении книги.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
