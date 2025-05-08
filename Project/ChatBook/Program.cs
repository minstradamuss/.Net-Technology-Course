using ChatBook.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Windows.Forms;
using ChatBook.DataAccess;
using ChatBook.Migrations;
using ChatBook.UI.Forms;
using ChatBook.UI.Windows;

namespace ChatBook
{
    internal static class Program
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        [STAThread]
        static void Main()
        {
            ApplyMigrations();
            ConfigureServices();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Получаем сервис
            var loginForm = ServiceProvider.GetRequiredService<LoginForm>();
            Application.Run(loginForm);
        }

        static void ConfigureServices()
        {
            var services = new ServiceCollection();

            services.AddSingleton<ApplicationDbContext>();
            services.AddSingleton<UserService>();

            // Регистрируем формы
            services.AddTransient<LoginForm>();
            services.AddTransient<MainForm>();
            services.AddTransient<FriendsForm>();
            services.AddTransient<EditProfileWindow>();
            services.AddTransient<ChatForm>();
            services.AddTransient<BookSearchWindow>();
            services.AddTransient<AddBookWindow>();

            ServiceProvider = services.BuildServiceProvider();

            // инициализация БД
            Database.SetInitializer(new DB.DbInitializer());
        }

        static void ApplyMigrations()
        {
            var configuration = new Configuration();
            var migrator = new DbMigrator(configuration);
            migrator.Update();
        }
    }
}
