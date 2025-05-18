using ChatBook.Entities;
using ChatBook.Services;
using ChatBook.ViewModels;
using System.Windows;

public partial class AddBookWindow : Window
{
    public AddBookWindow(UserService userService, User user, Book book = null, bool isReadOnly = false)
    {
        InitializeComponent();
        this.DataContext = new AddBookViewModel(userService, user, book, isReadOnly);
    }
}
