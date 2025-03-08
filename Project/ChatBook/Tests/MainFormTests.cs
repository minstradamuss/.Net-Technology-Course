using System.Collections.Generic;
using System.Windows.Forms;
using ChatBook.Domain.Models;
using ChatBook.UI.Forms;
using Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ChatBook.Tests
{
    [TestClass]
    public class MainFormTests
    {
        private MainForm _mainForm;
        private Mock<AddBookForm> _mockAddBookForm;
        private Book _testBook;

        [TestInitialize]
        public void Setup()
        {
            _mainForm = new MainForm("TestUser");
            _mainForm.Show();
            Application.DoEvents();

            _mockAddBookForm = new Mock<AddBookForm>();
            _testBook = new Book
            {
                Title = "Test Book",
                Status = "Прочитано",
                Rating = 5,
                Review = "Отличная книга!",
                CoverImagePath = "test_image.jpg"
            };
        }

        [TestMethod]
        public void AddBook_ShouldDisplayBookOnUI()
        {
            _mainForm.Invoke((MethodInvoker)(() =>
            {
                var method = _mainForm.GetType()
                    .GetMethod("AddBookForm_BookAdded",
                               System.Reflection.BindingFlags.NonPublic |
                               System.Reflection.BindingFlags.Instance);
                method?.Invoke(_mainForm, new object[] { _testBook });
            }));

            var booksField = _mainForm.GetType()
                .GetField("_userBooks",
                          System.Reflection.BindingFlags.NonPublic |
                          System.Reflection.BindingFlags.Instance);
            var books = booksField.GetValue(_mainForm) as Dictionary<Book, Panel>;

            Assert.IsNotNull(books, "Список книг не должен быть null");
            Assert.IsTrue(books.ContainsKey(_testBook), "Книга должна быть добавлена");
        }

        [TestMethod]
        public void EditBook_ShouldUpdateBookData()
        {
            _mainForm.Invoke((MethodInvoker)(() =>
            {
                var method = _mainForm.GetType()
                    .GetMethod("AddBookForm_BookAdded",
                               System.Reflection.BindingFlags.NonPublic |
                               System.Reflection.BindingFlags.Instance);
                method?.Invoke(_mainForm, new object[] { _testBook });
            }));

            var updatedBook = new Book
            {
                Title = "Updated Book",
                Status = "Читаю",
                Rating = 4,
                Review = "Интересно!",
                CoverImagePath = "new_image.jpg"
            };

            var bookCover = new PictureBox { Tag = _testBook };
            _mainForm.Invoke((MethodInvoker)(() =>
            {
                var method = _mainForm.GetType()
                    .GetMethod("BookCover_Click",
                               System.Reflection.BindingFlags.NonPublic |
                               System.Reflection.BindingFlags.Instance);
                method?.Invoke(_mainForm, new object[] { bookCover, EventArgs.Empty });
            }));

            var editBookForm = new AddBookForm(updatedBook);
            editBookForm.RaiseBookUpdated(updatedBook);

            var booksField = _mainForm.GetType()
                .GetField("_userBooks",
                          System.Reflection.BindingFlags.NonPublic |
                          System.Reflection.BindingFlags.Instance);
            var books = booksField.GetValue(_mainForm) as Dictionary<Book, Panel>;

            Assert.IsFalse(books.ContainsKey(updatedBook), "Книга должна быть обновлена");
            Assert.IsTrue(books.ContainsKey(_testBook), "Старая книга должна быть удалена");
        }

        [TestCleanup]
        public void Cleanup()
        {
            if (_mainForm != null && !_mainForm.IsDisposed)
            {
                _mainForm.Invoke((MethodInvoker)(() => _mainForm.Close()));
            }
        }
    }
}
