//using System;
//using System.Drawing;
//using System.IO;
//using System.Threading;
//using System.Windows.Forms;
//using ChatBook.Entities;
//using ChatBook.UI.Forms;
//using Moq;
//using NUnit.Framework;

//namespace ChatBook.Tests
//{
//    [TestFixture]
//    public class AddBookFormTests
//    {
//        //private AddBookForm _addBookForm;
//        private Mock<Action<Book>> _mockBookAdded;
//        private Mock<Action<Book>> _mockBookUpdated;
//        private Book _testBook;
//        private Thread _uiThread;

//        [SetUp]
//        public void Setup()
//        {
//            _mockBookAdded = new Mock<Action<Book>>();
//            _mockBookUpdated = new Mock<Action<Book>>();

//            _testBook = new Book
//            {
//                Title = "Test Book",
//                Status = "В планах",
//                Rating = 5,
//                Review = "Хорошая книга",
//            };

//            var formReady = new ManualResetEvent(false);

//            _uiThread = new Thread(() =>
//            {
//                //_addBookForm = new AddBookForm(_testBook);
//                //_addBookForm.BookAdded += _mockBookAdded.Object;
//                //_addBookForm.BookUpdated += _mockBookUpdated.Object;

//                formReady.Set();
//                //_addBookForm.ShowDialog();
//            });

//            _uiThread.SetApartmentState(ApartmentState.STA);
//            _uiThread.Start();

//            formReady.WaitOne();
//            Thread.Sleep(500); 
//        }



//        [Test]
//        [Apartment(ApartmentState.STA)]
//        public void SaveBook_ShouldInvokeBookAddedEvent()
//        {
//            var bookPredicate = It.IsAny<Book>();

//            _addBookForm.Invoke((MethodInvoker)(() =>
//            {
//                _addBookForm.GetType()
//                    .GetMethod("btnSaveBook_Click", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
//                    ?.Invoke(_addBookForm, new object[] { null, EventArgs.Empty });
//            }));

//            Thread.Sleep(100);

//            _mockBookAdded.Verify(x => x(bookPredicate), Times.AtMostOnce, "Книга должна быть добавлена один раз");
//        }

//        [Test]
//        [Apartment(ApartmentState.STA)]
//        public void EditBook_ShouldInvokeBookUpdatedEvent()
//        {
//            _addBookForm.Invoke((MethodInvoker)(() =>
//            {
//                _addBookForm.GetType()
//                    .GetMethod("btnSaveBook_Click", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
//                    ?.Invoke(_addBookForm, new object[] { null, EventArgs.Empty });
//            }));

//            _mockBookUpdated.Verify(x => x(It.IsAny<Book>()), Times.Once, "Книга должна быть обновлена один раз");
//        }


//        [Test]
//        [Apartment(ApartmentState.STA)]
//        public void UploadCover_ShouldSetCoverImagePath()
//        {
//            string testImagePath = "";

//            var coverFieldAfter = _addBookForm.GetType()
//                .GetField("txtCoverImagePath", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
//            var coverPathAfter = (coverFieldAfter.GetValue(_addBookForm) as TextBox)?.Text;

//            if (testImagePath.Length > 0)
//            {
//                _addBookForm.Invoke((MethodInvoker)(() =>
//                {
//                    var openFileDialogMock = new Mock<OpenFileDialog>();
//                    openFileDialogMock.Setup(x => x.ShowDialog()).Returns(DialogResult.OK);
//                    openFileDialogMock.Setup(x => x.FileName).Returns(testImagePath);

//                    var coverField = _addBookForm.GetType()
//                        .GetField("txtCoverImagePath", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

//                    var coverPathTextBox = coverField.GetValue(_addBookForm) as TextBox;
//                    Assert.That(coverPathTextBox, Is.Not.Null, "Поле txtCoverImagePath не найдено");
//                    coverPathTextBox.Text = openFileDialogMock.Object.FileName;

//                    var method = _addBookForm.GetType()
//                        .GetMethod("btnUploadCover_Click", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
//                    method?.Invoke(_addBookForm, new object[] { null, EventArgs.Empty });
//                }));
//            }

//            Assert.That(coverPathAfter, Is.EqualTo(testImagePath), "Путь к изображению должен обновиться автоматически");

//            if (!string.IsNullOrEmpty(coverPathAfter) && File.Exists(coverPathAfter))
//            {
//                _addBookForm.Invoke((MethodInvoker)(() =>
//                {
//                    var pictureBoxField = _addBookForm.GetType()
//                        .GetField("pictureBoxCover", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
//                    var pictureBox = pictureBoxField.GetValue(_addBookForm) as PictureBox;

//                    Assert.That(pictureBox, Is.Not.Null, "Поле pictureBoxCover не найдено");
//                    pictureBox.Image = Image.FromFile(coverPathAfter);
//                    pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
//                }));
//            }
//        }
//    }
//}
