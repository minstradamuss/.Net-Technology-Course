using System;
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

        public AddBookForm()
        {
            InitializeComponent();
            ToggleSaveButton(false);
        }

        public AddBookForm(Book book) : this()
        {
            _currentBook = book;
            isEditMode = true;
            txtBookTitle.Text = book.Title;
            cmbStatus.SelectedItem = book.Status;
            numRating.Value = book.Rating;
            txtReview.Text = book.Review;

            if (book.CoverImage != null && book.CoverImage.Length > 0)
            {
                pictureBoxCover.Image = ConvertByteArrayToImage(book.CoverImage);
                _coverImageBytes = book.CoverImage;
            }

            ToggleSaveButton(true);
        }

        private void btnSaveBook_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtBookTitle.Text))
            {
                MessageBox.Show("Введите название книги.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // ✅ Обновляем текущую книгу, а не создаем новую
            if (_currentBook == null) _currentBook = new Book();

            _currentBook.Title = txtBookTitle.Text;
            _currentBook.Status = cmbStatus.SelectedItem?.ToString() ?? "В планах";
            _currentBook.Rating = (int)numRating.Value;
            _currentBook.Review = txtReview.Text;
            _currentBook.CoverImage = _coverImageBytes; // 🔹 Сохранение изображения

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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
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

        public void RaiseBookUpdated(Book updatedBook)
        {
            BookUpdated?.Invoke(updatedBook);
        }
    }
}
