using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using ChatBook.Domain.Models;

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

        public AddBookForm()
        {
            InitializeComponent();
            ToggleSaveButton(false);
            InitializeStarRating();
            numRating.Visible = false;
        }

        public AddBookForm(Book book) : this()
        {
            _currentBook = book;
            isEditMode = true;
            txtBookTitle.Text = book.Title;
            cmbStatus.SelectedItem = book.Status;
            txtReview.Text = book.Review;
            _selectedRating = book.Rating;

            if (book.CoverImage != null && book.CoverImage.Length > 0)
            {
                pictureBoxCover.Image = ConvertByteArrayToImage(book.CoverImage);
                _coverImageBytes = book.CoverImage;
            }

            UpdateStarRating();
            ToggleSaveButton(true);
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

        private void InitializeStarRating()
        {
            FlowLayoutPanel starPanel = new FlowLayoutPanel
            {
                Location = new Point(20, 100),
                Size = new Size(200, 40),
                AutoSize = true
            };

            for (int i = 0; i < 5; i++)
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
            UpdateStarRating();
        }

        private void Star_Click(object sender, EventArgs e)
        {
            if (sender is Label starLabel)
            {
                _selectedRating = (int)starLabel.Tag; 
                UpdateStarRating();
            }
        }

        private void UpdateStarRating()
        {
            for (int i = 0; i < stars.Length; i++)
            {
                stars[i].Text = (i < _selectedRating) ? "🌟" : "☆";
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
    }
}
