using ChatBook.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Data.Common;
using System.Data.SQLite;
using System.Windows.Forms;
using ChatBook.UI.Forms;
using ChatBook.UI.ViewModels;

namespace ChatBook
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            var serviceProvider = new ServiceCollection()
                .AddSingleton<DbConnection>(provider =>
                {
                    string connectionString = "Data Source=C:\\Users\\User\\source\\repos\\.Net-Technology-Course\\Project\\ChatBook\\DB\\ChatBook.db"; // Подключение к SQLite
                    return new SQLiteConnection(connectionString);
                })
                .AddSingleton<ChatBookDbContext>()
                .AddSingleton<UserService>()
                .AddSingleton<RegisterViewModel>()
                .BuildServiceProvider();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var userService = serviceProvider.GetService<UserService>();

            Application.Run(new LoginForm(userService));
        }
    }
}
