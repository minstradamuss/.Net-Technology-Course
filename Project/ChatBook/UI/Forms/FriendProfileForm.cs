using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using ChatBook.Domain.Models;
using ChatBook.Services;

namespace ChatBook.UI.Forms
{
    public partial class FriendProfileForm : Form
    {
        private readonly User _user;
        private readonly UserService _userService;
        private readonly bool _isFriend;
        private readonly string _currentUserNickname;
        private Button btnAddFriend;

        public FriendProfileForm(User user, bool isFriend, UserService userService, string currentUserNickname)
        {
            InitializeComponent();
            _user = user;
            _userService = userService;
            _isFriend = isFriend;
            _currentUserNickname = currentUserNickname;

            lblFullName.Text = $"{user.FirstName} {user.LastName}";
            lblNickname.Text = user.Nickname;

            if (user.Avatar != null && user.Avatar.Length > 0)
            {
                pictureBoxAvatar.Image = ConvertByteArrayToImage(user.Avatar);
            }
            else
            {
                pictureBoxAvatar.BackColor = Color.Gray;
            }

            InitializeBookList();
            LoadUserBooks();

            if (!_isFriend)
            {
                btnAddFriend = new Button
                {
                    Text = "Добавить в друзья",
                    Dock = DockStyle.Bottom,
                    Height = 30
                };
                btnAddFriend.Click += BtnAddFriend_Click;
                Controls.Add(btnAddFriend);
            }

            // ✅ Добавляем обработчик на аватарку
            pictureBoxAvatar.DoubleClick += pictureBoxAvatar_DoubleClick;
        }




        private void InitializeBookList()
        {
            flowLayoutPanelbooks = new FlowLayoutPanel
            {
                AutoScroll = true,
                FlowDirection = FlowDirection.LeftToRight, // ✅ Горизонтальное размещение
                WrapContents = true, // ✅ Перенос на новую строку
                Dock = DockStyle.Bottom,
                Height = 250,
                Width = 600 // ✅ Ограничение ширины
            };
            Controls.Add(flowLayoutPanelbooks);
        }


        // ✅ Загрузка книг пользователя
        private void LoadUserBooks()
        {
            flowLayoutPanelbooks.Controls.Clear();
            var books = _userService.GetReadBooks(_user.Nickname);

            Console.WriteLine($"Загружено {books.Count} книг для пользователя {_user.Nickname}");

            if (books.Count == 0)
            {
                Label noBooksLabel = new Label
                {
                    Text = "Нет прочитанных книг",
                    AutoSize = true,
                    Padding = new Padding(5),
                    ForeColor = Color.Gray
                };
                flowLayoutPanelbooks.Controls.Add(noBooksLabel);
                return;
            }

            foreach (var book in books)
            {
                Panel bookPanel = CreateBookPanel(book);
                flowLayoutPanelbooks.Controls.Add(bookPanel);
            }
        }


        private Panel CreateFriendPanel(User user)
        {
            Panel panel = new Panel
            {
                Size = new Size(150, 180),
                BorderStyle = BorderStyle.FixedSingle,
                Margin = new Padding(10),
                Tag = user // ✅ Сохраняем объект User в Tag
            };

            PictureBox avatar = new PictureBox
            {
                Size = new Size(120, 120),
                BorderStyle = BorderStyle.FixedSingle,
                SizeMode = PictureBoxSizeMode.Zoom,
                Location = new Point(15, 10),
                Tag = user // ✅ Сохраняем объект User в Tag
            };

            if (user.Avatar != null && user.Avatar.Length > 0)
            {
                avatar.Image = ConvertByteArrayToImage(user.Avatar);
            }
            else
            {
                avatar.BackColor = Color.Gray;
            }

            Label nicknameLabel = new Label
            {
                Text = user.Nickname,
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Bottom,
                Height = 20,
                Tag = user
            };

            Label fullNameLabel = new Label
            {
                Text = $"{user.FirstName} {user.LastName}",
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Bottom,
                Height = 20,
                Tag = user
            };

            // ✅ Добавляем обработчик двойного клика ко всем элементам
            panel.DoubleClick += OpenUserProfile;
            avatar.DoubleClick += OpenUserProfile;
            nicknameLabel.DoubleClick += OpenUserProfile;
            fullNameLabel.DoubleClick += OpenUserProfile;

            panel.Controls.Add(avatar);
            panel.Controls.Add(nicknameLabel);
            panel.Controls.Add(fullNameLabel);

            return panel;
        }


        private Panel CreateBookPanel(Book book)
        {
            Panel bookPanel = new Panel
            {
                Size = new Size(120, 180), // ✅ Одинаковый размер для книг
                BorderStyle = BorderStyle.FixedSingle,
                Margin = new Padding(10),
                Tag = book
            };

            PictureBox bookCover = new PictureBox
            {
                Size = new Size(100, 140),
                BorderStyle = BorderStyle.FixedSingle,
                SizeMode = PictureBoxSizeMode.Zoom,
                Location = new Point(10, 10),
                Tag = book
            };

            Label bookTitle = new Label
            {
                Text = book.Title,
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Bottom,
                Height = 30
            };

            if (book.CoverImage != null && book.CoverImage.Length > 0)
            {
                try
                {
                    bookCover.Image = ConvertByteArrayToImage(book.CoverImage);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка загрузки изображения: {ex.Message}");
                    bookCover.BackColor = Color.Gray;
                }
            }
            else
            {
                bookCover.BackColor = Color.Gray; // ✅ Если нет обложки, делаем серый фон
            }

            bookCover.DoubleClick += OpenBookDetails;
            bookTitle.DoubleClick += OpenBookDetails;
            bookPanel.DoubleClick += OpenBookDetails;

            bookPanel.Controls.Add(bookCover);
            bookPanel.Controls.Add(bookTitle);

            return bookPanel;
        }



        private void OpenBookDetails(object sender, EventArgs e)
        {
            Book book = null;

            if (sender is Panel panel && panel.Tag is Book panelBook)
                book = panelBook;
            else if (sender is Label label && label.Tag is Book labelBook)
                book = labelBook;

            if (book == null) return;

            MessageBox.Show($"Открытие книги: {book.Title}\n\nРецензия: {book.Review}",
                "Детали книги", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnAddFriend_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_currentUserNickname))
            {
                MessageBox.Show("Ошибка: текущий пользователь не найден!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            bool success = _userService.AddFriend(_currentUserNickname, _user.Nickname);

            if (success)
            {
                MessageBox.Show($"{_user.Nickname} добавлен в друзья!", "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnAddFriend.Enabled = false;
            }
            else
            {
                MessageBox.Show("Ошибка при добавлении в друзья. Возможно, пользователь уже в списке друзей.",
                                "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void OpenUserProfile(object sender, EventArgs e)
        {
            User user = null;

            if (sender is Panel panel && panel.Tag is User panelUser)
                user = panelUser;
            else if (sender is PictureBox pictureBox && pictureBox.Tag is User pictureUser)
                user = pictureUser;
            else if (sender is Label label && label.Tag is User labelUser)
                user = labelUser;

            if (user == null)
            {
                MessageBox.Show("Ошибка: не удалось получить данные пользователя!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MainForm mainForm = new MainForm(user, _userService, isProfileViewOnly: true);
            mainForm.ShowDialog();
        }

        private void pictureBoxAvatar_DoubleClick(object sender, EventArgs e)
        {
            MainForm mainForm = new MainForm(_user, _userService, isProfileViewOnly: true);
            mainForm.ShowDialog();
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
