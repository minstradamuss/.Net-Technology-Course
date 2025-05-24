using ChatBook.DataAccess;
using ChatBook.Migrations;
using ChatBook.UI.Forms;
using ChatBook.UI.Windows;
using ChatBook.Domain.Interfaces;
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
using ChatBook.DataAccess.Repositories;
using ChatBook.DataAccess.Decorators;

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

            // DbContext
            services.AddSingleton<ApplicationDbContext>();

            // === Репозитории ===
            services.AddSingleton<IUserRepository, UserRepository>();
            services.AddScoped<IBookRepository>(sp =>
            {
                var baseRepo = new BookRepository(sp.GetRequiredService<ApplicationDbContext>());
                return new LoggingBookRepository(baseRepo);
            });
            services.AddSingleton<IMessageRepository, MessageRepository>();

            // === Доменные сервисы ===
            services.AddSingleton<ChatBook.Domain.Services.UserService>();

            // === ViewModels ===
            services.AddSingleton<AddBookViewModel>();
            services.AddSingleton<MainViewModel>();
            services.AddSingleton<BookSearchViewModel>();
            services.AddSingleton<BookViewModel>();
            services.AddSingleton<ProfileViewModel>();
            services.AddTransient<ChatViewModel>();
            services.AddSingleton<FriendsViewModel>();

            // === Формы и окна ===
            services.AddTransient<LoginForm>();
            services.AddTransient<MainForm>();
            services.AddTransient<FriendsForm>();
            services.AddTransient<EditProfileWindow>();
            services.AddTransient<ChatForm>();
            services.AddTransient<BookSearchWindow>();
            services.AddTransient<AddBookWindow>();

            // === Сервисы чата ===
            services.AddSingleton<IChatRepository, ChatRepository>();
            services.AddSingleton<IChatService, ChatService.Services.ChatService>();

            // БД инициализатор
            Database.SetInitializer(new DbInitializer());

            // Построение провайдера
            ServiceProvider = services.BuildServiceProvider();
        }



        static void ApplyMigrations()
        {
            var configuration = new Configuration();
            var migrator = new DbMigrator(configuration);
            migrator.Update();
        }
    }
}
