using ChatBook.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Data.Common;
using System.Data.SQLite;
using System.Windows.Forms;
using ChatBook.UI.Forms;
using ChatBook.DataAccess;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using ChatBook.Migrations;

namespace ChatBook
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            // Применяем миграции перед запуском
            ApplyMigrations();

            var serviceProvider = new ServiceCollection()
                // connectionString больше не нужен — EF сам возьмёт из App.config
                .AddSingleton<ApplicationDbContext>()
                .AddSingleton<UserService>()
                .BuildServiceProvider();

            // Seed Data (DbInitializer)
            Database.SetInitializer(new DB.DbInitializer());

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var userService = serviceProvider.GetService<UserService>();
            Application.Run(new LoginForm(userService));
        }


        static void ApplyMigrations()
        {
            var configuration = new Configuration();
            var migrator = new DbMigrator(configuration);
            migrator.Update();
        }
    }
}
