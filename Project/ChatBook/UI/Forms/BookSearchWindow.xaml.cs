using ChatBook.Entities;
using ChatBook.Services;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.IO;
using System.Windows.Media;

namespace ChatBook.UI.Windows
{
    public partial class BookSearchWindow : Window
    {
        private readonly UserService _userService;

        public BookSearchWindow(UserService userService)
        {
            InitializeComponent(); // Обязательно вызывает генерацию полей txtSearch, BooksPanel
            _userService = userService;
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
        }

        private void txtSearch_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtSearch.Text == "Введите название книги...")
            {
                txtSearch.Text = "";
                txtSearch.Foreground = Brushes.Black;
            }
        }


        private void txtSearch_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                txtSearch.Text = "Введите название книги...";
                txtSearch.Foreground = Brushes.Gray;
            }
        }



        private void Search_Click(object sender, RoutedEventArgs e)
        {
            string query = txtSearch.Text.Trim();
            if (string.IsNullOrWhiteSpace(query)) return;

            BooksPanel.Children.Clear();

            var booksWithReviews = _userService.SearchBooksWithReviews(query);
            foreach (var item in booksWithReviews)
            {
                var border = new Border
                {
                    BorderThickness = new Thickness(1),
                    BorderBrush = System.Windows.Media.Brushes.Gray,
                    Margin = new Thickness(10),
                    Padding = new Thickness(5),
                    Width = 160,
                    Cursor = System.Windows.Input.Cursors.Hand
                };

                var stack = new StackPanel();

                if (item.Book.CoverImage != null)
                {
                    var image = new Image { Height = 120, Width = 120, Margin = new Thickness(0, 0, 0, 5) };
                    using (var ms = new MemoryStream(item.Book.CoverImage))
                    {
                        var bitmap = new BitmapImage();
                        bitmap.BeginInit();
                        bitmap.CacheOption = BitmapCacheOption.OnLoad;
                        bitmap.StreamSource = ms;
                        bitmap.EndInit();
                        image.Source = bitmap;
                    }
                    stack.Children.Add(image);
                }

                stack.Children.Add(new TextBlock { Text = item.Book.Title, FontWeight = FontWeights.Bold, TextAlignment = TextAlignment.Center, TextWrapping = TextWrapping.Wrap });
                stack.Children.Add(new TextBlock { Text = $"Author: {item.Book.Author}", FontSize = 12 });
                stack.Children.Add(new TextBlock { Text = $"Rating: {item.Book.Rating}/5", FontSize = 12 });
                stack.Children.Add(new TextBlock { Text = $"User: {item.ReviewerNickname}", FontSize = 12, FontStyle = FontStyles.Italic });

                border.Child = stack;

                border.MouseLeftButtonUp += (s, ev) =>
                {
                    var user = _userService.GetUserByNickname(item.ReviewerNickname);

                    var thread = new System.Threading.Thread(() =>
                    {
                        var window = new AddBookWindow(_userService, user, item.Book, isReadOnly: true);
                        window.ShowDialog();
                    });

                    thread.SetApartmentState(System.Threading.ApartmentState.STA);
                    thread.Start();
                };

                BooksPanel.Children.Add(border);
            }
        }

    }
}
