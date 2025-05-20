using ChatBook.DataAccess;
using ChatBook.Migrations;
using ChatBook.UI.Forms;
using ChatBook.UI.Windows;
using ChatBook.Domain.Interfaces;
using ChatBook.Services;
using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using ChatService.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System.Windows.Forms;
using ChatBook.ViewModels;
using ChatBook.UI.ViewModel;
using System.Runtime.Remoting.Contexts;
using ChatBook.DB;
using System.IO;
using ChatService.Domain;

namespace ChatBook
{
    internal static class Program
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        [STAThread]
        static void Main()
        {
            //ApplyMigrations();
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

            // Регистрация Чат-сервиса
            services.AddSingleton<IChatRepository, ChatRepository>();
            services.AddSingleton<IChatService, ChatService.Services.ChatService>();

            services.AddSingleton<AddBookViewModel>();

            services.AddSingleton<MainViewModel>();
            services.AddSingleton<BookSearchViewModel>();
            services.AddSingleton<BookViewModel>();
            services.AddSingleton<ProfileViewModel>();
            services.AddTransient<ChatViewModel>();
            services.AddSingleton<FriendsViewModel>();
            services.AddTransient<LoginForm>();
            services.AddTransient<MainForm>();
            services.AddTransient<FriendsForm>();
            services.AddTransient<EditProfileWindow>();
            services.AddTransient<ChatForm>();
            services.AddTransient<BookSearchWindow>();
            services.AddTransient<AddBookWindow>();

            ServiceProvider = services.BuildServiceProvider();

            
            Database.SetInitializer(new DbInitializer());


        }


        static void ApplyMigrations()
        {
            var configuration = new Configuration();
            var migrator = new DbMigrator(configuration);
            migrator.Update();
        }
    }
}
