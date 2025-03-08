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
        private bool isEditMode = false; // Флаг режима редактирования

        public AddBookForm()
        {
            InitializeComponent();
            ToggleSaveButton(false);
        }

        public AddBookForm(Book book) : this()
        {
            _currentBook = book;
            isEditMode = true; // Устанавливаем флаг редактирования
            txtBookTitle.Text = book.Title;
            cmbStatus.SelectedItem = book.Status;
            numRating.Value = book.Rating;
            txtReview.Text = book.Review;

            if (!string.IsNullOrEmpty(book.CoverImagePath) && File.Exists(book.CoverImagePath))
            {
                using (var fs = new FileStream(book.CoverImagePath, FileMode.Open, FileAccess.Read))
                {
                    pictureBoxCover.Image = Image.FromStream(fs);
                }
                txtCoverImagePath.Text = book.CoverImagePath;
            }

            ToggleSaveButton(true); // Переключаем кнопки в режим "Сохранить"
        }

        private void btnSaveBook_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtBookTitle.Text))
            {
                MessageBox.Show("Введите название книги.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var newBook = new Book
            {
                Title = txtBookTitle.Text,
                Status = cmbStatus.SelectedItem?.ToString() ?? "В планах",
                Rating = (int)numRating.Value,
                Review = txtReview.Text,
                CoverImagePath = txtCoverImagePath.Text
            };

            if (isEditMode)
            {
                BookUpdated?.Invoke(newBook);
            }
            else
            {
                BookAdded?.Invoke(newBook);
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
                    txtCoverImagePath.Text = openFileDialog.FileName;
                    using (var fs = new FileStream(openFileDialog.FileName, FileMode.Open, FileAccess.Read))
                    {
                        pictureBoxCover.Image = Image.FromStream(fs);
                    }
                    pictureBoxCover.SizeMode = PictureBoxSizeMode.Zoom;
                }
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
